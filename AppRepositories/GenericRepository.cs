using EFCore.DbConnection;
using EFCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbFactory _dbFactory;
        private DbSet<T> _dbSet;

        public GenericRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            
        }
        protected DbSet<T> DbSet
        {
            get => _dbSet ??= _dbFactory.DbContext.Set<T>();
        }


        public IEnumerable<T> GetAll()
        {
            return DbSet.AsEnumerable();
        }               
        public T GetById(Guid id)
        {
            return DbSet.Find(id);
        }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }
        public void Delete(T entity)
        {            
             DbSet.Remove(entity);
        }
        public void Edit(T entity)
        {
            DbSet.Update(entity);
        }    
        
    }
}
