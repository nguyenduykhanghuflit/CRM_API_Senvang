using System.ComponentModel.DataAnnotations;

namespace CRM_Api_Senvang.Models
{

    public class Quotes
    {

        public int QuotesId { get; set; }

    }
    public class Deal
    {

        public int DealId { get; set; }

    }

    public class User
    {

        public string Username { get; set; }

    }

    public class UserLoginDto : User
    {

        public string Password { get; set; }
    }

    public class PubObj
    {
        public int autoId { get; set; }
    }

    public class QuotesDto : Quotes
    {



    }

    public class NewDealDto
    {
        public int OppTypeId { get; set; }
        public string Title { get; set; }
        public int? ObjectID { get; set; }
        public string CustName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Openning { get; set; }
        public DateTime? DeployDate { get; set; }

    }

    public class UpdateDealDto
    {
        public int Id { get; set; }
        public int? OppTypeId { get; set; }
        public string? Title { get; set; }
        public DateTime? Openning { get; set; }
        public DateTime? DeployDate { get; set; }
        public int? StatusId { get; set; }
        public string? Notes { get; set; }

    }

    public class NewCustomer
    {

    }

    public class FindCustomerDto
    {
        public string info { get; set; }
    }

    public class QueryParam
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }


}
