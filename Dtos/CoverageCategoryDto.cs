namespace SegurosLafiseBackend.Dtos
{
    public class CoverageCategoryDto
    {
        public int Id { get; set; }
        public string NameCategory { get; set; } = null!;
    }

    public class CreateCoverageCategoryDto
    {
        public string NameCategory { get; set; } = null!;
    }
}
