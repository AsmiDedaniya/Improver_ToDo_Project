using Improver.Models;

namespace Improver.BusinessModel
{
    public class TaskBOResponse :BaseResponse
    {
        public int Taskid { get; set; }

        public string Taskname { get; set; }

        public DateOnly Taskdate { get; set; }

        public DateTime? Starttime { get; set; }

        public DateTime? Endtime { get; set; }

        public int Categoryid { get; set; }

        public DateTime? Lastmodifieddate { get; set; }

        public DateTime? Createddate { get; set; }


        public string? Tasknote { get; set; }

        public bool? Isdeleted { get; set; }

        public string Taskpriority { get; set; }

        public string Taskstatus { get; set; }

        public int Createdbyid { get; set; }

        public static TaskBOResponse TaskCreate(Tasks objTask)
        {
            TaskBOResponse objTaskBoResponse = new TaskBOResponse();

            objTaskBoResponse.Taskid = objTask.Taskid;
            objTaskBoResponse.Taskname = objTask.Taskname;
            objTaskBoResponse.Taskdate = objTask.Taskdate;
            objTaskBoResponse.Starttime = objTask.Starttime;
            objTaskBoResponse.Endtime = objTask.Endtime;
            objTaskBoResponse.Categoryid = objTask.Categoryid;
            objTaskBoResponse.Createddate = DateTime.Now;
            objTaskBoResponse.Isdeleted = objTask.Isdeleted.HasValue ? objTask.Isdeleted.Value : false;
            objTaskBoResponse.Lastmodifieddate = objTask.Lastmodifieddate.HasValue ? objTask.Lastmodifieddate.Value : DateTime.Now;
            objTaskBoResponse.Tasknote = objTask.Tasknote;
            objTaskBoResponse.Createdbyid = objTask.Createdbyid;
            objTaskBoResponse.Taskpriority = objTask.Taskpriority != null
     ? objTask.Taskpriority.Code
     : string.Empty;

            objTaskBoResponse.Taskstatus = objTask.Taskstatus != null
                ? objTask.Taskstatus.Code
                : string.Empty;

            return objTaskBoResponse;
        }
    }
}
