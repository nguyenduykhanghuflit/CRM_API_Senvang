using CRM_Api_Senvang.Helpers;

namespace CRM_Api_Senvang.Repositories.Quotes
{
    public interface IQuotesRepository
    {
        ResponseHelper GetQuotesDeal();
        ResponseHelper GetQuotesDetail(int quotesId);
        ResponseHelper GetTaskByOfQuotes(int dealId);

    }
}
