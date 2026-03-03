using System;
using System.Collections.Generic;

namespace SegurosLafiseBackend.Entities;

public partial class Coverage
{
    public int Id { get; set; }

    public int IdCoverageCategory { get; set; }

    public decimal Rate { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public DateTime DeleteAt { get; set; }

    public virtual CoverageCategory IdCoverageCategoryNavigation { get; set; } = null!;

    public virtual ICollection<InsurancePolicyCoverage> InsurancePolicyCoverages { get; set; } = new List<InsurancePolicyCoverage>();
}
