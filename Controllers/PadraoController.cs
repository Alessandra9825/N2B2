using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO;
using EletroStar.Models;
using Microsoft.AspNetCore.Mvc;

namespace EletroStar.Controllers
{
    public class PadraoController<T> : Controller where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected bool GeraProximoId { get; set; }

        public virtual IActionResult Index()
        {

            var lista = DAO.Listagem();
            return View(lista);
        }
        
        public IActionResult Create(int id)
        {
            ViewBag.operacao = "I";
            T model = Activator.CreateInstance(typeof(T)) as T; //new Model();
            PreencheDadosParaView("I", model);
            return View("Form", model);
        }

        
        protected virtual void PreencheDadosParaView(string operacao, T model)
        {
            if (GeraProximoId && operacao == "I")
                model.id = DAO.ProximoId();
        }

        public IActionResult Salvar(T model, string operacao, string? confsenha, string? confemail)
        {
            try
            {
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

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.operacao = "A";
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