namespace Improver.BusinessModel
{
    public class TaskValidator
    {
        public string Taskchecker(TaskBORequest objBoRequest)
        {
            string validationmsg = "";

            if (objBoRequest == null)
            {
                return "task request object is null";
            }

            if (objBoRequest.Categoryid==null)
            {
                validationmsg += "category is required. ";
            }

            if (objBoRequest.Taskdate == null)
            {
                validationmsg += "taskdate is required. ";
            }

            if (objBoRequest.Taskpriority == null)
            {
                validationmsg += "taskpriority is required. ";
            }

            if (objBoRequest.Taskstatus == null)
            {
                validationmsg += "Taskstatus is required. ";
            }
            if (string.IsNullOrWhiteSpace(objBoRequest.Taskname))
            {
                validationmsg += "TaskName is required. ";
            }
            return validationmsg;
        }
    }
}
