using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AppRepositories
{
    public interface IRepository<T> where T: class
    {
        DbSet<T> DbSet { get; }
        DbContext DbContext { get; set; }

        IEnumerable<T> GetAll();
        IEnumerable<T> GetById(Guid id);
        void Add(T entity);
        void Edit(Guid id, T entity);
        void Delete(Guid id);
    }
}
