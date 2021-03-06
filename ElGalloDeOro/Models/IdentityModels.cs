﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElGalloDeOro.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Apellido Paterno")]
        public string apellidoP { get; set; }
        [Display(Name = "Apellido Materno")]
        public string apellidoM { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime fechaNac { get; set; }
        [Display(Name = "Telefono")]
        public string telefono { get; set; }
        [Display(Name = "Domicilio")]
        public string domicilio { get; set; }
        [Display(Name = "Ciudad")]
        public string ciudad { get; set; }
        [Display(Name = "Rol de usuario")]
        public string rolid { get; set; }

        public virtual ICollection<Mascota> mascotas { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Mascota> mascotas { get; set; }
        public DbSet<Archivos> archivos { get; set; }

        public DbSet<Persona> personas { get; set; }
        public DbSet<Cita> citas { get; set; }
        public DbSet<Imagen> imagenes { get; set; }
        public DbSet<MascotaClientes> mascota { get; set; }
        public IEnumerable ApplicationUsers { get; internal set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}