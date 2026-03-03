using System;
using System.Collections.Generic;

namespace SegurosLafiseBackend.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string NameClient { get; set; } = null!;

    public string Identification { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();
}
