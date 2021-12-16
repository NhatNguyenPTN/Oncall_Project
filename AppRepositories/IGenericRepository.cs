using EFCore.DbConnection;
using EFCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

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
