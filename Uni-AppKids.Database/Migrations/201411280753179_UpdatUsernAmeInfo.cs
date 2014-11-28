namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatUsernAmeInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phrases", "UserName", c => c.String());
            AddColumn("dbo.Words", "UserName", c => c.String());
            DropColumn("dbo.PhraseDictionary", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhraseDictionary", "UserName", c => c.String());
            DropColumn("dbo.Words", "UserName");
            DropColumn("dbo.Phrases", "UserName");
        }
    }
}
