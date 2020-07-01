using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO;
using EletroStar.DAO.Secundarias;
using EletroStar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EletroStar.Controllers
{
    public class ClienteController : PadraoController<ClienteViewModel>
    {
        public ClienteController()
        {
            DAO = new ClienteDAO();
            GeraProximoId = true;
        }
        public IActionResult AtualizaGridIndex(string cpf)
        {
            List<ClienteViewModel> lista;
            if (cpf == null)
                lista =( (DAO as ClienteDAO).ListagemComFiltro( "9999999"));
            else
                lista = ( (DAO as ClienteDAO).ListagemComFiltro(cpf));

            return PartialView("pvCliente", lista);
        }
        /// <summary>
        /// Converte a imagem recebida no form em um vetor de bytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }

        protected override void ValidaDados(ClienteViewModel model, string operacao, string? confsenha, string? confemail)
        {
            //utiliza a heranca para validar dados iguais
            base.ValidaDados(model, operacao, confsenha, confemail);
                        
            if (string.IsNullOrEmpty(model.cpf))
                ModelState.AddModelError("CPF", "Preencha o CPF (ex: 123.456.789-12).");

            if (string.IsNullOrEmpty(model.nome))
                ModelState.AddModelError("nome", "Preencha o nome completo.");
            if (string.IsNullOrEmpty(model.email))
                ModelState.AddModelError("email", "Preencha o email.");
            if (string.IsNullOrEmpty(model.senha))
                ModelState.AddModelError("senha", "Preencha a senha");
            if (string.IsNullOrEmpty(model.tel_celular))
                ModelState.AddModelError("tel_celular", "Preencha o numero de celular.");
            if (string.IsNullOrEmpty(model.tel_residencial))
                ModelState.AddModelError("tel_residencial", "Preencha o numero do residencial");

            if (model.id_EstadoCivil <= 0)
                ModelState.AddModelError("id_EstadoCivil", "Selecione uma opção!");
            if (model.genero <= 0)
                ModelState.AddModelError("genero", "Selecione uma genêro!");

            if (model.nascimento >= DateTime.Today)
                ModelState.AddModelError("nascimento", "Preencha a data de nascimento corretamente");

            if (model.senha != confsenha)
                ModelState.AddModelError("senha", "Senhas não conferem!");

            if (model.email != confemail)
                ModelState.AddModelError("email", "E-mails não conferem!");

            //Imagem será obrigatio apenas na inclusão. 
            //Na alteração iremos considerar a que já estava salva.
            if (model.Imagem == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");

            if (model.Imagem != null && model.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");

            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que
                //já estava salva.
                if (operacao == "A" && model.Imagem == null)
                {
                    ClienteViewModel cli = DAO.Consulta(model.id);
                    model.ImagemEmByte = cli.ImagemEmByte;
                }
                else
                {
                    model.ImagemEmByte = ConvertImageToByte(model.Imagem);
                }
            }
        }

        public IActionResult SalvarCliente(ClienteViewModel model, string admin, string operacao, string? confsenha, string? confemail)
        {
            try
            {
                ModelState.Clear();

                ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
                ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
                ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

                if (admin == "A")
                    model.admin = true;

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

                    string controller = this.ControllerContext.RouteData.Values["controller"].ToString();

                    if (controller == "Cliente" || controller == "Endereco")
                        return RedirectToAction("Index", "Home");

                    return RedirectToAction("Index");
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

        protected override void PreencheDadosParaView(string operacao, ClienteViewModel model)
        {
            //utiliza os dados do pai
            base.PreencheDadosParaView(operacao, model);

            model.nascimento = DateTime.Now;

            EstadoCivilDAO dao = new EstadoCivilDAO();
            var EstadosCivis = dao.Listagem();

            List<SelectListItem> listaEstadosCivis = new List<SelectListItem>();

            listaEstadosCivis.Add(new SelectListItem("Selecione seu Estado Civil...", "0"));

            foreach (var estados in EstadosCivis)
            {
                SelectListItem item = new SelectListItem(estados.descricao, estados.id.ToString());
                listaEstadosCivis.Add(item);
            }

            ViewBag.EstadoCivis = listaEstadosCivis;

            List<SelectListItem> listaGeneros = new List<SelectListItem>();

            listaGeneros.Add(new SelectListItem("Selecione seu Genero...", "0"));
            listaGeneros.Add(new SelectListItem("Masculino", "M"));
            listaGeneros.Add(new SelectListItem("Feminino", "F"));

            ViewBag.Genero = listaGeneros;

            List<SelectListItem> listaAdmin = new List<SelectListItem>();

            listaAdmin.Add(new SelectListItem("Selecione o tipo de acesso..", "0"));
            listaAdmin.Add(new SelectListItem("Cliente", "C"));
            listaAdmin.Add(new SelectListItem("Adminitsrador", "A"));

            ViewBag.administrador = listaAdmin;

        }
        
    }
}