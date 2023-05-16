using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;

namespace CRM_Api_Senvang.Repositories.Quotes
{
    public interface IQuotesRepository
    {
        ResponseHelper GetQuotesDeal(QueryParam queryParam, string username);

        ResponseHelper GetQuotesAssignByUser(QueryParam queryParam, string username);

        ResponseHelper AdminGetAllQuotes(QueryParam queryParam, string username);

        ResponseHelper GetQuotesDetail(int quotesId);

        ResponseHelper GetTaskByOfQuotes(int dealId);

        ResponseHelper CreateQuotes(NewQuotesDto deal, string username);

        ResponseHelper UpdateQuotes(UpdateQuotesDto deal, string username);

        ResponseHelper UpdateQuotesStatus(int quotesId, int statusId, string username);

        ResponseHelper DeleteQuotes(int quotesId, string username);

    }
}
