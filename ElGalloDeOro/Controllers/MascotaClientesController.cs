using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElGalloDeOro.Models;

namespace ElGalloDeOro.Controllers
{
    [Authorize]
    public class MascotaClientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MascotaClientes
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var mascotaClientes = db.mascota.Include(m => m.personas);
            return View(mascotaClientes.ToList());
        }

        // GET: MascotaClientes/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MascotaClientes mascotaCliente = db.mascota.Find(id);
            if (mascotaCliente == null)
            {
                return HttpNotFound();
            }
            return View(mascotaCliente);
        }

        // GET: MascotaClientes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.personaID = new SelectList(db.personas, "personaID", "nombres");
            return View();
        }

        // POST: MascotaClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(MascotaClientes mascotaClientes, HttpPostedFileBase fotoUpload)
        {
            if (ModelState.IsValid)
            {
                //Almacenar archivo

                //Si se subio una foto
                if (fotoUpload != null && fotoUpload.ContentLength > 0)
                {
                    //Crearemos un nuevo registro de archivo

                    Imagen fotoPerfil = new Imagen(fotoUpload, "Perfil");
                    fotoPerfil.contenido = Imagen.httpPostedFileBaseToByteArray(fotoUpload);

                    mascotaClientes.imagenes = new List<Imagen> { fotoPerfil };
                }

                db.mascota.Add(mascotaClientes);

                db.SaveChanges();

                return RedirectToAction("Index", "Personas", new { id = mascotaClientes.personaID });
            }

            ViewBag.personaID = new SelectList(db.personas, "personaID", "nombres", mascotaClientes.personaID);
            return View(mascotaClientes);
        }

        // GET: MascotaClientes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MascotaClientes mascotaCliente = db.mascota.Find(id);
            if (mascotaCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.personaID = new SelectList(db.personas, "personaID", "nombres", mascotaCliente.personaID);
            return View(mascotaCliente);
        }

        // POST: MascotaClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(MascotaClientes mascota, HttpPostedFileBase fotoUpload)
        {
            if (ModelState.IsValid)
            {
                //Si se recibió una foto
                if (fotoUpload != null && fotoUpload.ContentLength > 0)
                {
                    string tipoImagen = "Perfil";
                    Imagen fotoPerfil = db.imagenes.SingleOrDefault(imgen => imgen.mascotaID == mascota.mascotaID && imgen.tipo == tipoImagen);

                    if (fotoPerfil != null && fotoPerfil.imagenID > 0)
                    {
                        fotoPerfil.contenido = Imagen.httpPostedFileBaseToByteArray(fotoUpload);

                        //Se modifica el registro de la foto
                        db.Entry(fotoPerfil).State = EntityState.Modified;
                    }
                    else //Sino existe la foto
                    {
                        //Se crea una nueva
                        fotoPerfil = new Imagen(fotoUpload, tipoImagen);
                        fotoPerfil.contenido = Imagen.httpPostedFileBaseToByteArray(fotoUpload);
                        //Y se relaciona con la mascota editada
                        fotoPerfil.mascotaID = mascota.mascotaID;
                        db.imagenes.Add(fotoPerfil);
                    }
                }
                //Modifica el registro actual
                db.Entry(mascota).State = EntityState.Modified;
                db.SaveChanges();
                //Si llega tan lejos, es que todo salió bien
                return RedirectToAction("Index", "Personas");
            }
            //ViewBag.personaID = new SelectList(db.personas, "personaID", "nombres", mascota.personaID);

            return View();
        }

        // GET: MascotaClientes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MascotaClientes mascota = db.mascota.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // POST: MascotaClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            MascotaClientes mascota = db.mascota.Find(id);
            db.mascota.Remove(mascota);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
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
