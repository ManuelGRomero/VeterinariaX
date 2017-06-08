using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElGalloDeOro.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace ElGalloDeOro.Controllers
{
    [Authorize]
    public class MascotasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mascotas
        [Authorize(Roles = "Admin, Cliente")]
        public ActionResult Index()
        {
            return View(db.mascotas.ToList());
        }

        // GET: Mascotas/Details/5
        [Authorize(Roles = "Admin, Cliente")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Reportes(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }

            return View(mascota);
        }
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Cliente")]
        [HttpPost]
        public ActionResult Adoptar(int mascotaId)
        {
            //var mascotas = db.mascotas.Where(m=> m.Id==0).ToList();
            //var user = User.Identity.GetUserId();
            // M.ToList().
            var userId = User.Identity.GetUserId();

            var mascotaEncontrada = db.mascotas.Find(mascotaId);
            if (mascotaEncontrada.Id == null && mascotaEncontrada.Salida == null)
            {
                mascotaEncontrada.Id = userId;
                mascotaEncontrada.Salida = DateTime.UtcNow;
            }




            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Mascotas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            //var duenos = db.Users;
            //SelectList Id = new SelectList(User, "Id", "nombre");
            //ViewBag.Id = Id;
            return View();
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "mascotaID,nombre,raza,color,sexo,edad,vacuna,enfermded,fechaEsterilizacion,Entrada,Salida")] Mascota mascota, HttpPostedFileBase fotoUpload)
        {
            if (ModelState.IsValid)
            {
                //almacenar el archivo foto de la mascota
                if (fotoUpload != null && fotoUpload.ContentLength > 0)
                {
                    //Crearemos un nuevo registro de archivo
                    Archivos ar = new Archivos();
                    ar.formatoContenido = fotoUpload.ContentType;
                    ar.nombre = fotoUpload.FileName;
                    //Se asigna el tipo de foto que será
                    ar.tipo = "Perfil";
                    //se lee el archivo para descomponerlo
                    var reader = new BinaryReader(fotoUpload.InputStream);
                    ar.contenido = reader.ReadBytes(fotoUpload.ContentLength);
                    /*Se crea una nueva lista de archivos que contenga solamente
                     * el único archivo que acabamos de crear*/
                    mascota.archivos = new List<Archivos> { ar };

                }
                db.mascotas.Add(mascota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "mascotaID,nombre,raza,color,sexo,edad,vacuna,enfermded,fechaEsterilizacion,Entrada,Salida")] Mascota mascota, HttpPostedFileBase fotoUpload)
        {
            if (ModelState.IsValid)
            {
                if (fotoUpload != null && fotoUpload.ContentLength > 0)
                {
                    string tipoImagen = "Perfil";
                    Archivos fotoPerfil = db.archivos.SingleOrDefault(imgen => imgen.mascotaID == mascota.mascotaID && imgen.tipo == tipoImagen);

                    if (fotoPerfil != null && fotoPerfil.archivoID > 0)
                    {
                        fotoPerfil.contenido = Archivos.httpPostedFileBaseToByteArray(fotoUpload);

                        //Se modifica el registro de la foto
                        db.Entry(fotoPerfil).State = EntityState.Modified;
                    }
                    else //Sino existe la foto
                    {
                        //Se crea una nueva
                        fotoPerfil = new Archivos(fotoUpload, tipoImagen);
                        fotoPerfil.contenido = Archivos.httpPostedFileBaseToByteArray(fotoUpload);
                        //Y se relaciona con la mascota editada
                        fotoPerfil.mascotaID = mascota.mascotaID;
                        db.archivos.Add(fotoPerfil);
                    }
                }

                //    //Crearemos un nuevo registro de archivo                   
                //    Archivos fotoPerfil = db.archivos.Single(foto => foto.mascota.mascotaID == mascota.mascotaID);
                //    var reader = new BinaryReader(fotoUpload.InputStream);
                //    fotoPerfil.contenido = reader.ReadBytes(fotoUpload.ContentLength);
                //    //Se modifica el registro de la foto
                //    mascota.archivos = new List<Archivos> { fotoPerfil };
                //    db.Entry(mascota).State = EntityState.Modified;
                //}
                db.Entry(mascota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Mascotas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Mascota mascota = db.mascotas.Find(id);
            db.mascotas.Remove(mascota);
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
