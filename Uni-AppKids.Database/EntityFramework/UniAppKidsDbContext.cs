// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LearnSchoolDbContext.cs" company="Uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Database.EntityFramework
{
    using System.Data.Entity;

    using Uni_AppKids.Core.EntityModels;

    public class UniAppKidsDbContext : DbContext
    {
        public UniAppKidsDbContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual IDbSet<Word> Words { get; set; }

        public virtual IDbSet<Phrase> Phrases { get; set; }

        public virtual IDbSet<PhraseDictionary> PhraseDictionaries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Word>().ToTable("Words");
            modelBuilder.Entity<Phrase>().ToTable("Phrases");
            modelBuilder.Entity<PhraseDictionary>().ToTable("PhraseDictionary");
        }
    }
}
