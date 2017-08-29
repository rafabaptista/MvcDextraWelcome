using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class ContextDB : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ContextDB() : base("name=ContextDB")
        {
        }

        public DbSet<Lanche> Lanches { get; set; }

        public System.Data.Entity.DbSet<VLO.Models.Ingrediente> Ingredientes { get; set; }

        public System.Data.Entity.DbSet<VLO.Models.LancheIngrediente> LancheIngredientes { get; set; }

        public System.Data.Entity.DbSet<VLO.Models.Cardapio> Cardapios { get; set; }

        //public System.Data.Entity.DbSet<VLO.Models.Cardapio> Cardapios { get; set; }

        //public System.Data.Entity.DbSet<VLO.Models.LancheIngrediente> LancheIngredientes { get; set; }
    }
}
