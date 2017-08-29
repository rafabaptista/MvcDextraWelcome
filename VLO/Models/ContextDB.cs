using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class ContextDB : DbContext
    {
        public ContextDB() : base("name=ContextDB")
        {
        }

        public DbSet<Lanche> Lanches { get; set; }

        public System.Data.Entity.DbSet<VLO.Models.Ingrediente> Ingredientes { get; set; }

        public System.Data.Entity.DbSet<VLO.Models.LancheIngrediente> LancheIngredientes { get; set; }

        public System.Data.Entity.DbSet<VLO.Models.Cardapio> Cardapios { get; set; }
        
    }
}
