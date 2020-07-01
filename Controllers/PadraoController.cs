using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO;
using EletroStar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EletroStar.Controllers
{
    public class PadraoController<T> : Controller where T : PadraoViewModel
    {
      
        protected PadraoDAO<T> DAO { get; set; }
        protected bool GeraProximoId { get; set; }
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string name = this.ControllerContext.RouteData.Values["controller"].ToString();
            string action = this.ControllerContext.RouteData.Values["action"].ToString();

            if ((name == "Cliente" && action == "Create") ||
               (name == "Projeto" && action == "Index") ||
               (name == "Vendas" && action == "Index") ||
               (name == "Produto" && action == "Index")||
               (name == "Cliente" && action == "Salvar"))
            {
                ViewBag.Logado = false;
                ViewBag.Admin = false;
                base.OnActionExecuting(context);
            }
            else
            {
                if (!HelperController.VerificaUserLogado(HttpContext.Session))
                    context.Result = RedirectToAction("Index", "Login");
                else
                {
                    ViewBag.Logado = true;
                    ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
                    ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));
                    base.OnActionExecuting(context);
                }
            }
        }

        public virtual IActionResult Index()
        {
            ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
            ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
            ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

            var lista = DAO.Listagem();
            return View(lista);
        }

        public virtual IActionResult Create(int id)
        {
            ViewBag.operacao = "I";
            ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
            ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
            ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

            T model = Activator.CreateInstance(typeof(T)) as T; //new Model();
            PreencheDadosParaView("I", model);
            return View("Form", model);
        }

        protected virtual void PreencheDadosParaView(string operacao, T model)
        {
            if (GeraProximoId && operacao == "I")
                model.id = DAO.ProximoId();
        }

        public virtual IActionResult Salvar(T model, string operacao, string? confsenha, string? confemail)
        {
            try
            {
                ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
                ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
                ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

                ValidaDados(model, operacao, confsenha, confemail);
                if (ModelState.IsValid == false)
                {
                    ViewBag.operacao = operacao;                    
                    PreencheDadosParaView(operacao, model);
                    return View("Form", model);
                }
                else
                {
                    if (operacao == "I")
                        DAO.Insert(model);
                    else
                        DAO.Update(model);
                    return RedirectToAction("index");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.operacao = operacao;
                PreencheDadosParaView(operacao, model);
                return View("Form", model);
            }
        }

        protected virtual void ValidaDados(T model, string operacao, string? confsenha, string? confemail)
        {
            if (operacao == "I" && DAO.Consulta(model.id) != null)
                ModelState.AddModelError("Id", "Código já está em uso!");
            if (operacao == "A" && DAO.Consulta(model.id) == null)
                ModelState.AddModelError("Id", "Este registro não existe!");
            if (model.id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
        }

        public virtual IActionResult Edit(int id)
        {
            try
            {
                ViewBag.operacao = "A";
                ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
                ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
                ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

                var model = DAO.Consulta(id);

                if (model == null)
                    return RedirectToAction("index");
                else
                {
                    PreencheDadosParaView("A", model);
                    return View("Form", model);
                }
            }
            catch
            {
                return RedirectToAction("index");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                DAO.Delete(id);
                return RedirectToAction("index");
            }
            catch
            {
                return RedirectToAction("index");
            }
        }
    }
    
}