using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Third_exercise_REMAKE.Core.Models;
using Third_exercise_REMAKE.DAL.Infrastructure;
using Third_exercise_REMAKE.DAL.IRepository;

namespace Third_exercise_REMAKE.DAL.Repositories
{
    public class AgreementRepository : BaseRepository<AgreementModel>, IAgreementRepository
    {
        public AgreementRepository(AppDbContext context) : base(context) { }

        public List<AgreementModel> FilterSortPaging(
            List<Expression<Func<AgreementModel, bool>>>? filters,
            Func<IQueryable<AgreementModel>, IOrderedQueryable<AgreementModel>>? sort,
            int? from,
            int? total)
        {
            var query = _context.Set<AgreementModel>().AsQueryable();

            if (filters != null)
                foreach (var filter in filters) { query = query.Where(filter); }

            if (sort != null)
                query = sort(query);

            if (from != null && total != null)
                query = query.Skip(from.Value).Take(total.Value);

            return query.ToList();

        }
        public bool IsExist(int id)
        {
            return _context.Set<AgreementModel>().Any(e => e.Id == id);
        }


    }
}
