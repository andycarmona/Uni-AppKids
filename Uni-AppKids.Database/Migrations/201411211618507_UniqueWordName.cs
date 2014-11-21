namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueWordName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Words", "WordName", c => c.String(nullable: false));
         
        }

        public override void Down()
        {
            AlterColumn("dbo.Words", "WordName", c => c.String());
        
        }
    }
}
