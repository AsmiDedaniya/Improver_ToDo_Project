using System;
using System.Collections.Generic;

namespace Improver.Models;

public partial class Staticmiscvaluemap
{
    public int Staticmiscvaluemapid { get; set; }

    public string Code { get; set; } = null!;

    public int Staticmisctypeid { get; set; }

    public virtual Staticmisctype Staticmisctype { get; set; } = null!;
}
