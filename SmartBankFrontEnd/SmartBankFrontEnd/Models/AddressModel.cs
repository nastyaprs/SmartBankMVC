using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class AddressModel
    {
        public int Id { get; set; }

        [DisplayName("Країна")]
        public string Country { get; set; } = null!;

        [DisplayName("Місто")]
        public string City { get; set; } = null!;

        [DisplayName("Вулиця, будинок, квартира")]
        public string AddressLine { get; set; } = null!;
    }
}
