using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProyectoPA;

namespace ProyectoPA.Controllers
{

    public class AdminMascotasController : Controller
    {
        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

        // GET: AdminMascotas
        public ActionResult Index()
        {
            // Se implementa un try-catch para manejar errores
            try
            {
                var mascotas = db.Mascotas.ToList();
                return View(mascotas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: No se pudieron cargar las mascotas.";
                return View(); // Regresa la vista pero con mensaje de error
            }
        }


        // GET: AdminMascotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.Mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // GET: AdminMascotas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mascota,nombre,edad,especie,raza,peso,sexo,castrado,vacunado,desparasitado,descripcion,fecha_rescate,ruta_imagen,disponibilidad")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                db.Mascotas.Add(mascota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mascota);
        }

        // GET: AdminMascotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.Mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // POST: AdminMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mascota,nombre,edad,especie,raza,peso,sexo,castrado,vacunado,desparasitado,descripcion,fecha_rescate,ruta_imagen,disponibilidad")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mascota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mascota);
        }

        // GET: AdminMascotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.Mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // POST: AdminMascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mascota mascota = db.Mascotas.Find(id);
            db.Mascotas.Remove(mascota);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //Se visualiza el menu de administrador
        public ActionResult MenuAdmin()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ObtenerDetallesMascota(int id) // Recibe el ID de una mascota
        {
            var mascota = db.Mascotas.Find(id); // Busca la mascota en la base de datos

            if (mascota == null) // Si no la encuentra...
            {
                return HttpNotFound(); // ...devuelve error 404 (no encontrado)
            }

            // Creamos configuraciones para convertir a JSON
            var settings = new JsonSerializerSettings
            {
                // Si hay datos que se repiten entre sí, los ignora (evita errores)
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(mascota, settings); // Convierte la mascota a JSON

            return Content(json, "application/json"); // Devuelve el JSON al navegador
        }
    }
}
