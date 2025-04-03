using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoPA;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.IO;

namespace ProyectoPA.Controllers
{
    public class UsuariosController : Controller
    {
        private salvando_unas_patitasEntities db = new salvando_unas_patitasEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Rol);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.id_rol = new SelectList(db.Rols, "id_rol", "nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuario,id_rol,username,password,nombre,apellido,correo,telefono,ruta_imagen,activo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_rol = new SelectList(db.Rols, "id_rol", "nombre", usuario.id_rol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_rol = new SelectList(db.Rols, "id_rol", "nombre", usuario.id_rol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuario,id_rol,username,password,nombre,apellido,correo,telefono,ruta_imagen,activo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_rol = new SelectList(db.Rols, "id_rol", "nombre", usuario.id_rol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AboutMe()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }

            int userId = (int)Session["UserId"];
            var usuario = db.Usuarios.Find(userId);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(Usuario usuario, HttpPostedFileBase profilePicture)
        {
            // Obtener el usuario actual de la base de datos
            var usuarioActual = db.Usuarios.FirstOrDefault(u => u.username == User.Identity.Name);

            if (usuarioActual == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                // Actualizar imagen si se proporcionó
                if (profilePicture != null && profilePicture.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(profilePicture.FileName);
                    var path = Path.Combine(Server.MapPath("~/Imagenes/usuario/"), fileName);
                    profilePicture.SaveAs(path);
                    usuarioActual.ruta_imagen = "~/Imagenes/usuario/" + fileName;
                }

                // Actualizar otros campos
                usuarioActual.nombre = usuario.nombre;
                usuarioActual.apellido = usuario.apellido;
                usuarioActual.correo = usuario.correo;
                usuarioActual.telefono = usuario.telefono;

                db.Entry(usuarioActual).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("AboutMe");
            }

            return View("AboutMe", usuarioActual);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }

            int userId = (int)Session["UserId"];
            var usuario = db.Usuarios.Find(userId);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            // Validar que las nuevas contraseñas coincidan
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Las nuevas contraseñas no coinciden.");
                return View("AboutMe", usuario);
            }

            // Verificar contraseña actual
            string hashedCurrentPassword = HashPassword(currentPassword);
            if (usuario.password != hashedCurrentPassword)
            {
                ModelState.AddModelError("", "La contraseña actual es incorrecta.");
                return View("AboutMe", usuario);
            }

            // Actualizar contraseña
            usuario.password = HashPassword(newPassword);
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();

            // Mensaje de éxito
            TempData["SuccessMessage"] = "Contraseña actualizada correctamente.";
            return RedirectToAction("AboutMe");
        }

        // GET: Usuarios/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario modelo)
        {
            if (ModelState.IsValid)
            {
                // Encriptar la contraseña ingresada para comparar
                string hashedPassword = HashPassword(modelo.password);

                var usuario = db.Usuarios
                    .FirstOrDefault(u => u.correo == modelo.correo && u.password == hashedPassword);
                if (usuario != null)
                {
                    Session.Clear();
                    Session["UserId"] = usuario.id_usuario; // Almacenar ID en sesión
                    FormsAuthentication.SetAuthCookie(usuario.correo, false); // Usar correo como identificador
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos.");
            }
            return View(modelo);
        }

        // GET: Usuarios/Register
        public ActionResult Register()
        {
            ViewBag.id_rol = new SelectList(db.Rols, "id_rol", "nombre");
            return View();
        }

        // POST: Usuarios/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "id_usuario,id_rol,username,password,nombre,apellido,correo,telefono,ruta_imagen,activo")] Usuario usuario, HttpPostedFileBase profilePicture)
        {
            usuario.id_rol = 1;
            usuario.username = "Username Predeterminado";
            usuario.nombre = "Nombre Predeterminado";
            usuario.apellido = "Apellido Predeterminado";
            usuario.telefono = null;
            usuario.ruta_imagen = null;
            usuario.activo = "Sí";

            if (profilePicture != null && profilePicture.ContentLength > 0)
            {
                var fileName = System.IO.Path.GetFileName(profilePicture.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Imagenes/usuario/"), fileName);
                profilePicture.SaveAs(path);
                usuario.ruta_imagen = "~/Imagenes/usuario/" + fileName;
            }

            // Encriptar la contraseña ingresada
            string hashedPassword = HashPassword(usuario.password);
            usuario.password = hashedPassword;

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.id_rol = new SelectList(db.Rols, "id_rol", "nombre", usuario.id_rol);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // Limpiar la sesión
            Session.Clear();

            // Eliminar la cookie de autenticación
            FormsAuthentication.SignOut();

            // Redirigir a la página de login
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
