using CRM_Api_Senvang.Database;
using CRM_Api_Senvang.Helpers;

namespace CRM_Api_Senvang.Repositories.Statuses
{
    public class StatusesRepository : IStatusesRepository
    {
        private readonly SqlHelper sqlHelper;
        private readonly Utils utils;
        private readonly DatabaseManager databaseManager;

        public StatusesRepository(SqlHelper sqlHelper, Utils utils, DatabaseManager databaseManager)
        {
            this.sqlHelper = sqlHelper;
            this.utils = utils;
            this.databaseManager = databaseManager;

        }

        public ResponseHelper GetDealStatus()
        {
            string sqlQuery = "khangGetDealStatus";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();
        }

        public ResponseHelper GetHall()
        {
            string sqlQuery = "khangGetHall";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();
        }

        public ResponseHelper GetOppType()
        {
            string sqlQuery = "khangGetOppType";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();
        }

        public ResponseHelper GetPriority()
        {
            string sqlQuery = "khangGetPriority";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();
        }

        public ResponseHelper GetQuotesStatus()
        {
            string sqlQuery = "khangGetQuotesStatus";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();
        }

        public ResponseHelper GetTaskStatus()
        {
            string sqlQuery = "khangGetTaskStatus";
            QueryRespone queryRespone = utils.Query(sqlQuery);

            return queryRespone.HandleQueryResponese();
        }
    }
}
