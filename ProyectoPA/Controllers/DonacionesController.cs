using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPA.Controllers
{
    public class DonacionesController : Controller
    {
        // GET: Donaciones
        public ActionResult Index()
        {
            return View();
        }
    }
}