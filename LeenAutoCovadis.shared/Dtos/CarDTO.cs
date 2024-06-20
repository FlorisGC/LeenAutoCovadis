namespace LeenAutoCovadis.shared.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public long StartKilometers { get; set; }
        public long EndKilometers { get; set; }
        public long Kilometers { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public Boolean Available { get; set; }
        public UserDto User { get; set; }

    }
}