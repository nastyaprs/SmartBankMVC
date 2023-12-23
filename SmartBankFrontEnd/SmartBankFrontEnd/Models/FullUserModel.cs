using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class FullUserModel
    {
        public int Id { get; set; }

        [DisplayName("Ім'я")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Прізвище")]
        public string LastName { get; set; } = null!;

        [DisplayName("По батькові")]
        public string FathersName { get; set; } = null!;

        [DisplayName("Дата народження")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Номер паспорта")]
        public string PassportNumber { get; set; } = null!;

        [DisplayName("Номер телефону")]
        public string Phone { get; set; } = null!;

        [DisplayName("Пошта")]
        public string Email { get; set; } = null!;

        [DisplayName("Веріфікований")]
        public bool IsVerified { get; set; }

        public AddressModel Address { get; set; } = null!;

        [DisplayName("Країна")]
        public string Country { get; set; } = null!;

        [DisplayName("Місто")]
        public string City { get; set; } = null!;

        [DisplayName("Вулиця, будинок, квартира")]
        public string AddressLine { get; set; } = null!;

        [DisplayName("Дата реєстрації")]
        public DateTime DateIn { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
