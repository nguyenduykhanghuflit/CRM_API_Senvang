using CRM_Api_Senvang.Helpers;

namespace CRM_Api_Senvang.Repositories.Statuses
{
    public interface IStatusesRepository
    {
        ResponseHelper GetQuotesStatus();
        ResponseHelper GetTaskStatus();
        ResponseHelper GetDealStatus();
        ResponseHelper GetPriority();
        ResponseHelper GetOppType();
    }
}
