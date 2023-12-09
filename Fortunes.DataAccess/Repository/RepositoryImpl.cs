using Fortunes.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortunes.DataAccess.Repository
{
    public class RepositoryImpl<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<T> _dbset;
        public RepositoryImpl(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbset;
            return query.ToList();
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbset.Where(filter);
            return query.FirstOrDefault();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        public void DeleteAll(IEnumerable<T> entity)
        {
            _dbset.RemoveRange(entity);
        }
    }
}
