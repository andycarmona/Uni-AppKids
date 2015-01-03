namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DifficultyLevelAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameList", "DifficultyLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameList", "DifficultyLevel");
        }
    }
}
