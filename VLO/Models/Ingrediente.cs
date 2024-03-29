﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Ingrediente
    {
        [Key]
        public int IdIngrediente { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Digite o Nome do Ingrediente.")]
        public string IngredienteNome { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Digite o Valor do Ingrediente.")]
        [DataType(DataType.Currency, ErrorMessage = "Digite o valore separado por virgula (,)")]
        public decimal Valor { get; set; }
    }
}