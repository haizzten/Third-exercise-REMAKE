using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Third_exercise_REMAKE.DAL.Infrastructure;
using Third_exercise_REMAKE.DAL.IRepository;

namespace Third_exercise_REMAKE.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        internal AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public virtual void Add(T entity) => _context.Set<T>().Add(entity);

        public virtual void AddRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);

        public virtual void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;

        public virtual void Delete(T entity) => _context.Remove(entity);

        public virtual void DeleteRange(IEnumerable<T> entities) => _context.Set<T>().RemoveRange(entities);

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return query.ToList();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(predicate);
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return query.ToList();
        }

        public bool Any() => _context.Set<T>().Any();
        public int SaveChanges() => _context.SaveChanges();


    }
}