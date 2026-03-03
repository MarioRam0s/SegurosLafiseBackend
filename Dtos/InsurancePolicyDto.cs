namespace SegurosLafiseBackend.Dtos
{
    public class InsurancePolicyDto
    {
        public int Id { get; set; }

        public string PolicyNumber { get; set; } = null!; // InsurancePolicy1

        public int IdClient { get; set; }

        public int IdVehicle { get; set; }

        public DateOnly IssueDate { get; set; }

        public decimal CoverageAmount { get; set; }

        public decimal TotalPremium { get; set; }

        public bool Active { get; set; }
    }

    public class CreateInsurancePolicyDto
    {
        public string PolicyNumber { get; set; } = null!;

        public int IdClient { get; set; }

        public int IdVehicle { get; set; }

        public DateOnly IssueDate { get; set; }

        public decimal CoverageAmount { get; set; }

        public List<int> CoverageIds { get; set; } = new();
    }


}
