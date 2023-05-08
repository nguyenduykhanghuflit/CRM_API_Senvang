namespace CRM_Api_Senvang.Repositories.Login
{
    public interface ILoginRepository
    {
        Dictionary<string, string> Login(string username);
        bool CheckLogin(string username, string password);
    }
}
