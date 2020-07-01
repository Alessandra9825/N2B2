using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO.Principais;
using EletroStar.DAO.Relacionamento;
using EletroStar.Models.Principais;
using EletroStar.Models.Relacionamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using EletroStar.DAO.Secundarias;
using Microsoft.AspNetCore.Mvc.Rendering;
using EletroStar.Models;
using EletroStar.DAO;

namespace EletroStar.Controllers
{
    public class CarrinhoController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HelperController.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }
        public virtual IActionResult Index()
        {
            ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
            ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
            ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));
            ProdutoDAO DAO = new ProdutoDAO();
            var lista = DAO.Listagem();

            CategoriaDAO categDao = new CategoriaDAO();
            var categorias = categDao.Listagem();

            List<SelectListItem> listaCategorias = new List<SelectListItem>();
            listaCategorias.Add(new SelectListItem("Selecione uma categoria...", "0"));
            foreach (var cat in categorias)
            {
                SelectListItem item = new SelectListItem(cat.descricao, cat.id.ToString());
                listaCategorias.Add(item);
            }
            ViewBag.categorias = listaCategorias;

            return View(lista);
        }
        public IActionResult IndexProduto()
        {
            ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
            ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
            ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

            CarrinhoDAO dao = new CarrinhoDAO();

            int idCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));            

            var listaProdutos = dao.Listagem(idCliente);            

            ViewBag.TotalCarrinho = listaProdutos.Sum(c => c.quantidade);
            ViewBag.TotalValor = listaProdutos.Sum(c => (c.quantidade * c.valor));

            return View(listaProdutos);
        }

        public IActionResult IndexProjeto()
        {
            ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
            ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
            ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

            CarrinhoDAO dao = new CarrinhoDAO();

            int idCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

            var listaProjetos = dao.ListagemProjetos(idCliente);

            ViewBag.TotalCarrinho = listaProjetos.Sum(c => c.quantidade);
            ViewBag.TotalValor = listaProjetos.Sum(c => (c.quantidade * c.valor));

            return View(listaProjetos);
        }

        
        public IActionResult AdicionarProduto(int id_Produto, int quantidade)
        {
            try
            {
                int idCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

                CarrinhoDAO car = new CarrinhoDAO();
                CarrinhoViewModel carrinho = car.Consulta_IdCliente(idCliente);

                if(carrinho == null)
                {
                    carrinho = new CarrinhoViewModel(){
                        id_Cliente = idCliente
                    };
                    
                    car.Insert(carrinho);

                    carrinho = car.Consulta_IdCliente(idCliente);
                } 

                Carrinho_ProdutoDAO dao = new Carrinho_ProdutoDAO(); 
                Carrinho_ProdutoViewModel prod = dao.Consulta(carrinho.id, id_Produto);

                if(prod == null)
                {
                    prod = new Carrinho_ProdutoViewModel(){
                        id_Carrinho = carrinho.id,
                        id_Produto = id_Produto,
                        quantidade = quantidade
                    };                    

                    dao.Insert(prod);
                }
                else
                {
                    prod.quantidade = quantidade;
                    dao.Update(prod);
                }       

                return RedirectToAction("Index", "Vendas");
            }
            catch(Exception error)
            {
                ViewBag.error = error.Message;
                return RedirectToAction("Index", "Vendas");
            }
        }

        public IActionResult AdicionarProjeto(int id_Projeto, int quantidade)
        {
            CarrinhoDAO dao = new CarrinhoDAO();
            int idCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));
               
            return View();
        }

        public IActionResult AtualizaGridIndexP(int idCategoria)
        {
            ProdutoDAO DAO = new ProdutoDAO();
            List<ProdutoViewModel> lista;
            if (idCategoria == 0)
                lista = DAO.ListagemComFiltroCategoria(0);
            else
                lista = DAO.ListagemComFiltroCategoria(idCategoria);

            return PartialView("pvIndex", lista);
        }

    }
}