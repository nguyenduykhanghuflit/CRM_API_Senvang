using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;
using CRM_Api_Senvang.Repositories.Customer;
using CRM_Api_Senvang.Repositories.Deal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IDealRepository _dealRepository;
        private readonly TokenHelper _tokenHelper;


        public DealController(ICustomerRepository customerRepository, IDealRepository dealRepository, TokenHelper tokenHelper)
        {
            _customerRepository = customerRepository;
            _dealRepository = dealRepository;
            _tokenHelper = tokenHelper;

        }


        [HttpPost("/api/deal/create")]
        [Authorize]
        public IActionResult CreateDeal(NewDealDto dealDto)
        {

            //bug tạo deal lỗi nhưng vẫn tạo khách hàng

            string username = _tokenHelper.GetUsername(HttpContext);

            if (dealDto.ObjectID == null || dealDto.ObjectID == 0)
            {
                int autoId = _customerRepository.CreateCustomer(dealDto.CustName, dealDto.Phone, dealDto.Email, username);

                dealDto.ObjectID = autoId;
            }

            DateTime currentDate = DateTime.Now;

            if (dealDto.Openning == null) dealDto.Openning = currentDate;

            if (dealDto.DeployDate == null) dealDto.DeployDate = currentDate;


            return Ok(_dealRepository.CreateDeal(dealDto, username).HandleResponse());


        }

        [HttpPost("/api/deal/update")]
        [Authorize]
        public IActionResult UpdateDeal(UpdateDealDto dealDto)
        {

            string username = _tokenHelper.GetUsername(HttpContext);

            return Ok(_dealRepository.UpdateDeal(dealDto, username).HandleResponse());
        }



        [HttpPost("/api/deal/user")]
        [Authorize]
        public IActionResult GetDealByUser(QueryParam queryParam)
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

            return Ok(_dealRepository.GetDealCreateByUser(queryParam, username).HandleResponse());
        }

        [HttpPost("/api/deal/detail")]
        [Authorize]
        public IActionResult GetDealDetail(Deal deal)
        {

            return Ok(_dealRepository.GetDealDetail(deal.DealId).HandleResponse());
        }
    }
}
