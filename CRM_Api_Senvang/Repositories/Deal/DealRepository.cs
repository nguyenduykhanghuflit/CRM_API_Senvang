using CRM_Api_Senvang.Helpers;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using CRM_Api_Senvang.Database;
using CRM_Api_Senvang.Models;

namespace CRM_Api_Senvang.Repositories.Deal
{
    public class DealRepository : IDealRepository
    {

        private readonly SqlHelper sqlHelper;
        private readonly Utils utils;
        private readonly DatabaseManager databaseManager;


        public DealRepository(SqlHelper sqlHelper, Utils utils, DatabaseManager databaseManager)
        {
            this.sqlHelper = sqlHelper;
            this.utils = utils;
            this.databaseManager = databaseManager;

        }

        public ResponseHelper CreateDeal(NewDealDto deal, string username)
        {
            string sqlQuery = "khangCreateDeal";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@OppTypeId", value: deal.OppTypeId),
                new SqlParameter(parameterName: "@Title", value: deal.Title),
                new SqlParameter(parameterName: "@ObjectID", value: deal.ObjectID),
                new SqlParameter(parameterName: "@CustName", value: deal.CustName),
                new SqlParameter(parameterName: "@Phone", value: deal.Phone),
                new SqlParameter(parameterName: "@Email", value: deal.Email),
                new SqlParameter(parameterName: "@username", value: username),
                new SqlParameter(parameterName: "@openning", value: deal.Openning),
                new SqlParameter(parameterName: "@deploydate", value: deal.DeployDate),
            };
            QueryRespone respone = utils.Query(sqlQuery, CommandType.StoredProcedure, parameters.ToArray());



            return respone.HandleQueryResponese();

        }

        public ResponseHelper UpdateDeal(UpdateDealDto deal, string username)
        {
            string sqlQuery = "khangUpdateDeal";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@Id", value: deal.Id),
                new SqlParameter(parameterName: "@Title", value: deal.Title),
                new SqlParameter(parameterName: "@OppTypeId", value: deal.OppTypeId),
                new SqlParameter(parameterName: "@openning", value: deal.Openning),
                new SqlParameter(parameterName: "@deploydate", value: deal.DeployDate),
                new SqlParameter(parameterName: "@Status", value: deal.StatusId),
                new SqlParameter(parameterName: "@Notes", value: deal.Notes),
                new SqlParameter(parameterName: "@username", value: username),
            };
            QueryRespone respone = utils.Query(sqlQuery, CommandType.StoredProcedure, parameters.ToArray());



            return respone.HandleQueryResponese();
        }

        public ResponseHelper GetDealCreateByUser(QueryParam queryParam, string username)
        {
            string sqlQuery = "khangGetDealByUsername";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@username", value: username),
                new SqlParameter(parameterName: "@PageNumber", value: queryParam.PageNumber),
                new SqlParameter(parameterName: "@PageSize", value: queryParam.PageSize),
                new SqlParameter(parameterName: "@StartDate", value: queryParam.StartDate),
                new SqlParameter(parameterName: "@EndDate", value: queryParam.EndDate)
            };
            QueryRespone deals = utils.Query(sqlQuery, CommandType.StoredProcedure, parameters.ToArray());
            return deals.HandleQueryResponese();
        }

        public ResponseHelper GetDealDetail(int dealId)
        {
            string sqlQuery = "khangGetDealDetail";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@Id", value: dealId),

            };
            QueryRespone deal = utils.Query(sqlQuery, CommandType.StoredProcedure, parameters.ToArray());
            return deal.HandleQueryResponese();
        }

        public ResponseHelper DeleteDeal(int dealId, string username)
        {
            throw new NotImplementedException();
        }

    }
}
