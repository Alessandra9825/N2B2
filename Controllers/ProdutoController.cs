using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO.Principais;
using EletroStar.DAO.Secundarias;
using EletroStar.Models;
using EletroStar.Models.Principais;
using EletroStar.Models.Secundarias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EletroStar.Controllers
{
    public class ProdutoController : PadraoController<ProdutoViewModel>
    {
        public ProdutoController()
        {
            DAO = new ProdutoDAO();
            GeraProximoId = true;
        }
        public IActionResult AtualizaGridIndex(string nome)
        {
            List<ProdutoViewModel> lista;
            if (nome == null)
                lista = ((DAO as ProdutoDAO).ListagemComFiltro(" "));
            else
                lista = ((DAO as ProdutoDAO).ListagemComFiltro(nome));

            FabricanteDAO fab = new FabricanteDAO();
            var fabricantes = fab.Listagem();

            CategoriaDAO cat = new CategoriaDAO();
            var categorias = cat.Listagem();

            foreach(ProdutoViewModel produto in lista)
            {
                produto.Fabricante = fabricantes.Find(c => c.id == produto.id_Fabricante).nome;
                produto.Categoria = categorias.Find(c => c.id == produto.id_Categoria).descricao;
            }            

            return PartialView("pvProduto", lista);
        }


        public override IActionResult Index()
        {
            ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
            ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
            ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

            var lista = DAO.Listagem();

            FabricanteDAO fab = new FabricanteDAO();
            var fabricantes = fab.Listagem();

            CategoriaDAO cat = new CategoriaDAO();
            var categorias = cat.Listagem();

            foreach (ProdutoViewModel produto in lista)
            {
                produto.Fabricante = fabricantes.Find(c => c.id == produto.id_Fabricante).nome;
                produto.Categoria = categorias.Find(c => c.id == produto.id_Categoria).descricao;
            }

            return View(lista);
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

        protected override void ValidaDados(ProdutoViewModel model, string operacao, string? confsenha, string? confemail)
        {
            base.ValidaDados(model, operacao, confsenha, confemail);
            if (string.IsNullOrEmpty(model.descricao))
                ModelState.AddModelError("descricao", "Preencha a descrição do produto.");

            if (string.IsNullOrEmpty(model.nome))
                ModelState.AddModelError("nome", "Preencha o nome do produto.");

            if (string.IsNullOrEmpty((model.valor).ToString()) || model.valor <= 0)
                ModelState.AddModelError("valor", "Preencha o valor corretamente.");

            if (model.id_Categoria <= 0)
                ModelState.AddModelError("id_Categoria", "Selecione uma categoria!");

            if (model.id_Fabricante <= 0)
                ModelState.AddModelError("id_Fabricante", "Selecione um fabricante!");

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
                    ProdutoViewModel p = DAO.Consulta(model.id);
                    model.ImagemEmByte = p.ImagemEmByte;
                }
                else
                {
                    model.ImagemEmByte = ConvertImageToByte(model.Imagem);
                }
            }

        }

        protected override void PreencheDadosParaView(string operacao, ProdutoViewModel model)
        {
            //utiliza os dados do pai
            base.PreencheDadosParaView(operacao, model);


            CategoriaDAO dao = new CategoriaDAO();
            var categorias = dao.Listagem();

            List<SelectListItem> listaCategorias = new List<SelectListItem>();

            listaCategorias.Add(new SelectListItem("Selecione uma categoria...", "0"));

            foreach (var cate in categorias)
            {
                SelectListItem item = new SelectListItem(cate.descricao, cate.id.ToString());
                listaCategorias.Add(item);
            }

            ViewBag.categorias = listaCategorias;

            FabricanteDAO dao1 = new FabricanteDAO();
            var fabricantes = dao1.Listagem();

            List<SelectListItem> listaFabricantes = new List<SelectListItem>();

            listaFabricantes.Add(new SelectListItem("Selecione um Fabricante...", "0"));

            foreach (var fab in fabricantes)
            {
                SelectListItem item = new SelectListItem(fab.nome, fab.id.ToString());
                listaFabricantes.Add(item);
            }

            ViewBag.fabricantes = listaFabricantes;
        }

        public IActionResult AtualizaGridIndexProduto(int categoriaId)
        {
            List<ProdutoViewModel> lista;
            if (categoriaId == 0)
                lista = (DAO as ProdutoDAO).Listagem();
            else
                lista = (DAO as ProdutoDAO).ListagemComFiltroCategoria(categoriaId);

            return PartialView("pvGrid", lista);
        }

        
    }


}
