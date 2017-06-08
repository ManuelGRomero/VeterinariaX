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
    public class ImagenController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Imagen

        public FileResult download(int id = 0)
        {
            var imagen = db.imagenes.Find(id);
            return File(imagen.contenido, imagen.formatoContenido);
        }
    }
}