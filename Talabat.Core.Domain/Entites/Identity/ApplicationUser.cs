using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Entites.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public required string DispalyName { get; set; }
        public virtual Address? Address { get; set; }
    }
}
