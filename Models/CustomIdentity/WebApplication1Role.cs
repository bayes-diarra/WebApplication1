using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.CustomIdentity
{
    public class WebApplication1Role : IdentityRole
    {
        [StringLength(100)]
        public string Description { get; set;
        }
        public ICollection<WebApplication1UserRole> UserRoles { get; set; }
    }
}
