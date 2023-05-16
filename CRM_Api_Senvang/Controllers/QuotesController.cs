using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;
using CRM_Api_Senvang.Repositories.Customer;
using CRM_Api_Senvang.Repositories.Deal;
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
        private readonly TokenHelper _tokenHelper;

        public QuotesController(IQuotesRepository quotesRepository, TokenHelper tokenHelper)
        {
            _quotesRepository = quotesRepository;
            _tokenHelper = tokenHelper;

        }

        [HttpPost("/api/quotes/user")]
        [Authorize]
        public IActionResult GetQuotesByUser(QueryParam queryParam)
        {
            string username = _tokenHelper.GetUsername(HttpContext);

            if (queryParam.PageSize > 20 || queryParam.PageSize < 1)
            {
                queryParam.PageSize = 20;
            }

            if (queryParam.PageNumber < 1)
            {
                queryParam.PageNumber = 1;
            }
            return Ok(_quotesRepository.GetQuotesDeal(queryParam, username).HandleResponse());
        }

        [HttpPost("/api/quotes/assign/user")]
        [Authorize]
        public IActionResult GetQuotesAssginToUser(QueryParam queryParam)
        {
            string username = _tokenHelper.GetUsername(HttpContext);

            if (queryParam.PageSize > 20 || queryParam.PageSize < 1)
            {
                queryParam.PageSize = 20;
            }

            if (queryParam.PageNumber < 1)
            {
                queryParam.PageNumber = 1;
            }
            return Ok(_quotesRepository.GetQuotesAssignByUser(queryParam, username).HandleResponse());
        }

        [HttpPost("/api/quotes/detail")]
        [Authorize]
        public IActionResult GetQuotesDetail(Quotes quotes)
        {

            return Ok(_quotesRepository.GetQuotesDetail(quotes.QuotesId).HandleResponse());
        }

        [HttpPost("/api/quotes/task")]
        [Authorize]
        public IActionResult GetQuotesTask(Deal deal)
        {

            return Ok(_quotesRepository.GetTaskByOfQuotes(deal.DealId).HandleResponse());
        }

        [HttpPost("/api/quotes/create")]
        [Authorize]
        public IActionResult CreateQuotes(NewQuotesDto quotesDto)
        {
            string username = _tokenHelper.GetUsername(HttpContext);

            return Ok(_quotesRepository.CreateQuotes(quotesDto, username).HandleResponse());
        }

        [HttpPost("/api/quotes/update")]
        [Authorize]
        public IActionResult UpdateQuotes(UpdateQuotesDto quotesDto)
        {
            string username = _tokenHelper.GetUsername(HttpContext);

            return Ok(_quotesRepository.UpdateQuotes(quotesDto, username).HandleResponse());
        }





        [HttpPost("/api/quotes/delete")]
        [Authorize]
        public IActionResult DeleteQuotes(Quotes quotes)
        {
            string username = _tokenHelper.GetUsername(HttpContext);
            return Ok(_quotesRepository.DeleteQuotes(quotes.QuotesId, username).HandleResponse());
        }
    }
}
