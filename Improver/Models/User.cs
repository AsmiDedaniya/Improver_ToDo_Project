using System;
using System.Collections.Generic;

namespace Improver.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Createddate { get; set; }

    public DateTime Lastmodifieddate { get; set; }

    public string? UserimageUrl { get; set; }

    public bool? Isdeleted { get; set; }
}
