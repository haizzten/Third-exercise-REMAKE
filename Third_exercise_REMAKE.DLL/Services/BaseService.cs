using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Third_exercise_REMAKE.BLL.IServices;
using Third_exercise_REMAKE.DAL.IRepository;

namespace Third_exercise_REMAKE.BLL.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public readonly IBaseRepository<T> _TRepository;
        public BaseService(IBaseRepository<T> TRepository)
        {
            this._TRepository = TRepository;
        }

        public virtual int Create(T T)
        {
            _TRepository.Add(T);
            int result = _TRepository.SaveChanges();
            return result;
        }

        public virtual int Update(T T)
        {
            _TRepository.Update(T);
            int result = _TRepository.SaveChanges();
            return result;
        }

        public virtual void Delete(T T)
        {
            _TRepository.Delete(T);
            _TRepository.SaveChanges();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return _TRepository.Filter(predicate, includes);
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return _TRepository.GetAll(includes);
        }
    }
}
