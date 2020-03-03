using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Contracts.Infra.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();

        T Get(Expression<Func<T, bool>> expression);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
