using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElGalloDeOro.Models
{
    public class Persona
    {
        [Key]
        public int personaID { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Nombre(s)")]
        public string nombres { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        public string apellidoP { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        public string apellidoM { get; set; }

        [Required]
        [Display(Name = "Télefono")]
        public string telefono { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        //una persona tiene muchas citas
        virtual public ICollection<Cita> citas { get; set; }

        //una persona tiene muchas mascotas
        virtual public ICollection<MascotaClientes> mascota { get; set; }
    }
}