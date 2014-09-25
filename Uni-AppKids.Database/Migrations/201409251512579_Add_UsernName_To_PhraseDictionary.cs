namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UsernName_To_PhraseDictionary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhraseDictionary", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhraseDictionary", "UserName");
        }
    }
}
