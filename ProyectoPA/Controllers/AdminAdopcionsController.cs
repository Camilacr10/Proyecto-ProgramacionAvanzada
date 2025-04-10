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
    public class AdminAdopcionsController : Controller
    {
        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

        // GET: AdminAdopcions
        public ActionResult Index()
        {
            //Se implementa un try-catch para manejar errores
            try
            {
                var adopcions = db.Adopcions.Include(a => a.Mascota).Include(a => a.Usuario);
                return View(adopcions.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: No se pudieron cargar las adopciones.";
                return View(); // Regresa la vista pero con mensaje de error
            }
        }

        // GET: AdminAdopcions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopcion adopcion = db.Adopcions.Find(id);
            if (adopcion == null)
            {
                return HttpNotFound();
            }
            return View(adopcion);
        }

        // GET: AdminAdopcions/Create
        public ActionResult Create()
        {
            ViewBag.id_mascota = new SelectList(db.Mascotas, "id_mascota", "nombre");
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "username");
            return View();
        }

        // POST: AdminAdopcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_adopcion,id_usuario,id_mascota,fecha_adopcion")] Adopcion adopcion)
        {
            if (ModelState.IsValid)
            {
                db.Adopcions.Add(adopcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_mascota = new SelectList(db.Mascotas, "id_mascota", "nombre", adopcion.id_mascota);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "username", adopcion.id_usuario);
            return View(adopcion);
        }

        // GET: AdminAdopcions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopcion adopcion = db.Adopcions.Find(id);
            if (adopcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_mascota = new SelectList(db.Mascotas, "id_mascota", "nombre", adopcion.id_mascota);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "username", adopcion.id_usuario);
            return View(adopcion);
        }

        // POST: AdminAdopcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_adopcion,id_usuario,id_mascota,fecha_adopcion")] Adopcion adopcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adopcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_mascota = new SelectList(db.Mascotas, "id_mascota", "nombre", adopcion.id_mascota);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "username", adopcion.id_usuario);
            return View(adopcion);
        }

        // GET: AdminAdopcions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopcion adopcion = db.Adopcions.Find(id);
            if (adopcion == null)
            {
                return HttpNotFound();
            }
            return View(adopcion);
        }

        // POST: AdminAdopcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adopcion adopcion = db.Adopcions.Find(id);
            db.Adopcions.Remove(adopcion);
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
        public ActionResult ObtenerDetallesAdopcion(int id) // Recibe el ID de una adopción
        {
            var adopcion = db.Adopcions.Find(id); // Busca la adopción en la base de datos

            if (adopcion == null) // Si no la encuentra...
            {
                return HttpNotFound(); // ...devuelve error 404 (no encontrado)
            }

            // Creamos configuraciones para convertir a JSON
            var settings = new JsonSerializerSettings
            {
                // Si hay datos que se repiten entre sí, los ignora (evita errores)
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(adopcion, settings); // Convierte la adopción a JSON

            return Content(json, "application/json"); // Devuelve el JSON al navegador
        }
    }
}
