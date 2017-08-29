using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class LancheIngrediente
    {
        [Key]
        public int IdLancheIngrediente { get; set; }
        public int IdLanche { get; set; }
        public int IdIngrediente { get; set; }
    }
}