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

        [HttpPost("/api/admin/task/create")]
        /*  [Authorize(Policy = "AdminPolicy")]*/
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

        [HttpPost("/api/user/task/update")]
        [Authorize]
        public IActionResult UserUpdateProgressOrStatus(UpdateTaskProgressOrUserDto progressOrUserDto)
        {
            string username = _tokenHelper.GetUsername(HttpContext);
            return Ok(_taskRepository.UserUpdateProgressOrStatus(progressOrUserDto, username));
        }


    }
}
