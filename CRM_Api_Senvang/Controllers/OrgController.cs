using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;
using CRM_Api_Senvang.Repositories.Org;
using CRM_Api_Senvang.Repositories.Quotes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgController : ControllerBase
    {

        private readonly IOrgRepository _orgRepository;
        private readonly TokenHelper _tokenHelper;


        public OrgController(IOrgRepository orgRepository, TokenHelper tokenHelper)
        {
            _orgRepository = orgRepository;
            _tokenHelper = tokenHelper;

        }

        [HttpGet("/api/org/employees")]
        [Authorize]
        public IActionResult GetEmployeese()
        {

            return Ok(_orgRepository.GetEmployeese().HandleResponse());
        }


        [HttpGet("/api/org/list")]
        [Authorize]
        public IActionResult GetOrg()
        {
            return Ok(_orgRepository.GetOrg().HandleResponse());
        }

        [HttpPost("/api/org/config")]
        [Authorize]
        public IActionResult ConfigOrdForUser(PubObj pubObj)
        {

            var orgList = _orgRepository.GetOrg();
            var data = orgList.Data as List<dynamic>;
            var find = data?.Find(item => item["OBJ_AUTOID"] == pubObj.autoId);
            ResponseHelper response = new ResponseHelper(-1, "Obj invalid", null);
            if (find == null) return Ok(response.HandleResponse());

            string username = _tokenHelper.GetUsername(HttpContext);
            return Ok(_orgRepository.OrgConfig(username, pubObj.autoId).HandleResponse());

        }

    }
}
