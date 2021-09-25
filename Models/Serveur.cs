using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Serveur
    {
        [Key]
        [Required]
        [StringLength(20)]
        [Display(Name = "Numéro du serveur")]
        public string NumServeur { get; set; }
        //public ICollection<AffectationDirectionServeur> AffectationDirectionServeurs { get; set; }
    }
}
