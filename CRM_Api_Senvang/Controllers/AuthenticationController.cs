using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;
using CRM_Api_Senvang.Repositories.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Utils utils;
        private readonly ILoginRepository loginRepository;
        public AuthenticationController(Utils utils, ILoginRepository loginRepository)
        {
            this.utils = utils;
            this.loginRepository = loginRepository;
        }


        [HttpPost("/api/auth/login")]
        public IActionResult Login(UserLoginDto userLogin)
        {
            ResponseHelper response;

            bool loginInfoValid = utils.LoginInfoValid(userLogin.Username, userLogin.Password);
            if (!loginInfoValid)
            {
                response = new ResponseHelper(-1, "Failed", "Username or password invalid");
                return Ok(response.HandleResponse());
            }

            bool loginValid = loginRepository.CheckLogin(userLogin.Username, userLogin.Password);
            if (!loginValid)
            {
                response = new ResponseHelper(-2, "Failed", "Username or password invalid");
                return Ok(response.HandleResponse());
            }

            var result = loginRepository.Login(userLogin.Username);

            //danh sách các Quotes được tạo bởi Deal của user đăng nhập trong ngày 
            // store username in firebase



            response = new ResponseHelper(1, "Success", result);
            return Ok(response.HandleResponse());

        }
    }
}
