using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO.Principais;
using Microsoft.AspNetCore.Mvc;

namespace EletroStar.Controllers
{
    public class HomeController : Controller
    {
        public virtual IActionResult Index()
        {
            return View();
        }
    }
}