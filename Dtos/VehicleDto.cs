namespace SegurosLafiseBackend.Dtos
{
    public class VehicleDto
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int ManufacturingYear { get; set; }

        public decimal CommercialValue { get; set; }
    }

    public class CreateVehicleDto
    {
        public string LicensePlate { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int ManufacturingYear { get; set; }

        public decimal CommercialValue { get; set; }
    }
}
