using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace mmp_prj.Models;

public partial class Task
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? Duration { get; set; }

    public virtual ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
}
