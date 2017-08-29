using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Cardapio : LancheIngrediente
    {
        public string LancheNome { get; set; }
        public string IngredienteNome { get; set; }
        public decimal Valor { get; set; }
    }
}