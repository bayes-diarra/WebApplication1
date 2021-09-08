using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.CustomIdentity
{
    public class WebApplication1Role : IdentityRole
    {
        public ICollection<WebApplication1UserRole> UserRoles { get; set; }
    }
}
