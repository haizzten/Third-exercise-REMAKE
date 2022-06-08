using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Third_exercise_REMAKE.Core.Models;

namespace Third_exercise_REMAKE.BLL.BindingModels.Agreement
{
    public class FilterSortPagingModel
    {
        public List<FilterModel> filterModelList { get; set; }
        public PagingModel pagingModel { get; set; }
        public SortModel sortModel { get; set; }
    }
    public class QueryResultModel
    {
        public List<AgreementModel> agreementList { get; set; }
        public int lastIndex { get; set; }
    }
}
