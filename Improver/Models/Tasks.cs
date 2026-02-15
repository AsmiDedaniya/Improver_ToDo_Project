using System;
using System.Collections.Generic;

namespace Improver.Models;

public partial class Tasks
{
    public int Taskid { get; set; }

    public DateOnly Taskdate { get; set; }

    public DateTime? Starttime { get; set; }

    public DateTime? Endtime { get; set; }

    public int Categoryid { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Lastmodifieddate { get; set; }

    public string? Tasknote { get; set; }

    public bool? Isdeleted { get; set; }

    public int? Taskpriorityid { get; set; }

    public int? Taskstatusid { get; set; }

    public int Createdbyid { get; set; }

    public string Taskname { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User Createdby { get; set; } 

    public virtual Staticmiscvaluemap? Taskpriority { get; set; }

    public virtual Staticmiscvaluemap? Taskstatus { get; set; }
}
