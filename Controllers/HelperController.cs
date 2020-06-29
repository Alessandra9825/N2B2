using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EletroStar.Controllers
{
    public class HelperController : Controller
    {
     public static Boolean VerificaUserLogado(ISession session)
     {
            string logado = session.GetString("Logado");
            if (logado == null) 
                return false;
            else
                return true; 
     } 
    }
    

}