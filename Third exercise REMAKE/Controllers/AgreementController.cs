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
        public async Task<ActionResult> GetAgreements([FromQuery] AgreementPagingDto dto)
        {
            return Ok(_agreementService.Paging(dto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgreementModel>> GetAgreement(string id)
        {
            return Ok(_agreementService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<AgreementModel>> CreateAgreement(AgreementDto dto)
        {
            int result = _agreementService.Create(dto);

            if (result > 0)
            {
                return CreatedAtAction("GetAgreement", new { id = dto.Id }, dto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgreement(string id, AgreementDto dto)
        {
            if (!_agreementService.IsExist(id))
            {
                return NotFound();
            }
            return Ok(_agreementService.Update(dto));
        }

        [HttpPost("filter")]
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
