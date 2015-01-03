namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGameObjectTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameObject",
                c => new
                    {
                        GameObjectId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GameObjectId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameObject");
        }
    }
}
