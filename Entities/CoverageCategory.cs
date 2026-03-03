using System;
using System.Collections.Generic;

namespace SegurosLafiseBackend.Entities;

public partial class CoverageCategory
{
    public int Id { get; set; }

    public string NameCategory { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public DateTime DeleteAt { get; set; }

    public virtual ICollection<Coverage> Coverages { get; set; } = new List<Coverage>();
}
