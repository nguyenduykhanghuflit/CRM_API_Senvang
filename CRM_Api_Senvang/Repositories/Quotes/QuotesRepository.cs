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

        public ResponseHelper GetQuotesDeal()
        {
            string sqlQuery = "khangGetDealQuotes";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();

        }

        public ResponseHelper GetQuotesDetail(int quotesId)
        {
            string sqlQuery = "khangGetQuotesByQuotesId";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@quotesId", value: quotesId)
            };
            QueryRespone quotes = utils.Query(sqlQuery, CommandType.StoredProcedure, parameters.ToArray());

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
            QueryRespone quotesTask = utils.Query(sqlQuery, CommandType.StoredProcedure, parameters.ToArray());
            return quotesTask.HandleQueryResponese();
        }


    }
}
