using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPA.Controllers
{
   
        public class ListadoMController : Controller
        {
            private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

            // Acción para el listado de mascotas
            public ActionResult listado()
            {
                return View();
            }
        }
    }

