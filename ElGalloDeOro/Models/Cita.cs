using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ElGalloDeOro.Models
{
    public class Cita
    {
        [Key]
        public int citaID { get; set; }

        [Required]
        [Display(Name = "Estado de la mascota")]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Motivo de la cita")]
        public string MotivoCita { get; set; }

        [Required]
        [Display(Name = "Fecha de la Cita")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]//sirve para cambiar el tipo de dato de "DateTime" por "Date"
        public DateTime fecha { get; set; }

        //una cita tiene una persona por cita
        [Display(Name = "Nombre dueño")]
        public int personaID { get; set; }
        virtual public Persona personas { get; set; }
    }
}