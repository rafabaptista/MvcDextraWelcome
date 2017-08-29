using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Lanche
    {
        [Key]
        public int IdLanche { get; set; }

        [Required(ErrorMessage ="Digite o nome do Lanche.")]
        public string LancheNome { get; set; }
        
    }
}