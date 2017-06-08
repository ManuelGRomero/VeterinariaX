using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElGalloDeOro.Models;
using System.ComponentModel.DataAnnotations;

namespace ElGalloDeOro.VMUserName
{
    public class VMUserName
    {
        [Key]
        public string id { get; set; }
        public string nombrecompleto { get; set; }
        public string email { get; set; }
        public string rolid { get; set; }

        public VMUserName(ApplicationUser user)
        {
            this.nombrecompleto = user.nombre + " " + user.apellidoP + " " + user.apellidoM;
            this.email = user.Email;
            this.rolid = user.Roles.First().RoleId;
            this.id = user.Id;
            if (user.Roles.Count > 0)
            {
                this.rolid = user.Roles.First().RoleId;
            }
        }
    }
}