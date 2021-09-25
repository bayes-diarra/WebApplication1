using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Direction
    {
        [Key]
        [Required]
        [Display(Name = "Unité administrative")]
        public int NumDirection { get; set; }

        
        [StringLength(100)]
        [Display(Name = "Ancien nom de la direction")]
        public string AncienNomDirection { get; set; }


        [Required]
        [StringLength(100)]
        [Display(Name = "Nom de la direction")]
        public string NomDirection { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Acronyme de la direction")]
        [RegularExpression(@"^[A-Z]*$")]
        public string AcronimeDirection { get; set; }

        //public ICollection<RequeteAchat> RequeteAchats { get; set; }
        //public ICollection<Appartenir> Appartenirs { get; set; }
        //public ICollection<AffectationDirectionServeur> AffectationDirectionServeurs { get; set; }
        //public ICollection<AffectationDirectionPoste> AffectationDirectionPostes { get; set; }
    }
}
