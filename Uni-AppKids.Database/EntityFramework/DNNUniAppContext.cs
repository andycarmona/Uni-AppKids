using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni_AppKids.Core.EntityModels;

namespace Uni_AppKids.Database.EntityFramework
{
   public class DNNUniAppContext : DbContext
    {
       public DNNUniAppContext()
           : base("DNNMainDb")
        {
        }



        public virtual IDbSet<PhraseDictionary> PhraseDictionaries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PhraseDictionary>().ToTable("PhraseDictionary");
        }
    }
}
