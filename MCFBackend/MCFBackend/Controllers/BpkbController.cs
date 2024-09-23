using Azure;
using MCFBackend.Services.Helper;
using MCFBackend.Services.Interfaces;
using MCFBackend.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace MCFBackend.Controllers
{
    public class BpkbController : Controller
    {
        private readonly IBpkb iBpkb;
        public BpkbController(IBpkb iBpkb) => this.iBpkb = iBpkb;
        [HttpGet("api/GetAllBpkb")]
        public async Task<ActionResult> GetAllBpkb()
        {
            var bpkbs = iBpkb.GetAllBpkbAsync();

            var response = new
            {
                ResponseBpkbs = bpkbs
            };
            return Ok(response);
        }

        [HttpGet("api/GetAllData")]
        public async Task<ActionResult> GetAllData()
        {
            var locations = iBpkb.GetAllLocationsAsync();
            var userName = User.Identity.Name;
            var response = new
            {
                User = new
                {
                    userName
                },
                Locations = locations
            };

            return Ok(new
            {
                success = true,
                message = "Get record successfully",
                data = response
            });
        }

        [HttpPost("api/TrBpkb")]
        public async Task<ActionResult> Create([FromBody] tr_bpkb trBpkb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Model is invalid",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            try
            {
                await iBpkb.AddAsync(trBpkb);
                return Ok(new
                {
                    success = true,
                    message = "Record created successfully",
                    data = trBpkb
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while creating the record",
                    error = ex.Message
                });
            }
        }
        [HttpGet("api/edit/{agreementNumber}")]
        public async Task<IActionResult> Edit(string agreementNumber)
        {
            var bpkb = await iBpkb.GetBpkbByAgreementNumberAsync(agreementNumber);

            if (bpkb == null)
            {
                return NotFound();
            }

            var model = new tr_bpkb
            {
                AgreementNumber = bpkb.AgreementNumber,
                BpkbNo = bpkb.BpkbNo,
                BranchId = bpkb.BranchId,
                BpkbDate = bpkb.BpkbDate,
                FakturNo = bpkb.FakturNo,
                FakturDate = bpkb.FakturDate,
                LocationId = bpkb.LocationId,
                PoliceNo = bpkb.PoliceNo,
                BpkbDateIn = bpkb.BpkbDateIn,
                CreatedBy = bpkb.CreatedBy,
                CreatedOn = bpkb.CreatedOn,
                LastUpdatedBy = bpkb.LastUpdatedBy,
                LastUpdatedOn = bpkb.LastUpdatedOn
            };

            return Ok(model);
        }

        [HttpPut("api/edit/{agreementNumber}")]
        public async Task<IActionResult> Edit(string agreementNumber, [FromBody] tr_bpkb model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBpkb = await iBpkb.GetBpkbByAgreementNumberAsync(agreementNumber);
            if (existingBpkb == null)
            {
                return NotFound();
            }

            existingBpkb.BpkbNo = model.BpkbNo;
            existingBpkb.BranchId = model.BranchId;
            existingBpkb.BpkbDate = model.BpkbDate;
            existingBpkb.FakturNo = model.FakturNo;
            existingBpkb.FakturDate = model.FakturDate;
            existingBpkb.LocationId = model.LocationId;
            existingBpkb.PoliceNo = model.PoliceNo;
            existingBpkb.BpkbDateIn = model.BpkbDateIn;
            existingBpkb.LastUpdatedBy = model.LastUpdatedBy;
            existingBpkb.LastUpdatedOn = model.LastUpdatedOn;

            await iBpkb.UpdateBpkbAsync(existingBpkb);

            return NoContent();
        }
        [HttpDelete("api/delete/{agreementNumber}")]
        public async Task<IActionResult> DeleteBpkb(string agreementNumber)
        {
            if (string.IsNullOrEmpty(agreementNumber))
            {
                return BadRequest("Agreement number is required.");
            }
            var result = await iBpkb.DeleteBpkbAsync(agreementNumber);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

    }
}
