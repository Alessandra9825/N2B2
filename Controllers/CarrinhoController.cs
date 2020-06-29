using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO.Principais;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using EletroStar.Models;
using EletroStar.Models.Principais;
using EletroStar.Controllers;
using Microsoft.AspNetCore.Http;


namespace EletroStar.Controllers
{
    public class CarrinhoController : Controller
    {

        public IActionResult Index()
        {
            try
            {
                ProdutoDAO dao = new ProdutoDAO();
                var listaDeProdutos = dao.Listagem();
                var carrinho = ObtemCarrinhoNaSession();
                @ViewBag.TotalCarrinho = carrinho.Sum(c => c.quantidade);

                return View(listaDeProdutos);
            }
            catch
            {
                return View();
            }
        }


        public IActionResult Detalhes(int idProduto)
        {
            List<CarrinhoViewModel> carrinho = ObtemCarrinhoNaSession();

            ProdutoDAO proDao = new ProdutoDAO();
            var modelProduto = proDao.Consulta(idProduto);

            CarrinhoViewModel carrinhoModel = carrinho.Find(c => c.ProdutoId == idProduto);
            if (carrinhoModel == null)
            {
                carrinhoModel = new CarrinhoViewModel();
                carrinhoModel.ProdutoId = idProduto;
                carrinhoModel.Nome = modelProduto.descricao;
                carrinhoModel.quantidade = 0;
            }

            // preenche a imagem  carrinhoModel.ImagemEmBase64 = modelCidade.ImagemEmBase64;
            return View(carrinhoModel);
        }

        private List<CarrinhoViewModel> ObtemCarrinhoNaSession()
        {
            List<CarrinhoViewModel> carrinho = new List<CarrinhoViewModel>();
            string carrinhoJson = HttpContext.Session.GetString("carrinho");
            if (carrinhoJson != null)
                carrinho = JsonConvert.DeserializeObject <List< CarrinhoViewModel>> (carrinhoJson);

            return carrinho;
        }

        public IActionResult AdicionarCarrinho(int Produtoid, int Quantidade)
        {
            List<CarrinhoViewModel> carrinho = ObtemCarrinhoNaSession();

            CarrinhoViewModel carrinhoModel = carrinho.Find(c => c.ProdutoId == Produtoid);

            if (carrinhoModel != null && Quantidade == 0)
            {
                //tira do carrinho                 
                carrinho.Remove(carrinhoModel);
            }
            else if (carrinhoModel == null && Quantidade > 0)
            {
                //não havia no carrinho, vamos adicionar
                ProdutoDAO prodDao = new ProdutoDAO();
                var modelProduto = prodDao.Consulta(Produtoid);

                carrinhoModel = new CarrinhoViewModel();
                carrinhoModel.id = Produtoid;
                carrinhoModel.Nome = modelProduto.nome;
                carrinho.Add(carrinhoModel);
            }

            if (carrinhoModel != null)
                carrinhoModel.quantidade = Quantidade;

            string carrinhoJson = JsonConvert.SerializeObject(carrinho);
            HttpContext.Session.SetString("carrinho", carrinhoJson);

            return RedirectToAction("Index");
        }


        public IActionResult Visualizar()
        {
            ProdutoDAO dao = new ProdutoDAO(); var carrinho = ObtemCarrinhoNaSession();
            foreach (var item in carrinho)
            {
                var prod = dao.Consulta(item.ProdutoId);
                item.ImagemEmBase64 = prod.ImagemEmBase64;
            }

            return View(carrinho);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HelperController.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true; base.OnActionExecuting(context);
            }
        }

    }
}