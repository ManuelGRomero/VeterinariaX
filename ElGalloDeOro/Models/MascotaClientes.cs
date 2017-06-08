using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElGalloDeOro.Models
{
    public class MascotaClientes
    {
        [Key]
        public int mascotaID { get; set; }

        [Required]
        [Display(Name = "Nombre mascota")]
        public string nombreMascota { get; set; }

        [Required]
        [Display(Name = "Raza")]
        public string Raza { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string sexo { get; set; }

        [Required]
        [Display(Name = "Señas particulares")]
        public string señasParticulares { get; set; }



        //una cita tiene una persona por cita
        [Display(Name = "Nombre del dueño")]
        public int personaID { get; set; }
        virtual public Persona personas { get; set; }

        public virtual ICollection<Imagen> imagenes { get; set; }
    }
}