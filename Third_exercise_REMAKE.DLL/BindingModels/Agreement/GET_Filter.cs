using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Third_exercise_REMAKE.BLL.BindingModels.Agreement
{
    public class FilterModel
    {
        public string columnName { get; set; }
        public string valueType { get; set; }
        public string filterType { get; set; }
        public string filterValue { get; set; }
    }


}
