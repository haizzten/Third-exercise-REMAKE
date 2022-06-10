using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Third_exercise_REMAKE.BLL.Dtos.Agreement;
using Third_exercise_REMAKE.Core.Models;

namespace Third_exercise_REMAKE.BLL.IServices
{
    public interface IAgreementService
    {
        public AgreementModel GetById(string id);
        public FilterResultDto FilterSortPaging(AgreementFilterSortPagingDto dto);
        public FilterResultDto Paging(AgreementPagingDto dto);
        public AgreementModel Update(AgreementDto dto);
        public int Create(AgreementDto dto);
        public bool Delete(string id);
        public bool IsExist(string id);
    }
}
