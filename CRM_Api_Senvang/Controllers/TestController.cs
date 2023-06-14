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
using Nest;
using KmlToGeoJson;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace CRM_Api_Senvang.Controllers.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly SqlHelper sqlHelper;
        private readonly TokenHelper tokenHelper;
        private readonly Utils utils;

        public TestController(SqlHelper sqlHelper, TokenHelper tokenHelper, Utils utils)
        {
            this.sqlHelper = sqlHelper;
            this.tokenHelper = tokenHelper;
            this.utils = utils;
        }

        // TEST SERVER
        [HttpGet("/")]
        public IActionResult Ping()
        {
            //string username = tokenHelper.GetUsername(HttpContext);
            return Ok("Oke server");
        }

        [HttpGet("/location")]
        public IActionResult DistanceBetweenPlaces(double lat2, double lon2)
        {
            double lat1 = 10.7651521, lon1 = 106.7066008;
            /*    a = sin²(Δlat/2) + cos(lat1).cos(lat2).sin²(Δlong/2)
                  c = 2.atan2(√a, √(1−a))
                  d = R.c*/
            const double RADIUS = 6378.16; //bán kính trung bình trái đất
            double dlon = utils.Radians(lon2 - lon1);
            double dlat = utils.Radians(lat2 - lat1);

            double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) +
                     (Math.Cos(utils.Radians(lat1)) * Math.Cos(utils.Radians(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2)));

            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = (angle * RADIUS);
            return Ok((distance * 1000));

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


        [HttpGet("/api/kml")]
        public IActionResult GetKml()
        {
            var xml = XDocument.Load(@"G:\Desktop\gls\gls research\week8\CRM_Api_Senvang\CRM_Api_Senvang\Files\TradeZone.kml");
            return Ok(xml.ToString());

        }


        [HttpPost("/api/upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "TradeZone.kml");
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                return Ok(new { length = file.Length, name = "TradeZone.kml" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }


}







