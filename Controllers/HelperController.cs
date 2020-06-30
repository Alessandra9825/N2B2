using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EletroStar.Controllers
{
    public class HelperController : Controller
    {
        public static bool VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }

        public static bool VerificaUserAdmin(ISession session)
        {
            string admin = session.GetString("Admin");
            if (admin == null)
                return false;
            else
                return true;
        }

        public static string IdCliente(ISession session)
        {
            return session.GetString("IdCliente");
        }
    }
}