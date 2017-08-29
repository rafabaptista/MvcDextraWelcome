namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostClass1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LancheIngredientes", "LancheNome", c => c.String());
            AddColumn("dbo.LancheIngredientes", "IngredienteNome", c => c.String());
            AddColumn("dbo.LancheIngredientes", "Valor", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.LancheIngredientes", "IdCatalogo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LancheIngredientes", "IdCatalogo", c => c.Int());
            DropColumn("dbo.LancheIngredientes", "Valor");
            DropColumn("dbo.LancheIngredientes", "IngredienteNome");
            DropColumn("dbo.LancheIngredientes", "LancheNome");
        }
    }
}
