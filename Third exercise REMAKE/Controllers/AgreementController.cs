using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Third_exercise_REMAKE.BLL.Dtos.Agreement;
using Third_exercise_REMAKE.BLL.IServices;
using Third_exercise_REMAKE.Core.Models;

namespace Third_exercise_REMAKE.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class AgreementController : ControllerBase
    {
        private IAgreementService _agreementService;
        public AgreementController(IAgreementService agreementService)
        {
            this._agreementService = agreementService;
        }

        [HttpGet()]
        public async Task<ActionResult> Get([FromQuery] int start, int end)
        {
            AgreementPagingDto dto = new AgreementPagingDto { start = start, end = end };
            return Ok(_agreementService.Paging(dto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgreementModel>> Get(string id)
        {
            AgreementModel model = _agreementService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AgreementModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (_agreementService.IsExist(id + ""))
            {
                _agreementService.Update(model);
                return Ok(_agreementService.GetById(id + ""));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<ActionResult<AgreementModel>> Post(AgreementModel agreement)
        {
            int result = _agreementService.Create(agreement);

            if (result == 0)
            {
                return BadRequest();
            }
            else if (result > 0)
            {
                return CreatedAtAction("GetAgreement", new { id = agreement.Id }, agreement);
            }

            return NoContent();
        }

        [HttpPost("filter-sort-paging")]
        public async Task<ActionResult> GetByFilterSortPaging([FromBody] AgreementFilterSortPagingDto dto)
        {
            var result = _agreementService.FilterSortPaging(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isSuccess = _agreementService.Delete(id);
            if (isSuccess == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
