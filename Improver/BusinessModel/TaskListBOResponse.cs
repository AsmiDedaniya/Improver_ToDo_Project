namespace Improver.BusinessModel
{
    public class TaskListBOResponse :BaseResponse
    {
        public List<TaskBOResponse> Tasks { get; set; }
    }
}
