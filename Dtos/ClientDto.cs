namespace SegurosLafiseBackend.Dtos
{
    public class ClientDto
    {
        public int Id { get; set; }

        public string NameClient { get; set; } = null!;

        public string Identification { get; set; } = null!;

        public string Email { get; set; } = null!;
    }

    public class CreateClientDto
    {
        public string NameClient { get; set; } = null!;
        public string Identification { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
