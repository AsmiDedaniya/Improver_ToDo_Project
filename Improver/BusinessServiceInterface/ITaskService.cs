using Improver.BusinessModel;

namespace Improver.BusinessServiceInterface
{
    public interface ITaskService
    {
        Task<TaskBOResponse> Create(TaskBORequest objBoRequest);

        Task<TaskBOResponse> Update(TaskBORequest objBoRequest);

        Task<TaskBOResponse> GetById(int TaskId, int Createdbyid);

        Task<TaskListBOResponse> GetList(int CreatedbyId, DateOnly Taskdate);

        Task<TaskBOResponse> Delete(int TaskId, int UserId);
    }
}
