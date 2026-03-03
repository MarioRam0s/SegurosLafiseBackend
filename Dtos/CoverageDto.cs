namespace SegurosLafiseBackend.Dtos
{
    public class CoverageDto
    {
        public int Id { get; set; }
        public int IdCoverageCategory { get; set; }
        public decimal Rate { get; set; }
    }

    public class CreateCoverageDto
    {
        public int IdCoverageCategory { get; set; }
        public decimal Rate { get; set; }
    }
}
