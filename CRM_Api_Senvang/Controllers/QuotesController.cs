using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;
using CRM_Api_Senvang.Repositories.Quotes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesRepository _quotesRepository;


        public QuotesController(IQuotesRepository quotesRepository)
        {
            _quotesRepository = quotesRepository;

        }

        [HttpGet("/api/qoutes/deal")]
        [Authorize]
        public IActionResult GetQuotesDeal()
        {

            return Ok(_quotesRepository.GetQuotesDeal().HandleResponse());
        }

        [HttpPost("/api/qoutes/detail")]
        [Authorize]
        public IActionResult GetQuotesDetail(Quotes quotes)
        {

            return Ok(_quotesRepository.GetQuotesDetail(quotes.QuotesId).HandleResponse());
        }

        [HttpPost("/api/qoutes/task")]
        [Authorize]
        public IActionResult GetQuotesTask(Deal deal)
        {

            return Ok(_quotesRepository.GetTaskByOfQuotes(deal.DealId).HandleResponse());
        }
    }
}
