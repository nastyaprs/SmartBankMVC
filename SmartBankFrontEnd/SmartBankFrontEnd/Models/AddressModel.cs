namespace SmartBankFrontEnd.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
    }
}
