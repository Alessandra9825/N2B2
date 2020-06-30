using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO;
using EletroStar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EletroStar.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string email, string senha)
        {
            ClienteDAO c = new ClienteDAO();
            ClienteViewModel cliente = c.Login(email, senha);

            if (cliente != null)
            {
                HttpContext.Session.SetString("Logado", "true");
                HttpContext.Session.SetString("IdCliente", cliente.id.ToString());

                if (cliente.admin)
                    HttpContext.Session.SetString("Admin", "true");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Erro = "Usuário ou senha inválidos!";
                return View("Index");
            }
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}