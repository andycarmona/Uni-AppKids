namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGameListTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameList",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        AssignedGameObjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.GameObject", t => t.AssignedGameObjectId, cascadeDelete: true)
                .Index(t => t.AssignedGameObjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameList", "AssignedGameObjectId", "dbo.GameObject");
            DropIndex("dbo.GameList", new[] { "AssignedGameObjectId" });
            DropTable("dbo.GameList");
        }
    }
}
