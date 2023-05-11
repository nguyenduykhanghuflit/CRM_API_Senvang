using CRM_Api_Senvang.Models;
using Newtonsoft.Json;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Data.SqlClient;
using System.Data;

namespace CRM_Api_Senvang.Helpers
{
    public class Utils
    {
        private readonly SqlHelper sqlHelper;



        public Utils(SqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;

        }

        public string XmlToJsonString(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var jsonTop10 = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.None, true);
            return jsonTop10.Replace("\"", "\'").Replace("\n", " ");
        }


        public bool LoginInfoValid(string username, string password)
        {
            return username.Length <= 20 && password.Length <= 10;
        }
        public string HashPasswordSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToHexString(hashValue);
            }
        }


        public QueryRespone Query(string sqlQuery, CommandType commandType = CommandType.StoredProcedure,
            params SqlParameter[]? sqlParameters)
        {
            try
            {

                var response = sqlHelper.HandleReadData(sqlQuery, commandType, sqlParameters);

                if (!response.ContainsKey("Error"))
                {
                    return new QueryRespone(false, "Success", response["Data"]);

                }
                else
                {
                    return new QueryRespone(true, response["Error"], null);

                }

            }
            catch (Exception ex)
            {

                return new QueryRespone(true, "Query error: " + ex.Message, "");

            }
        }


    }

    public class QueryRespone
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public QueryRespone(bool Error, string Message, dynamic Data)
        {

            this.Error = Error;
            this.Message = Message;
            this.Data = Data;
        }

        public ResponseHelper HandleQueryResponese()
        {
            if (this.Error)
            {
                return new ResponseHelper(-1, this.Message, null);
            }
            else
            {
                return new ResponseHelper(0, this.Message, this.Data);
            }
        }

    }
}
