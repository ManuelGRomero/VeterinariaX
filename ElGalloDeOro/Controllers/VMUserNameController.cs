using ElGalloDeOro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ElGalloDeOro.Controllers
{
    internal class VMUserName
    {
        private ApplicationUser User;

        public VMUserName(ApplicationUser User)
        {
            this.User = User;
        }
    }
}