using System;
using System.Collections.Generic;

namespace SegurosLafiseBackend.Entities;

public partial class InsurancePolicyCoverage
{
    public int Id { get; set; }

    public int IdPolicy { get; set; }

    public int IdCoverage { get; set; }

    public decimal AppliedRate { get; set; }

    public decimal AppliedCoverageAmount { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public DateTime DeleteAt { get; set; }

    public virtual Coverage IdCoverageNavigation { get; set; } = null!;

    public virtual InsurancePolicy IdPolicyNavigation { get; set; } = null!;
}
