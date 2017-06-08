using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElGalloDeOro.Models
{
    public class Mascota
    {
        [Key]
        public int mascotaID { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Raza")]
        public string raza { get; set; }
        [Display(Name = "Color")]
        public string color { get; set; }
        [Display(Name = "Sexo")]
        public string sexo { get; set; }
        [Display(Name = "Edad")]
        public string edad { get; set; }
        [Display(Name = "Vacunas")]
        public string vacuna { get; set; }
        [Display(Name = "Enfermedades")]
        public string enfermded { get; set; }
        [Display(Name = "Fecha de Esterilización")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? fechaEsterilizacion { get; set; }
        [Display(Name = "Fecha de Entrada al C.A.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Entrada { get; set; }
        [Display(Name = "Fecha de Salida al C.A.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Salida { get; set; }


        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Archivos> archivos { get; set; }
    }
}