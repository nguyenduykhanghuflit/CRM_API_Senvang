using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;
using CRM_Api_Senvang.Repositories.Statuses;
using CRM_Api_Senvang.Repositories.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITask _taskRepository;
        private readonly TokenHelper _tokenHelper;

        public TaskController(ITask taskRepository, TokenHelper tokenHelper)
        {
            _taskRepository = taskRepository;
            _tokenHelper = tokenHelper;
        }

        [HttpGet("/api/Task/TaskType")]
        [Authorize]
        public IActionResult GetTaskTypeList()
        {
            return Ok(_taskRepository.GetTaskTypeList().HandleResponse());
        }

        [HttpGet("/api/Task/List")]
        [Authorize]
        public IActionResult GetTaskList()
        {
            return Ok(_taskRepository.GetTaskList().HandleResponse());
        }

        [HttpPost("/api/Task/Detail")]
        [Authorize]
        public IActionResult GetTaskDetail(TaskDto taskDto)
        {
            return Ok(_taskRepository.GetTaskDetail(taskDto).HandleResponse());
        }



        [HttpPost("/api/user/task/update")]
        [Authorize]
        public IActionResult UserUpdateProgressOrStatus(UpdateTaskProgressOrUserDto progressOrUserDto)
        {
            string username = _tokenHelper.GetUsername(HttpContext);
            return Ok(_taskRepository.UserUpdateProgressOrStatus(progressOrUserDto, username));
        }

        [HttpPost("/api/task/assign/user")]
        [Authorize]
        public IActionResult GetDealAssginToUser(QueryParam queryParam)
        {

            string username = _tokenHelper.GetUsername(HttpContext);

            if (queryParam.PageSize > 20 || queryParam.PageSize < 1)
            {
                queryParam.PageSize = 20;
            }

            if (queryParam.PageNumber < 1)
            {
                queryParam.PageNumber = 1;
            }

            return Ok(_taskRepository.GetTaskAssignToUser(queryParam, username).HandleResponse());
        }


    }
}
