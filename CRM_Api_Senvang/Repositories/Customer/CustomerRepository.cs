using CRM_Api_Senvang.Helpers;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using CRM_Api_Senvang.Database;
using CRM_Api_Senvang.Models;

namespace CRM_Api_Senvang.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly SqlHelper sqlHelper;
        private readonly Utils utils;
        private readonly DatabaseManager databaseManager;


        public CustomerRepository(SqlHelper sqlHelper, Utils utils, DatabaseManager databaseManager)
        {
            this.sqlHelper = sqlHelper;
            this.utils = utils;
            this.databaseManager = databaseManager;

        }

        public int CreateCustomer(string custName, string phone, string email, string username)
        {

            try
            {
                List<SqlParameter> sqlParameters = new()
                    {
                        new SqlParameter() {ParameterName= "@CustName", Value =custName},
                        new SqlParameter() {ParameterName= "@Phone",Value=phone },
                        new SqlParameter() {ParameterName= "@Email",Value=email },
                        new SqlParameter() {ParameterName= "@username",Value=username }
                    };

                var commandType = CommandType.StoredProcedure;
                databaseManager.OpenConnection();
                List<int> autoIds = new List<int>();
                using (var command = databaseManager.CreateCommand("khangspCreateCustomer", commandType, sqlParameters.ToArray()))
                {
                    using SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int autoId = (int)reader["autoId"];
                        autoIds.Add(autoId);
                    }

                }

                databaseManager.CloseConnection();
                return autoIds[0];
            }
            catch (Exception ex)
            {
                throw new Exception("Create customer error: {0}", ex);
            }
        }

        public ResponseHelper FindCustomer(string input)
        {
            string sqlQuery = "khangFindCustomerByPhoneOrId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@input", value: input)
            };

            var commandType = CommandType.StoredProcedure;
            QueryRespone customer = utils.Query(sqlQuery, commandType, parameters.ToArray());
            return customer.HandleQueryResponese();

        }
    }
}
