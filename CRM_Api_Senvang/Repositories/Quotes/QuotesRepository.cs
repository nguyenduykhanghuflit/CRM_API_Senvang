using CRM_Api_Senvang.Database;
using CRM_Api_Senvang.Helpers;
using System.Data.SqlClient;
using System.Data;
using CRM_Api_Senvang.Models;

namespace CRM_Api_Senvang.Repositories.Quotes
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly SqlHelper sqlHelper;
        private readonly Utils utils;
        private readonly DatabaseManager databaseManager;

        public QuotesRepository(SqlHelper sqlHelper, Utils utils, DatabaseManager databaseManager)
        {
            this.sqlHelper = sqlHelper;
            this.utils = utils;
            this.databaseManager = databaseManager;

        }

        public ResponseHelper GetQuotesDeal(QueryParam queryParam, string username)
        {
            string sqlQuery = "khangGetDealQuotes";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@username", value: username),
                new SqlParameter(parameterName: "@PageNumber", value: queryParam.PageNumber),
                new SqlParameter(parameterName: "@PageSize", value: queryParam.PageSize),
                new SqlParameter(parameterName: "@StartDate", value: queryParam.StartDate),
                new SqlParameter(parameterName: "@EndDate", value: queryParam.EndDate)
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone queryRespone = utils.Query(sqlQuery, commandType, parameters.ToArray());

            return queryRespone.HandleQueryResponese();

        }

        public ResponseHelper GetQuotesDetail(int quotesId)
        {
            string sqlQuery = "khangGetQuotesByQuotesId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@quotesId", value: quotesId)
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone quotes = utils.Query(sqlQuery, commandType, parameters.ToArray());


            List<dynamic>? quotesData = (List<dynamic>?)quotes.Data;
            if (quotesData != null)
            {
                foreach (var item in quotesData)
                {
                    List<SqlParameter> parametersDetail = new()
                     {
                         new SqlParameter(parameterName: "@quotesId", value: item["QuotesId"])
                     };

                    QueryRespone quotesDetail = utils.Query("khangGetQuotesDetail", CommandType.StoredProcedure, parametersDetail.ToArray());
                    item["Detail"] = quotesDetail.Data;
                }

                return quotes.HandleQueryResponese();
            }

            else
            {
                return quotes.HandleQueryResponese();
            }



        }

        public ResponseHelper GetTaskByOfQuotes(int dealId)
        {
            string sqlQuery = "khangGetTaskQuotesByDealId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@DealId", value: dealId)
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone quotesTask = utils.Query(sqlQuery, commandType, parameters.ToArray());
            return quotesTask.HandleQueryResponese();
        }

        public ResponseHelper CreateQuotes(NewQuotesDto quotesDto, string username)
        {
            string sqlQuery = "khangCreateQuotesByDeal";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@username", value: username),
                new SqlParameter(parameterName: "@DealId", value: quotesDto.DealId),
                new SqlParameter(parameterName: "@HallId", value: quotesDto.HallId),
                new SqlParameter(parameterName: "@GuestQty", value: quotesDto.GuestQty),
                new SqlParameter(parameterName: "@TableQty", value: quotesDto.TableQty),
                new SqlParameter(parameterName: "@Deposits", value: quotesDto.Deposits),

            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone respone = utils.Query(sqlQuery, commandType, parameters.ToArray());



            return respone.HandleQueryResponese();
        }

        public ResponseHelper UpdateQuotes(UpdateQuotesDto quotesDto, string username)
        {
            string sqlQuery = "khangUpdateQuotes";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@Id", value: quotesDto.Id),
              new SqlParameter(parameterName: "@HallId", value: quotesDto.HallId),
                new SqlParameter(parameterName: "@GuestQty", value: quotesDto.GuestQty),
                new SqlParameter(parameterName: "@TableQty", value: quotesDto.TableQty),
                new SqlParameter(parameterName: "@Deposits", value: quotesDto.Deposits),
                new SqlParameter(parameterName: "@username", value: username),
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone respone = utils.Query(sqlQuery, commandType, parameters.ToArray());



            return respone.HandleQueryResponese();
        }

        public ResponseHelper UpdateQuotesStatus(int quotesId, int statusId, string username)
        {
            string sqlQuery = "khangUpdateQuotesStatus";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@quotesId", value: quotesId),
               new SqlParameter(parameterName: "@statusId", value: statusId),
                new SqlParameter(parameterName: "@username", value: username),
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone respone = utils.Query(sqlQuery, commandType, parameters.ToArray());



            return respone.HandleQueryResponese();
        }

        public ResponseHelper DeleteQuotes(int quotesId, string username)
        {
            string sqlQuery = "khangDeleteQuotes";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@quotesId", value: quotesId),
                new SqlParameter(parameterName: "@username", value: username),
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone respone = utils.Query(sqlQuery, commandType, parameters.ToArray());



            return respone.HandleQueryResponese();
        }
    }
}
