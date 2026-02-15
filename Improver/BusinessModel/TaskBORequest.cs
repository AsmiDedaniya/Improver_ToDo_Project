using Improver.Models;

namespace Improver.BusinessModel
{
    public class TaskBORequest
    {
        public int? Taskid { get; set; }

        public string Taskname { get; set; }

        public DateOnly Taskdate { get; set; }

        public DateTime? Starttime { get; set; }

        public DateTime? Endtime { get; set; }

        public int Categoryid { get; set; }

        public DateTime? Lastmodifieddate { get; set; }

        public string? Tasknote { get; set; }

        public bool? Isdeleted { get; set; }

        public string Taskpriority { get; set; }

        public string Taskstatus { get; set; }

        public int Createdbyid { get; set; }

        public static Tasks TaskCreate(TaskBORequest objBoRequest)
        {
            Tasks objTask = new Tasks();

            objTask.Taskid = objBoRequest.Taskid.HasValue ? objBoRequest.Taskid.Value : 0;
            objTask.Taskname = objBoRequest.Taskname;
            objTask.Taskdate = objBoRequest.Taskdate;
            objTask.Starttime = objBoRequest.Starttime;
            objTask.Endtime = objBoRequest.Endtime;
            objTask.Categoryid = objBoRequest.Categoryid;
            objTask.Createddate = DateTime.Now;
            objTask.Isdeleted = objBoRequest.Isdeleted.HasValue ? objBoRequest.Isdeleted.Value : false;
            objTask.Lastmodifieddate = objBoRequest.Lastmodifieddate.HasValue ? objBoRequest.Lastmodifieddate.Value : DateTime.Now;
            objTask.Tasknote = objBoRequest.Tasknote;
            objTask.Createdbyid = objBoRequest.Createdbyid;
        


            return objTask;
        }
    }
}
