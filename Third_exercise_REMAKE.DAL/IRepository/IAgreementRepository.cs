using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Third_exercise_REMAKE.Core.Models;

namespace Third_exercise_REMAKE.DAL.IRepository
{
    public interface IAgreementRepository : IBaseRepository<AgreementModel>
    {
        List<AgreementModel> FilterSortPaging(
            List<Expression<Func<AgreementModel, bool>>> filters,
            Func<IQueryable<AgreementModel>, IOrderedQueryable<AgreementModel>> sort,
            int? from,
            int? total);
        bool IsExist(int id);
    }

}
