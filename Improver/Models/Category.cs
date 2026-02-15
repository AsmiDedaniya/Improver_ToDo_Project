using System;
using System.Collections.Generic;

namespace Improver.Models;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; }

    public int Userid { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime Createddate { get; set; }

    public DateTime Lastmodifieddate { get; set; }

    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

    public virtual User User { get; set; } 
}
