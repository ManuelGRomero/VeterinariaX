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
    public class ArchivosController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //[Authorize]
        public FileResult download(int id)
        {
            Archivos ar = db.archivos.Find(id);
            return File(ar.contenido, ar.formatoContenido);
        }
    }
}