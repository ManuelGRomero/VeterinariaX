using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ElGalloDeOro.Models
{
    public class Archivos
    {
        public Archivos() { }

        public Archivos(HttpPostedFileBase fotoUpload, string tipoFoto)
        {
            this.formatoContenido = fotoUpload.ContentType;
            this.nombre = fotoUpload.FileName;
            this.tipo = tipoFoto;
        }

        [Key]
        public int archivoID { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public string formatoContenido { get; set; }
        public byte[] contenido { get; set; }


        //un archivo pertenece a una mascota
        public int mascotaID { get; set; }
        public Mascota mascota { get; set; }

        public static byte[] httpPostedFileBaseToByteArray(HttpPostedFileBase tpfb)
        {
            var reader = new System.IO.BinaryReader(tpfb.InputStream);
            return reader.ReadBytes(tpfb.ContentLength);
        }
    }
}