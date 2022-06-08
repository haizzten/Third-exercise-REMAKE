using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Third_exercise_REMAKE.BLL.BindingModels.Agreement;
using Third_exercise_REMAKE.BLL.IServices;
using Third_exercise_REMAKE.BLL.Services;
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

        [HttpGet("All")]
        public async Task<ActionResult> GetAgreements()
        {
            List<AgreementModel> agreements = new List<AgreementModel>(_agreementService.GetAll());

            if (agreements.Any()) return Ok(agreements);
            else return NotFound();
        }

        // GET: api/Agreements
        [HttpGet()]
        public async Task<ActionResult> GetAgreements([FromQuery] int start, int end)
        {
            PagingModel pagingModel = new PagingModel { start = start, end = end };
            return Ok(_agreementService.Paging(pagingModel));
        }

        // GET: api/Agreement/5
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<AgreementModel>> GetAgreement(string id)
        {
            AgreementModel model = _agreementService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/Agreement/5
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> PutAgreement(int id, AgreementModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) return BadRequest();

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

        // POST: api/Agreement
        [HttpPost("Create")]
        public async Task<ActionResult<AgreementModel>> PostAgreement(AgreementModel agreement)
        {

            if (_agreementService == null)
            {
                return Problem("Entity set 'MyDbContext.Agreements' is null.");
            }

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

        [HttpPost("FilterSort")]
        public async Task<ActionResult> FilterSort([FromBody] FilterSortPagingModel filterSortModel)
        {
            var result = _agreementService.FilterSortPaging(filterSortModel);
            //var responseBody = new ObjectResult(result);

            return Ok(result);
        }

        // DELETE: api/Agreement/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAgreement(string id)
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
