using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartBankFrontEnd.Models
{
    public class CurrencyModel
    {
        [DisplayName("Номер валюти")]
        [Required(ErrorMessage ="Введіть значення")]
        [Range(1,3,ErrorMessage ="Введіть дійсний номер валюти")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
