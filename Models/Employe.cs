using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Employe
    {

        [Display(Name = "Identifiant de l'employé")]
        public int EmployeID { get; set; }

        [Required(ErrorMessage = "Le champ adresse courriel est requis.")]
        [StringLength(50)]
        [Display(Name = "Adresse courriel")]

        
        [RegularExpression(@"^[A-Za-z]+[0-9]*(.[A-Za-z0-9-]+)*@msss.gouv.qc.ca$",
            ErrorMessage = "Saisissez une adresse e-mail valide avec \"@msss.gouv.qc.ca\".")]
        //[EmailAddress(ErrorMessage = "Saisissez une adresse e-mail valide.")]
        public string AdrCourriel { get; set; }

        [Required(ErrorMessage = "Le champ Prenom est requis")]
        [StringLength(50)]
        [Display(Name = "Prenom de l'employé")]
        [RegularExpression(@"^[A-Z]+[-a-zA-Z]*[ ]*[-a-zA-Z]*[ ]*$",
            ErrorMessage = "Saisissez un Prenom correct")]

        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champ Nom est requis.")]
        [StringLength(50)]
        [Display(Name = "Nom de l'employé")]
        [RegularExpression(@"^[A-Z]+[-a-zA-Z]*[ ]*[-a-zA-Z]*[ ]*$",
            ErrorMessage = "Saisissez un Nom correct.")]
        public string Nom { get; set; }


        //public ICollection<Appartenir> Appartenirs { get; set; }

        public ICollection<PosteTravail> PosteTravails {get; set;}
    }
}
