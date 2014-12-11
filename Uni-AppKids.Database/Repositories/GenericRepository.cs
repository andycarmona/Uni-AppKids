// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericRepository.cs" company="Uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;

    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Database.EntityFramework;

    public class GenericRepository<TEntity> where TEntity : class
    {
        private readonly UniAppKidsDbContext context;

        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(UniAppKidsDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public GenericRepository()
        {
            
        }

        public virtual bool Compare<TEntity>(TEntity object1, TEntity object2)
        {
            // Get the type of the object
            Type type = typeof(TEntity);

            // return false if any of the object is false
            if (object1 == null || object2 == null)
            {
                return false;
            }

            // Loop through each properties inside class and get values for the property from both the objects and compare
            var object1Value = string.Empty;
            var object2Value = string.Empty;
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                if (property.Name != "ExtensionData")
                {
                    if (type.GetProperty(property.Name).GetValue(object1, null) != null)
                    {
                        object1Value = type.GetProperty(property.Name).GetValue(object1, null).ToString();
                    }

                    if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                    {
                        object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                    }

                    if (object1Value.Trim() != object2Value.Trim())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (
                var includeProperty in
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
            
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
         
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.AddOrUpdate(entityToUpdate);
            context.SaveChanges();
        }

        public virtual IEnumerable<TEntity> GetAllData()
        {
            IQueryable<TEntity> query = dbSet;
            return query.ToList();
        }   
    }
}
