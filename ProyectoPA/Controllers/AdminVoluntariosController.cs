using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoPA;

namespace ProyectoPA.Controllers
{
    public class AdminVoluntariosController : Controller
    {
        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

        // GET: AdminVoluntarios
        public ActionResult Index()
        {
            // Se implementa un try-catch para manejar errores
            try
            {
                var voluntarios = db.Voluntarios.ToList();
                return View(voluntarios);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: No se pudieron cargar los voluntarios.";
                return View(); // Regresa la vista pero con mensaje de error
            }
        }


        // GET: AdminVoluntarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = db.Voluntarios.Find(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            return View(voluntario);
        }

        // GET: AdminVoluntarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminVoluntarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Voluntario,Nombre,Apellido,Correo,Telefono,Edad,Intereses")] Voluntario voluntario)
        {
            if (ModelState.IsValid)
            {
                db.Voluntarios.Add(voluntario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voluntario);
        }

        // GET: AdminVoluntarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = db.Voluntarios.Find(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            return View(voluntario);
        }

        // POST: AdminVoluntarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Voluntario,Nombre,Apellido,Correo,Telefono,Edad,Intereses")] Voluntario voluntario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voluntario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voluntario);
        }

        // GET: AdminVoluntarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = db.Voluntarios.Find(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            return View(voluntario);
        }

        // POST: AdminVoluntarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voluntario voluntario = db.Voluntarios.Find(id);
            db.Voluntarios.Remove(voluntario);
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
    }
}
