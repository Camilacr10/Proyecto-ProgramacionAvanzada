using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ProyectoPA.Controllers
{

    public class ListadoMController : Controller
    {
        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

        // Acción para el listado de mascotas
        public ActionResult listado()
        {
            var listadoMascotas = db.Mascotas.Where(m => m.disponibilidad == "Si").ToList();
            return View(listadoMascotas);
        }

        public ActionResult Detalles(int id)
        {
            var mascota = db.Mascotas.FirstOrDefault(m => m.id_mascota == id);

            if (mascota == null)
            {
                return HttpNotFound(); // Retorna error 404 si no encuentra la mascota
            }

            return View(mascota); // Retorna la vista detalles.cshtml con la información de la mascota
        }


    }
}

