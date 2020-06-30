using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO;
using EletroStar.Models;
using EletroStar.DAO.Secundarias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EletroStar.Controllers
{
    public class EnderecoController : PadraoController<EnderecoViewModel>
    {
        public EnderecoController()
        {
            DAO = new EnderecoDAO();
            GeraProximoId = true;
        }

        public IActionResult EditCliente(int idCliente)
        {

            EnderecoDAO dao = new EnderecoDAO();
            EnderecoViewModel endereco = dao.Consulta_IdCliente(idCliente);

            if (endereco == null)
            {
                return RedirectToAction("Create");
            }
            else
            {                
                return RedirectToAction("Edit", endereco);
            }
                        
        }

        public override IActionResult Salvar(EnderecoViewModel model, string operacao, string? confsenha, string? confemail)
        {
            try
            {
                ValidaDados(model, operacao, confsenha, confemail);
                if (ModelState.IsValid == false)
                {
                    ViewBag.operacao = operacao;
                    ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
                    ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
                    ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));
                    PreencheDadosParaView(operacao, model);
                    return View("Form", model);
                }
                else
                {
                    if (operacao == "I")
                        DAO.Insert(model);
                    else
                        DAO.Update(model);
                    return RedirectToAction("Index", "Home");
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

        protected override void PreencheDadosParaView(string operacao, EnderecoViewModel model)
        {
            //utiliza os dados do pai
            base.PreencheDadosParaView(operacao, model);
                      

            UFDAO dao = new UFDAO();
            var uf = dao.Listagem();

            List<SelectListItem> listaEstados = new List<SelectListItem>();

            listaEstados.Add(new SelectListItem("Selecione seu Estado...", "0"));

            foreach (var estados in uf)
            {
                SelectListItem item = new SelectListItem(estados.descricao, estados.id.ToString());
                listaEstados.Add(item);
            }

            ViewBag.Estados = listaEstados;            
        }

        public int EstadoID(string uf)
        {
            switch (uf.ToUpper())
            {
                case "AC":
                    return 1;
                case "AL":
                    return 2;
                case "AP":
                    return 3;
                case "AM":
                    return 4;
                case "BA":
                    return 5;
                case "CE":
                    return 6;
                case "DF":
                    return 7;
                case "ES":
                    return 8;
                case "GO":
                    return 9;
                case "MA":
                    return 10;
                case "MT":
                    return 11;
                case "MS":
                    return 12;
                case "MG":
                    return 13;
                case "PA":
                    return 14;
                case "PB":
                    return 15;
                case "PR":
                    return 16;
                case "PE":
                    return 17;
                case "PI":
                    return 18;
                case "RJ":
                    return 19;
                case "RN":
                    return 20;
                case "RS":
                    return 21;
                case "RO":
                    return 22;
                case "RR":
                    return 23;
                case "SC":
                    return 24;
                case "SP":
                    return 25;
                case "SE":
                    return 26;
                case "TO":
                    return 27;
            }

            return 0;
        }
    }
}