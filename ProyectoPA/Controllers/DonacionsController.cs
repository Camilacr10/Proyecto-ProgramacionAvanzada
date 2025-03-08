using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProyectoPA.Controllers
{
    public class DonacionsController : Controller
    {
        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

        // GET: Donacions
        public ActionResult Index()
        {
            return View(db.Donacions.ToList());
        }

        // GET: Donacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donacion donacion = db.Donacions.Find(id);
            if (donacion == null)
            {
                return HttpNotFound();
            }
            return View(donacion);
        }

        // GET: Donacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_donacion,monto,nombre_donante,email_donante,metodo_pago,numero_tarjeta,fecha_expiracion,codigo_seguridad,fecha_donacion")] Donacion donacion)
        {
            if (ModelState.IsValid)
            {
                db.Donacions.Add(donacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donacion);
        }

        // GET: Donacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donacion donacion = db.Donacions.Find(id);
            if (donacion == null)
            {
                return HttpNotFound();
            }
            return View(donacion);
        }

        // POST: Donacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_donacion,monto,nombre_donante,email_donante,metodo_pago,numero_tarjeta,fecha_expiracion,codigo_seguridad,fecha_donacion")] Donacion donacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donacion);
        }

        // GET: Donacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donacion donacion = db.Donacions.Find(id);
            if (donacion == null)
            {
                return HttpNotFound();
            }
            return View(donacion);
        }

        // POST: Donacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donacion donacion = db.Donacions.Find(id);
            db.Donacions.Remove(donacion);
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
