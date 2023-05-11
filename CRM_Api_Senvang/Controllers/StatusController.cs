using CRM_Api_Senvang.Repositories.Quotes;
using CRM_Api_Senvang.Repositories.Statuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        private readonly IStatusesRepository _statusesRepository;


        public StatusController(IStatusesRepository statusesRepository)
        {
            _statusesRepository = statusesRepository;
        }

        [HttpGet("/api/status/deal")]
        [Authorize]
        public IActionResult GetDealStatus()
        {

            return Ok(_statusesRepository.GetDealStatus().HandleResponse());
        }



        [HttpGet("/api/status/quotes")]
        [Authorize]
        public IActionResult GetQuotesStatus()
        {

            return Ok(_statusesRepository.GetQuotesStatus().HandleResponse());
        }




        [HttpGet("/api/status/task")]
        [Authorize]
        public IActionResult GetTaskStatus()
        {

            return Ok(_statusesRepository.GetTaskStatus().HandleResponse());
        }


        [HttpGet("/api/priority")]
        [Authorize]
        public IActionResult GetPriority()
        {
            return Ok(_statusesRepository.GetPriority().HandleResponse());
        }

        [HttpGet("/api/oppotype")]
        [Authorize]
        public IActionResult GetOppType()
        {
            return Ok(_statusesRepository.GetOppType().HandleResponse());
        }

        [HttpGet("/api/hall")]
        [Authorize]
        public IActionResult GetHall()
        {
            return Ok(_statusesRepository.GetHall().HandleResponse());
        }
    }
}
