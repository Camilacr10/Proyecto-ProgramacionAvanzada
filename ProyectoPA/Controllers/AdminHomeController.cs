using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPA.Controllers
{
    public class AdminHomeController : Controller
    {
        //Se visualiza el menu de administrador
        public ActionResult MenuAdmin()
        {
            return View();
        }
    }
}