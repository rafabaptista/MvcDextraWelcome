namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LancheIngredientes",
                c => new
                    {
                        IdLancheIngrediente = c.Int(nullable: false, identity: true),
                        IdLanche = c.Int(nullable: false),
                        IdIngrediente = c.Int(nullable: false),
                        IdCatalogo = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IdLancheIngrediente);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LancheIngredientes");
        }
    }
}
