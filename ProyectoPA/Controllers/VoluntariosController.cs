using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Win32.SafeHandles;
using ProyectoPA; // <-- Asegurar que usa el namespace del EDMX

namespace ProyectoPA.Controllers
{
    public class VoluntariosController : Controller
    {
        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities(); // Contexto del EDMX

        public ActionResult Index()
        {
            var voluntarios = db.Voluntarios.ToList(); // Obtener lista de voluntarios
            return View(voluntarios);
        }

        public ActionResult Create()
        {
            return View(new Voluntario()); // Asegurar que la vista recibe un solo objeto
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Voluntario voluntario)
        {
            if (ModelState.IsValid)
            {
                db.Voluntarios.Add(voluntario);
                db.SaveChanges();
                return RedirectToAction("Index", "Donaciones");
            }
            return View(voluntario);
        }
    }
}