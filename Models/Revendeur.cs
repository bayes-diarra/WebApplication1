using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public enum TypeRevendeur
    {
        Fournisseur, Manufacturier
    }
    public class Revendeur
    {
        [Display(Name = "Identifiant du Revendeur")]
        public int RevendeurID { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Type de revendeur")]
        public TypeRevendeur? TypeRevendeur { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nom du Revendeur")]
        public string NomRevendeur { get; set; }

        //public ICollection<DetailAchat> DetailAchats { get; set; }
    }
}
