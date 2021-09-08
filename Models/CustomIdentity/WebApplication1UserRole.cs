using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.CustomIdentity
{
    public class WebApplication1UserRole : IdentityUserRole<string>
    {
        public virtual WebApplication1User User { get; set; }
        public virtual WebApplication1Role Role { get; set; }
    }
}
