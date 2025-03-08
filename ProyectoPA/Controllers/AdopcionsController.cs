using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProyectoPA.Controllers
{
    public class AdopcionsController : Controller
    {
        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

        // GET: Adopcions
        public ActionResult Index()
        {
            var mascotasDisponibles = db.Mascotas
                            .Where(m => m.disponibilidad == "SI") 
                            .Select(m => new { id_mascota = m.id_mascota, nombre = m.nombre })
                            .ToList();

            ViewBag.MascotasDisponibles = new SelectList(mascotasDisponibles, "id_mascota", "nombre");


            return View();
        }

        // GET: Adopcions/Details/5
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

        // GET: Adopcions/Create
        public ActionResult Create()
        {
            ViewBag.id_mascota = new SelectList(db.Mascotas, "id_mascota", "nombre");
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "username");
            return View();
        }

        // POST: Adopcions/Create
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

        // GET: Adopcions/Edit/5
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

        // POST: Adopcions/Edit/5
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

        // GET: Adopcions/Delete/5
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

        // POST: Adopcions/Delete/5
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
    }
}
