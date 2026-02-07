using System;
using System.Collections.Generic;

namespace Improver.Models;

public partial class User
{
    public int? Userid { get; set; }

    public string? Firstname { get; set; } 

    public string? Lastname { get; set; } 

    public string Password { get; set; } 

    public string Email { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Lastmodifieddate { get; set; }

    public string? UserimageUrl { get; set; }

    public bool? Isdeleted { get; set; }
}
