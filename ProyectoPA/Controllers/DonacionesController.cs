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

        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

        // GET: Donaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "monto,nombre_donante,email_donante,metodo_pago,numero_tarjeta,fecha_expiracion,codigo_seguridad,fecha_donacion")] Donacion donacion)
        {
            if (ModelState.IsValid)
            {
                donacion.fecha_donacion = DateTime.Now; // Asignar la fecha actual
                db.Donacions.Add(donacion);
                db.SaveChanges();
                return RedirectToAction("Confirmacion");
            }

            return View(donacion);
        }

        // Vista de confirmación
        public ActionResult Confirmacion()
        {
            return View();
        }
    }
}