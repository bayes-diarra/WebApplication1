using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public enum TypePoste
    {
        Equipe, Individuel
    }

    public class PosteTravail
    {
        [Key]
        [Required]
        [StringLength(20)]
        [Display(Name = "Numéro du poste de travail")]
        public string NumPoste { get; set; }

        [Required]
        [Display(Name = "Type de poste de travail")]
        public TypePoste TypePoste { get; set; }

        [Required]
        public int EmployeID { get; set; }
        [ForeignKey("EmployeID")]
        public Employe Employe { get; set; }
        ////public ICollection<AffectationEmployePoste> AffectationEmployePostes { get; set; }
    }
}
