using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        [DisplayName("Кількість грошей")]
        public decimal AmountOfMoney { get; set; }

        [DisplayName("Валюта")]
        public string Currency { get; set; }

        [DisplayName("Дата створення")]
        public DateTime DateIn { get; set; }

        public string Token { get; set; } = string.Empty;

        public int? UserId { get; set; }
    }
}
