namespace Uni_AppKids.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAttributesToWordModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhraseDictionary",
                c => new
                    {
                        PhraseDictionaryId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        DictionaryName = c.String(),
                    })
                .PrimaryKey(t => t.PhraseDictionaryId);
            
            CreateTable(
                "dbo.Phrases",
                c => new
                    {
                        PhraseId = c.Int(nullable: false, identity: true),
                        AssignedDictionaryId = c.Int(nullable: false),
                        PhraseText = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        WordsIds = c.String(),
                    })
                .PrimaryKey(t => t.PhraseId)
                .ForeignKey("dbo.PhraseDictionary", t => t.AssignedDictionaryId, cascadeDelete: true)
                .Index(t => t.AssignedDictionaryId);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        WordId = c.Int(nullable: false, identity: true),
                        WordName = c.String(),
                        Image = c.String(),
                        SoundFile = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        WordDescription = c.String(),
                        AssignedPhraseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WordId)
                .ForeignKey("dbo.Phrases", t => t.AssignedPhraseId, cascadeDelete: true)
                .Index(t => t.AssignedPhraseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Words", "AssignedPhraseId", "dbo.Phrases");
            DropForeignKey("dbo.Phrases", "AssignedDictionaryId", "dbo.PhraseDictionary");
            DropIndex("dbo.Words", new[] { "AssignedPhraseId" });
            DropIndex("dbo.Phrases", new[] { "AssignedDictionaryId" });
            DropTable("dbo.Words");
            DropTable("dbo.Phrases");
            DropTable("dbo.PhraseDictionary");
        }
    }
}
