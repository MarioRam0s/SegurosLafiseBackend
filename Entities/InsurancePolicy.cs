using System;
using System.Collections.Generic;

namespace SegurosLafiseBackend.Entities;

public partial class InsurancePolicy
{
    public int Id { get; set; }

    public string InsurancePolicy1 { get; set; } = null!;

    public int IdClient { get; set; }

    public int IdVehicle { get; set; }

    public DateOnly IssueDate { get; set; }

    public decimal CoverageAmount { get; set; }

    public decimal TotalPremium { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public DateTime DeleteAt { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Vehicle IdVehicleNavigation { get; set; } = null!;

    public virtual ICollection<InsurancePolicyCoverage> InsurancePolicyCoverages { get; set; } = new List<InsurancePolicyCoverage>();
}
