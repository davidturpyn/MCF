using Azure;
using MCFBackend.Services.Interfaces;
using MCFBackend.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCFBackend.Controllers
{
    public class BpkbController : Controller
    {
        private readonly IBpkb iBpkb;
        public BpkbController(IBpkb iBpkb) => this.iBpkb = iBpkb;

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
    }
}
