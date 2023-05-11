using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;

namespace CRM_Api_Senvang.Repositories.Task
{
    public interface ITask
    {
        ResponseHelper GetTaskTypeList();

        ResponseHelper GetTaskList();

        ResponseHelper GetTaskDetail(TaskDto taskDto);

        ResponseHelper AdminCreateTask(NewTaskDto taskDto, string username);

        ResponseHelper AdminUpdateUserOrStatus(UpdateTaskStatusOrUserDto statusOrUserDto, string username);

        ResponseHelper UserUpdateProgressOrStatus(UpdateTaskProgressOrUserDto progressOrUserDto, string username);


    }
}
