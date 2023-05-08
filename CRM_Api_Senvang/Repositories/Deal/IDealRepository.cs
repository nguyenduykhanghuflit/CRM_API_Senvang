using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;

namespace CRM_Api_Senvang.Repositories.Deal
{
    public interface IDealRepository
    {

        ResponseHelper CreateDeal(NewDealDto deal, string username);

        ResponseHelper UpdateDeal(UpdateDealDto deal, string username);

        ResponseHelper GetDealCreateByUser(QueryParam queryParam, string username);

        ResponseHelper GetDealDetail(int dealId);

        ResponseHelper DeleteDeal(int dealId, string username);
    }
}
