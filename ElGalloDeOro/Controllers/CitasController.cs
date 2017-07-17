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
    public class CitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Citas
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var citas = db.citas.Include(c => c.personas);
            return View(citas.ToList());
        }

        // GET: Citas/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.personaID = new SelectList(db.personas, "personaID", "nombres");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Cita cita, bool enDetallesCita = false)
        {
            if (ModelState.IsValid)
            {
                db.citas.Add(cita);
                db.SaveChanges();

                //Si estaba en la pantalla de detalles de Persona
                if (enDetallesCita)
                {
                    //Redireccionar a esa pantalla
                    return RedirectToAction("Index", "Personas", new { id = cita.personaID });
                }
                //Sino
                else
                {
                    //Regresar una vista
                    return RedirectToAction("Index","Personas");
                }
            }

            ViewBag.personaID = new SelectList(db.personas, "personaID", "nombres", cita.personaID);
            return View(cita);
        }

        // GET: Citas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.personaID = new SelectList(db.personas, "personaID", "nombres", cita.personaID);
            return View(cita);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Cita cita, bool enDetallesCita = false)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();

                //Si estaba en la pantalla de detalles de Persona
                if (enDetallesCita)
                {
                    //Redireccionar a esa pantalla
                    return RedirectToAction("Index", "Personas", new { id = cita.personaID });
                }
                else
                {
                    return RedirectToAction("Details", "Personas", new { id = cita.personaID });
                }
            }
            ViewBag.personaID = new SelectList(db.personas, "personaID", "nombres", cita.personaID);
            return View(cita);
        }

        // GET: Citas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.citas.Find(id);
            db.citas.Remove(cita);
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
