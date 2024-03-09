using AirBnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Common.Interfaces
{
    public interface IRepository <T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        bool Any(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Delete(T entity);

    }
}
