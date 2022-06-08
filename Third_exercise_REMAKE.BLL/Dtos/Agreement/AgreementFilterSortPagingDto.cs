using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Third_exercise_REMAKE.Core.Models;

namespace Third_exercise_REMAKE.BLL.Dtos.Agreement
{
    public class AgreementFilterSortPagingDto
    {
        public List<AgreementFilterDto> filterDtoList { get; set; }
        public AgreementPagingDto  pagingDto { get; set; }
        public AgreementSortDto sortDto { get; set; }
    }
}
