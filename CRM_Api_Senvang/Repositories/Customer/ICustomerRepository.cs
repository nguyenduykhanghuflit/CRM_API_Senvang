using CRM_Api_Senvang.Helpers;

namespace CRM_Api_Senvang.Repositories.Customer
{
    public interface ICustomerRepository
    {

        int CreateCustomer(string custName, string phone, string email, string username);

        ResponseHelper FindCustomer(string input);
    }
}
