namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ArrayInts_with_WordId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phrases", "WordsIds", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Phrases", "WordsIds");
        }
    }
}
