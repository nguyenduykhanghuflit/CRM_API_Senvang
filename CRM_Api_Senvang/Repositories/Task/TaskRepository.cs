using CRM_Api_Senvang.Helpers;
using CRM_Api_Senvang.Models;
using System.Data.SqlClient;
using System.Data;
using CRM_Api_Senvang.Database;

namespace CRM_Api_Senvang.Repositories.Task
{
    public class TaskRepository : ITask
    {
        private readonly SqlHelper sqlHelper;
        private readonly Utils utils;
        private readonly DatabaseManager databaseManager;

        public TaskRepository(SqlHelper sqlHelper, Utils utils, DatabaseManager databaseManager)
        {
            this.sqlHelper = sqlHelper;
            this.utils = utils;
            this.databaseManager = databaseManager;

        }

        public ResponseHelper AdminCreateTask(NewTaskDto taskDto, string username)
        {
            string sqlQuery = "khangCreateTaskForQuotes";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@QuotesId", value: taskDto.QuotesId),
                new SqlParameter(parameterName: "@TaskName", value: taskDto.TaskName),
                new SqlParameter(parameterName: "@StartDate", value: taskDto.StartDate),
                new SqlParameter(parameterName: "@EndDate", value: taskDto.EndDate),
                new SqlParameter(parameterName: "@AssignToUser", value: taskDto.AssignToUser),
                new SqlParameter(parameterName: "@Notes", value: taskDto.Notes),
                new SqlParameter(parameterName: "@TaskType", value: taskDto.TaskType),
                new SqlParameter(parameterName: "@Priority", value: taskDto.Priority),
                new SqlParameter(parameterName: "@username", value: username),
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone task = utils.Query(sqlQuery, commandType, parameters.ToArray());
            return task.HandleQueryResponese();
        }

        public ResponseHelper AdminUpdateUserOrStatus(UpdateTaskStatusOrUserDto statusOrUserDto, string username)
        {
            string sqlQuery = "khangAdminUpdateAssginUserAndStatusTask";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@TaskId", value: statusOrUserDto.TaskId),
                new SqlParameter(parameterName: "@AssignToUser", value: statusOrUserDto.AssignToUser),
                new SqlParameter(parameterName: "@Status", value: statusOrUserDto.Status),
                new SqlParameter(parameterName: "@username", value: username),
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone task = utils.Query(sqlQuery, commandType, parameters.ToArray());
            return task.HandleQueryResponese();
        }

        public ResponseHelper GetTaskDetail(TaskDto taskDto)
        {
            string sqlQuery = "khangGetTaskDetail";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@TaskId", value: taskDto.TaskId)

            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone task = utils.Query(sqlQuery, commandType, parameters.ToArray());
            return task.HandleQueryResponese();
        }

        public ResponseHelper GetTaskList()
        {
            string sqlQuery = "khangGetTaskBasic";
            var commandType = CommandType.StoredProcedure;
            QueryRespone task = utils.Query(sqlQuery, commandType);
            return task.HandleQueryResponese();
        }

        public ResponseHelper GetTaskTypeList()
        {
            string sqlQuery = "khangGetTaskType";
            var commandType = CommandType.StoredProcedure;
            QueryRespone taskType = utils.Query(sqlQuery, commandType);
            return taskType.HandleQueryResponese();
        }

        public ResponseHelper UserUpdateProgressOrStatus(UpdateTaskProgressOrUserDto progressOrUserDto, string username)
        {
            string sqlQuery = "khangUserUpdateTaskProgressAndStatus";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@TaskId", value: progressOrUserDto.TaskId),
                new SqlParameter(parameterName: "@Progress", value: progressOrUserDto.Progress),
                new SqlParameter(parameterName: "@Status", value: progressOrUserDto.Status),
                new SqlParameter(parameterName: "@username", value: username),
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone task = utils.Query(sqlQuery, commandType, parameters.ToArray());
            return task.HandleQueryResponese();
        }

        public ResponseHelper AdminConfirmSuccess(TaskDto taskDto, string username)
        {
            string sqlQuery = "khangAdminConFirmTask";
            List<SqlParameter> parameters = new()
            {
                new SqlParameter(parameterName: "@taskId", value: taskDto.TaskId),
                new SqlParameter(parameterName: "@username", value: username),
            };
            var commandType = CommandType.StoredProcedure;
            QueryRespone task = utils.Query(sqlQuery, commandType, parameters.ToArray());
            return task.HandleQueryResponese();
        }
    }
}
