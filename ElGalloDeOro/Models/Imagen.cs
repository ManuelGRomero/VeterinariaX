using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElGalloDeOro.Models
{
    public class Imagen
    {
        public Imagen() { }

        public Imagen(HttpPostedFileBase fotoUpload, string tipoFoto)
        {
            this.formatoContenido = fotoUpload.ContentType;
            this.nombre = fotoUpload.FileName;
            this.tipo = tipoFoto;
        }

        [Key]
        public int imagenID { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public string formatoContenido { get; set; }
        public byte[] contenido { get; set; }

        //Una imagen corresponde a una mascota
        public int mascotaID { get; set; }
        public MascotaClientes mascota { get; set; }

        public static byte[] httpPostedFileBaseToByteArray(HttpPostedFileBase tpfb)
        {
            var reader = new System.IO.BinaryReader(tpfb.InputStream);
            return reader.ReadBytes(tpfb.ContentLength);
        }
    }
}