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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDealRepository _dealRepository;
        private readonly TokenHelper _tokenHelper;


        public CustomerController(ICustomerRepository customerRepository, IDealRepository dealRepository, TokenHelper tokenHelper)
        {
            _customerRepository = customerRepository;
            _dealRepository = dealRepository;
            _tokenHelper = tokenHelper;

        }


        [HttpPost("/api/customer/find")]
        [Authorize]
        public IActionResult FindCustomerByPhoneOrId(FindCustomerDto customerDto)
        {

            return Ok(_customerRepository.FindCustomer(customerDto.info).HandleResponse());

        }
    }
}
