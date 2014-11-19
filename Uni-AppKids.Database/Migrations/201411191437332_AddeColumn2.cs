namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddeColumn2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Words", "WordDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Words", "WordDescription");
        }
    }
}
