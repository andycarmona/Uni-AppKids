namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        WordId = c.Int(nullable: false, identity: true),
                        WordName = c.String(),
                        Image = c.String(),
                        SoundFile = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        AssignedPhraseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WordId)
                .ForeignKey("dbo.Phrases", t => t.AssignedPhraseId, cascadeDelete: true)
                .Index(t => t.AssignedPhraseId);
            
            CreateTable(
                "dbo.Phrases",
                c => new
                    {
                        PhraseId = c.Int(nullable: false, identity: true),
                        AssignedDictionaryId = c.Int(nullable: false),
                        PhraseText = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PhraseId)
                .ForeignKey("dbo.PhraseDictionary", t => t.AssignedDictionaryId, cascadeDelete: true)
                .Index(t => t.AssignedDictionaryId);
            
            CreateTable(
                "dbo.PhraseDictionary",
                c => new
                    {
                        PhraseDictionaryId = c.Int(nullable: false, identity: true),
                        DictionaryName = c.String(),
                    })
                .PrimaryKey(t => t.PhraseDictionaryId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Phrases", new[] { "AssignedDictionaryId" });
            DropIndex("dbo.Words", new[] { "AssignedPhraseId" });
            DropForeignKey("dbo.Phrases", "AssignedDictionaryId", "dbo.PhraseDictionary");
            DropForeignKey("dbo.Words", "AssignedPhraseId", "dbo.Phrases");
            DropTable("dbo.PhraseDictionary");
            DropTable("dbo.Phrases");
            DropTable("dbo.Words");
        }
    }
}
