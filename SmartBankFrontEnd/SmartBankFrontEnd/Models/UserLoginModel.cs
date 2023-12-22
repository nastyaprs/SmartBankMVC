using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class UserLoginModel
    {
        [DisplayName("Пошта")]
        [Required(ErrorMessage = "Введіть пошту")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Введіть пошту")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
