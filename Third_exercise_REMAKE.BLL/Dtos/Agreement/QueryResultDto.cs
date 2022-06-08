using Third_exercise_REMAKE.Core.Models;

namespace Third_exercise_REMAKE.BLL.Dtos.Agreement
{
    public class FilterResultDto
    {
        public List<AgreementModel> agreementList { get; set; }
        public int lastIndex { get; set; }
    }
}
