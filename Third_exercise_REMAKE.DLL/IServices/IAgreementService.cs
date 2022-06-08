using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Third_exercise_REMAKE.BLL.Dtos.Agreement;
using Third_exercise_REMAKE.Core.Models;

namespace Third_exercise_REMAKE.BLL.IServices
{
    public interface IAgreementService : IBaseService<AgreementModel>
    {
        public AgreementModel GetById(string id);
        public QueryResultModel FilterSortPaging(AgreementFilterSortPagingDto dto);
        public QueryResultModel Paging(AgreementPagingDto dto);
        public bool Delete(string id);
        public bool IsExist(string id);
    }
}
