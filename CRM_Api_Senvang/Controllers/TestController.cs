using CRM_Api_Senvang.Database;
using CRM_Api_Senvang.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using Quartz.Util;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Configuration;

using Microsoft.AspNetCore.Authorization;
using System.Numerics;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;

using SqlParameter = System.Data.SqlClient.SqlParameter;
using CRM_Api_Senvang.Models;

namespace CRM_Api_Senvang.Controllers.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly SqlHelper sqlHelper;
        private readonly TokenHelper tokenHelper;

        public TestController(SqlHelper sqlHelper, TokenHelper tokenHelper)
        {
            this.sqlHelper = sqlHelper;
            this.tokenHelper = tokenHelper;
        }

        // TEST SERVER
        [HttpGet("/")]
        public IActionResult Ping()
        {
            //string username = tokenHelper.GetUsername(HttpContext);
            return Ok("Oke server");
        }


        // DEAL
        [HttpGet("/api/internship/getdeals")]
        [Authorize(Policy = "AdminPolicy")]
        // [Authorize(Policy = "UserPolicy")]
        public IActionResult GetDeals(int? pageNumber, string? filterType, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                int pageSize = 15;
                if (pageNumber == null || pageNumber <= 0)
                {
                    pageNumber = 1;
                }

                string sqlQuery = "khangGetDeal";
                List<SqlParameter> sqlParameters = new()
                    {
                        new SqlParameter() {ParameterName= "@PageNumber", Value =pageNumber},
                        new SqlParameter() {ParameterName= "@PageSize",Value =pageSize,},
                        new SqlParameter() {ParameterName= "@Type",Value =filterType,},
                        new SqlParameter() {ParameterName= "@StartDate",Value =startDate,},
                        new SqlParameter() {ParameterName= "@EndDate",Value =endDate,},
                    };

                var commandType = CommandType.StoredProcedure;


                var response = sqlHelper.HandleReadData(sqlQuery, commandType, sqlParameters.ToArray());

                if (!response.ContainsKey("Error"))
                {
                    ResponseHelper responseHepler = new(0, "Success", response["Data"]);
                    return Ok(responseHepler.HandleResponse());
                }
                else
                {
                    ResponseHelper responseHepler = new(-1, response["Error"], null);
                    return Ok(responseHepler.HandleResponse());
                }

            }
            catch (Exception ex)
            {
                ResponseHelper responseHepler = new(-1, "Server Error: " + ex.Message, null);
                return Ok(responseHepler.HandleResponse());
            }


        }

    }






}
