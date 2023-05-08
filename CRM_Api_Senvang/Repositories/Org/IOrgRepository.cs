using CRM_Api_Senvang.Helpers;

namespace CRM_Api_Senvang.Repositories.Org
{
    public interface IOrgRepository
    {
        ResponseHelper GetEmployeese();
        ResponseHelper GetOrg();
        ResponseHelper OrgConfig(string username, int orgId);
    }
}
