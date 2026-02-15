using Improver.BusinessModel;
using Improver.BusinessServiceInterface;
using Improver.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Improver.BusinessServices
{
    public class TaskService :ITaskService
    {
        public readonly AppDbContext _dbContext;


        public TaskService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<TaskBOResponse> Create(TaskBORequest objBORequest)
        {
            TaskBOResponse newTask = new TaskBOResponse();
            try
            {
                TaskValidator validator = new TaskValidator();
                string validationMessage = validator.Taskchecker(objBORequest);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    newTask.IsSuccess = false;
                    newTask.Message = validationMessage;
                    //throw new ArgumentException(validationMessage);
                    return newTask;
                }
                //fetch taskstatus and taskpriorityid

                var priority = await _dbContext.Staticmiscvaluemap.Include(x => x.Staticmisctype).FirstOrDefaultAsync(x => x.Code == objBORequest.Taskpriority
                && x.Staticmisctype.Code == "TaskPriority");

                var status = await _dbContext.Staticmiscvaluemap.Include(x => x.Staticmisctype).FirstOrDefaultAsync(x => x.Code == objBORequest.Taskstatus
                && x.Staticmisctype.Code == "TaskStatus");

                if(priority == null || status == null)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = "Invalid TaskPriority or TaskStatus";
                    return newTask;
                }


                // 3 Create Task entity
                Tasks task = TaskBORequest.TaskCreate(objBORequest);

                task.Taskpriorityid = priority.Staticmiscvaluemapid;
                task.Taskstatusid = status.Staticmiscvaluemapid;

                var taskExist = _dbContext.Tasks.Where(c => c.Taskname == task.Taskname && c.Createdbyid == objBORequest.Createdbyid && c.Isdeleted == false && c.Taskdate == task.Taskdate).FirstOrDefault();
                if (taskExist != null)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = $"task with the same name already exists: {task.Taskname}";

                    return newTask;
                }
                _dbContext.Tasks.Add(task);
                await _dbContext.SaveChangesAsync();

                var savedTask = await _dbContext.Tasks
           .Include(t => t.Taskpriority)
           .Include(t => t.Taskstatus)
           .FirstAsync(t => t.Taskid == task.Taskid);

                newTask = TaskBOResponse.TaskCreate(savedTask);
                newTask.IsSuccess = true;
                return newTask;

            }
            catch (Exception ex)
            {
                newTask.IsSuccess = false;
                newTask.Message = $"task creation faliled : {ex.Message}";

                return newTask;
            }
        }

        public async Task<TaskBOResponse> Update(TaskBORequest objBORequest)
        {
            TaskBOResponse newTask = new TaskBOResponse();
            try
            {
                TaskValidator validator = new TaskValidator();
                string validationMessage = validator.Taskchecker(objBORequest);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    newTask.IsSuccess = false;
                    newTask.Message = validationMessage;
                    return newTask;
                }

                var taskObj = TaskBORequest.TaskCreate(objBORequest);

                // 🔹 Find existing category (belongs to user & not deleted)
                var existingTask = await _dbContext.Tasks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c =>
                        c.Taskid == taskObj.Taskid &&
                        c.Createdbyid == objBORequest.Createdbyid &&
                        c.Isdeleted == false &&
                        c.Taskdate == taskObj.Taskdate);

                if (existingTask == null)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = "task not found";
                    return newTask;
                }

                // 🔹 Duplicate name check (exclude current task)
                bool duplicateExists = await _dbContext.Tasks.AnyAsync(c =>
                    c.Taskname == taskObj.Taskname &&
                    c.Createdbyid == taskObj.Createdbyid &&
                    c.Isdeleted == false &&
                    c.Taskid != taskObj.Taskid &&
                    c.Taskdate == taskObj.Taskdate);

                if (duplicateExists)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = $"task with the same name already exists: {taskObj.Taskname}";
                    return newTask;
                }

                //fetch taskstatus and taskpriorityid

                var priority = await _dbContext.Staticmiscvaluemap.Include(x => x.Staticmisctype).FirstOrDefaultAsync(x => x.Code == objBORequest.Taskpriority
                && x.Staticmisctype.Code == "TaskPriority");

                var status = await _dbContext.Staticmiscvaluemap.Include(x => x.Staticmisctype).FirstOrDefaultAsync(x => x.Code == objBORequest.Taskstatus
                && x.Staticmisctype.Code == "TaskStatus");

                if (priority == null || status == null)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = "Invalid TaskPriority or TaskStatus";
                    return newTask;
                }


                // 3 Create Task entity
                Tasks task = TaskBORequest.TaskCreate(objBORequest);

                task.Taskpriorityid = priority.Staticmiscvaluemapid;
                task.Taskstatusid = status.Staticmiscvaluemapid;


                _dbContext.Tasks.Update(task);
                await _dbContext.SaveChangesAsync();

                var savedTask = await _dbContext.Tasks
          .Include(t => t.Taskpriority)
          .Include(t => t.Taskstatus)
          .FirstAsync(t => t.Taskid == task.Taskid);

                newTask = TaskBOResponse.TaskCreate(savedTask);
                newTask.IsSuccess = true;
                newTask.Message = "task updated successfully";

                return newTask;
            }
            catch (Exception ex)
            {
                newTask.IsSuccess = false;
                newTask.Message = $"task update failed: {ex.Message}";
                return newTask;
            }
        }

        public async Task<TaskBOResponse> GetById(int TaskId, int Createdbyid)
        {
            TaskBOResponse newTask = new TaskBOResponse();
            try
            {
                if (TaskId <= 0)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = $"Task with the taskid does not exists: {TaskId}";
                    return newTask;
                }

                var TaskData = _dbContext.Tasks.Where(c => c.Taskid == TaskId && c.Isdeleted == false && c.Createdbyid == Createdbyid).FirstOrDefault();
                if (TaskData == null)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = $"Task with the taskid does not exists: {TaskId}";
                    return newTask;
                }
                var savedTask = await _dbContext.Tasks
.Include(t => t.Taskpriority)
                .Include(t => t.Taskstatus)
.FirstAsync(t => t.Taskid == TaskId);

                newTask = TaskBOResponse.TaskCreate(savedTask);
                newTask.IsSuccess = true;
                newTask.Message = "Task Fetch successfully";

                return newTask;
            }
            catch (Exception ex)
            {
                newTask.IsSuccess = false;
                newTask.Message = $"task fetch failed: {ex.Message}";
                return newTask;
            }
        }

        public async Task<TaskListBOResponse> GetList(int CreatedbyId, DateOnly Taskdate)
        {
            TaskListBOResponse newTask = new TaskListBOResponse();
            try
            {


                var taskData = await _dbContext.Tasks.Where(c => c.Isdeleted == false && c.Createdbyid == CreatedbyId && c.Taskdate == Taskdate).ToListAsync();

                if (taskData == null)
                {
                    newTask.IsSuccess = true;
                    newTask.Message = "Create your New Categories";
                }

                newTask.Tasks = new List<TaskBOResponse>();
                foreach (var task in taskData)
                {
                    // Convert Category entity to CategoryBOResponse
                    var savedTask = await _dbContext.Tasks
.Include(t => t.Taskpriority)
            .Include(t => t.Taskstatus)
.FirstAsync(t => t.Taskid == task.Taskid);

                    TaskBOResponse taskResponse =
                        TaskBOResponse.TaskCreate(savedTask);

                    // Add converted object to list
                    newTask.Tasks.Add(taskResponse);
                }
                newTask.IsSuccess = true;
                newTask.Message = "Task Fetch successfully";

                return newTask;
            }
            catch (Exception ex)
            {
                newTask.IsSuccess = false;
                newTask.Message = $"Task fetch failed: {ex.Message}";
                return newTask;
            }
        }

        public async Task<TaskBOResponse> Delete(int TaskId, int Createdbyid)
        {
            TaskBOResponse newTask = new TaskBOResponse();
            try
            {
                if (TaskId <= 0)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = $"Category with the TaskId does not exists: {TaskId}";
                    return newTask;
                }

                var taskData = await _dbContext.Tasks.Where(c => c.Taskid == TaskId && c.Isdeleted == false && c.Createdbyid == Createdbyid).FirstOrDefaultAsync();
                if (taskData == null)
                {
                    newTask.IsSuccess = false;
                    newTask.Message = $"Task with the TaskId does not exists: {TaskId}";
                    return newTask;
                }

                // 🔹 Soft delete
                taskData.Isdeleted = true;
                taskData.Lastmodifieddate = DateTime.Now;

                _dbContext.Tasks.Update(taskData);
                await _dbContext.SaveChangesAsync();

                newTask.IsSuccess = true;
                newTask.Message = "Task deleted successfully";

                return newTask;
            }
            catch (Exception ex)
            {
                newTask.IsSuccess = false;
                newTask.Message = $"Task deleted failed: {ex.Message}";
                return newTask;
            }
        }
    }
}
