using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartBankFrontEnd.Models
{
    public class UserRegisterModel
    {
        [DisplayName("Ім'я")]
        [Required(ErrorMessage = "Введіть значення")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Прізвище")]
        [Required(ErrorMessage = "Введіть значення")]
        public string LastName { get; set; } = null!;

        [DisplayName("По батькові")]
        [Required(ErrorMessage = "Введіть значення")]
        public string FathersName { get; set; } = null!;

        [DisplayName("Дата народження")]
        [Required(ErrorMessage = "Введіть значення")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Номер паспорта")]
        [Required(ErrorMessage = "Введіть значення")]
        public string PassportNumber { get; set; } = null!;

        [DisplayName("Номер телефону")]
        [Required(ErrorMessage = "Введіть значення")]
        public string Phone { get; set; } = null!;

        [DisplayName("Електрона пошта")]
        [Required(ErrorMessage = "Введіть значення")]
        public string Email { get; set; } = null!;

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Введіть значення")]
        public string Password { get; set; } = string.Empty;

        [DisplayName("Країна")]
        [Required(ErrorMessage = "Введіть значення")]
        public string Country { get; set; } = null!;

        [DisplayName("Місто")]
        [Required(ErrorMessage = "Введіть значення")]
        public string City { get; set; } = null!;

        [DisplayName("Вулиця, дім, квартира")]
        [Required(ErrorMessage = "Введіть значення")]
        public string AddressLine { get; set; } = null!;

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
