using EFCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AppRepositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        public Repository()
        {
        }
        public DbSet<T> DbSet
        {
            get
            {
                return DbContext.Set<T>();
            }
        }

        public DbContext DbContext { get; set; }

        public void Add(T entity)
        {
            DbSet.Add(entity);

        }

        public void Delete(Guid id)
        {
            T existing = DbSet.Find(id);
            if (existing != null)
                DbSet.Remove(existing);
        }

        public void Edit(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
           return DbSet.AsEnumerable();
        }

        public IEnumerable<T> GetById(Guid id)
        {
            return DbSet.AsEnumerable();
        }
    }
}
