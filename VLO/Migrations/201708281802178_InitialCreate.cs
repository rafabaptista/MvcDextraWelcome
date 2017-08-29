namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredientes",
                c => new
                    {
                        IdIngrediente = c.Int(nullable: false, identity: true),
                        IngredienteNome = c.String(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdIngrediente);
            
            CreateTable(
                "dbo.Lanches",
                c => new
                    {
                        IdLanche = c.Int(nullable: false, identity: true),
                        LancheNome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdLanche);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Lanches");
            DropTable("dbo.Ingredientes");
        }
    }
}
