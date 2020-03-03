using Domain.Contracts.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        protected DbSet<T> DbSet { get => _dbSet; }

        public Repository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity) => _dbSet.Add(entity);


        public void Delete(T entity) => _dbSet.Remove(entity);
        
        public IList<T> GetAll() => _dbSet.ToList();


        public void Update(T entity) => _dbSet.Update(entity);

        public T Get(Expression<Func<T, bool>> expression) => _dbSet
                                                              .Where(expression)
                                                              .First();
       
    }
}
