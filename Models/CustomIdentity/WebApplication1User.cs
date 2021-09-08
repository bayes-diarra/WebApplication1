using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models.CustomIdentity
{
    public enum Role
    {
        Administrateur, Externe, Interne
    }
    public class WebApplication1User : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public DateTime DateBirth { get; set; }

        [PersonalData]
        public string UniteAdmin { get; set; }

        [PersonalData]
        public string Poste { get; set; }

        [PersonalData]
        public Role Role { get; set; }

        public ICollection<WebApplication1UserRole> UserRoles { get; set; }
    }
}
