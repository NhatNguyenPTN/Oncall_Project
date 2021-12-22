using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public interface IGenericRepository<T> where T: class
    {                
        IEnumerable<T> GetAll();        
        T GetById(Guid id);
        void Add(T entity);
        void Edit( T entity);
        void Delete(T entity);       
    }
}
