using CRM_Api_Senvang.Database;
using CRM_Api_Senvang.Helpers;
using System.Data.SqlClient;
using System.Data;
using CRM_Api_Senvang.Models;

namespace CRM_Api_Senvang.Repositories.Org
{
    public class OrgRepository : IOrgRepository
    {
        private readonly SqlHelper sqlHelper;
        private readonly Utils utils;
        private readonly DatabaseManager databaseManager;

        public OrgRepository(SqlHelper sqlHelper, Utils utils, DatabaseManager databaseManager)
        {
            this.sqlHelper = sqlHelper;
            this.utils = utils;
            this.databaseManager = databaseManager;

        }

        public ResponseHelper GetEmployeese()
        {
            string sqlQuery = "khangGetEmployees";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();
        }

        public ResponseHelper GetOrg()
        {
            string sqlQuery = "khangGetOrg";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();
        }

        public ResponseHelper OrgConfig(string username, int orgId)
        {

            string sqlQuery = "khangSetOrgForUser";
            List<SqlParameter> sqlParameters = new()
                    {
                        new SqlParameter() {ParameterName= "@username", Value =username},
                        new SqlParameter() {ParameterName= "@orgId",Value=orgId }
                    };
            var commandType = CommandType.StoredProcedure;
            QueryRespone quotesTask = utils.Query(sqlQuery, commandType, sqlParameters.ToArray());
            return quotesTask.HandleQueryResponese();
        }
    }
}
