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
        public int? SaleMenId { get; set; }
        public string? Notes { get; set; }

    }

    public class UpdateDealDto
    {
        public int Id { get; set; }
        public int? OppTypeId { get; set; }
        public string? Title { get; set; }
        public DateTime? Openning { get; set; }
        public DateTime? DeployDate { get; set; }
        public string? Notes { get; set; }
        public int? SaleMenId { get; set; }

    }

    public class NewQuotesDto
    {
        public int DealId { get; set; }
        public int? HallId { get; set; }
        public int? GuestQty { get; set; }
        public int? TableQty { get; set; }
        public int? Deposits { get; set; }

    }

    public class UpdateQuotesDto
    {
        public int Id { get; set; }
        public int? HallId { get; set; }
        public int? GuestQty { get; set; }
        public int? TableQty { get; set; }
        public int? Deposits { get; set; }

    }

    public class UpdateQuotesStatus : Quotes
    {
        public int StatusId { get; set; }
    }

    public class UpdateDealStatus : Deal
    {
        public int StatusId { get; set; }
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

    public class TaskDto
    {
        public int TaskId { get; set; }
    }

    public class NewTaskDto
    {
        public int QuotesId { get; set; }

        public string TaskName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AssignToUser { get; set; }

        public string Notes { get; set; }

        public int TaskType { get; set; }

        public int Priority { get; set; }
    }

    public class UpdateTaskStatusOrUserDto : TaskDto
    {
        public int? AssignToUser { get; set; }

        public int? Status { get; set; }
    }

    public class UpdateTaskProgressOrUserDto : TaskDto
    {
        public int? Progress { get; set; }

        public int? Status { get; set; }
    }

}
