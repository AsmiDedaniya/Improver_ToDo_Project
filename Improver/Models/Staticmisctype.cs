using System;
using System.Collections.Generic;

namespace Improver.Models;

public partial class Staticmisctype
{
    public int Staticmisctypeid { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<Staticmiscvaluemap> Staticmiscvaluemaps { get; set; } = new List<Staticmiscvaluemap>();
}
