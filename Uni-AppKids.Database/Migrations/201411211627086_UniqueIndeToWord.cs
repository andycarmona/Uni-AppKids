namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueIndeToWord : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Words", "WordName", c => c.String(maxLength: 50));
            CreateIndex("dbo.Words", "WordName", true);
        }

        public override void Down()
        {
           
            DropIndex("dbo.Words", "WordName");
        }
    }
}
