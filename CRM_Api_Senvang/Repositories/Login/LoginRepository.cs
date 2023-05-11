using CRM_Api_Senvang.Helpers;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Printing;
using System.Text;

namespace CRM_Api_Senvang.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SqlHelper sqlHelper;
        private readonly Utils utils;
        private readonly TokenHelper tokenHelper;
        public LoginRepository(SqlHelper sqlHelper, Utils utils, TokenHelper tokenHelper)
        {
            this.sqlHelper = sqlHelper;
            this.utils = utils;
            this.tokenHelper = tokenHelper;
        }
        public bool CheckLogin(string username, string password)
        {

            List<SqlParameter> sqlParameters = new()
                    {
                        new SqlParameter() {ParameterName= "@UserName", Value =username},
                        new SqlParameter() {ParameterName= "@Passencrypt",
                           Value=utils.HashPasswordSHA256(password) }
                    };

            var commandType = CommandType.StoredProcedure;
            var response = sqlHelper.HandleReadData("khangCheckLogin", commandType, sqlParameters.ToArray());

            if (response.ContainsKey("Error"))
            {
                throw new Exception(response["Error"]);
            }
            else
            {
                int check = (int)response["Data"][0]["result"];
                return check == 1;
            }

        }

        public Dictionary<string, string> Login(string username)
        {

            string role = "user";
            List<SqlParameter> sqlParameters = new()
                    {
                        new SqlParameter() {ParameterName= "@UserName", Value =username},

                    };

            var commandType = CommandType.StoredProcedure;
            var response = sqlHelper.HandleReadData("khangGetRole", commandType, sqlParameters.ToArray());

            if (response.ContainsKey("Error"))
            {
                throw new Exception(response["Error"]);
            }
            else
            {
                bool roleRes = (bool)response["Data"][0]["Role"];
                role = roleRes ? "admin" : "user";
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            result["AccessToken"] = tokenHelper.GenerateToken(username, role);
            result["Role"] = role;
            return result;



        }
    }
}
