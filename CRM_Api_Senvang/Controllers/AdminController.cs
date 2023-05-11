using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;
using CRM_Api_Senvang.Repositories.Deal;
using CRM_Api_Senvang.Repositories.Quotes;
using CRM_Api_Senvang.Repositories.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IDealRepository _dealRepository;
        private readonly IQuotesRepository _quotesRepository;
        private readonly ITask _taskRepository;
        private readonly TokenHelper _tokenHelper;

        public AdminController(ITask taskRepository, IDealRepository dealRepository, IQuotesRepository quotesRepository, TokenHelper tokenHelper)
        {
            _taskRepository = taskRepository;
            _dealRepository = dealRepository;
            _quotesRepository = quotesRepository;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("/api/admin/deal/status")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult UpdateDealStatus(UpdateDealStatus deal)
        {
            string username = _tokenHelper.GetUsername(HttpContext);
            return Ok(_dealRepository.UpdateDealStatus(deal.DealId, deal.StatusId, username).HandleResponse());
        }

        [HttpPost("/api/admin/quotes/status")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult UpdateQuotesStatus(UpdateQuotesStatus quotesDto)
        {
            string username = _tokenHelper.GetUsername(HttpContext);

            return Ok(_quotesRepository.UpdateQuotesStatus(quotesDto.QuotesId, quotesDto.StatusId, username).HandleResponse());
        }

        [HttpPost("/api/admin/task/create")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AdminCreateTask(List<NewTaskDto> listTask)
        {
            string username = _tokenHelper.GetUsername(HttpContext);

            List<string> createTaskError = new();
            List<dynamic> createTaskSuccess = new();
            listTask.ForEach(item =>
            {
                var task = _taskRepository.AdminCreateTask(item, username);
                if (task.ErrCode < 0)
                {
                    string msg = $"{task.Message}, Taskname: {item.TaskName}, AssignToUser: {item.AssignToUser} ";
                    createTaskError.Add(msg);
                }
                else
                {
                    createTaskSuccess.Add(task.Data);
                }

            });
            Dictionary<string, dynamic> result = new()
            {
                { "CreateTaskError", createTaskError },
                { "CreateTaskSuccess", createTaskSuccess }
            };
            int errCode = createTaskError.Count > 0 ? 1 : 0;
            ResponseHelper responseHelper = new(errCode, "Success", result);
            return Ok(responseHelper.HandleResponse());

        }

        [HttpPost("/api/admin/task/update")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AdminUpdateUserOrStatus(UpdateTaskStatusOrUserDto statusOrUserDto)
        {
            string username = _tokenHelper.GetUsername(HttpContext);
            return Ok(_taskRepository.AdminUpdateUserOrStatus(statusOrUserDto, username));
        }

        [HttpPost("/api/admin/task/confirm")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AdminConfirmSuccess(TaskDto taskDto)
        {
            string username = _tokenHelper.GetUsername(HttpContext);
            return Ok(_taskRepository.AdminConfirmSuccess(taskDto, username));
        }

    }
}
