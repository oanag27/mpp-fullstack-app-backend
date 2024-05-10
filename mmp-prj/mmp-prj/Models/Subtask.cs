using System;
using System.Collections.Generic;

namespace mmp_prj.Models;

public partial class Subtask
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Completed { get; set; }

    public int? TaskId { get; set; }

    public virtual Task? Task { get; set; }
}
