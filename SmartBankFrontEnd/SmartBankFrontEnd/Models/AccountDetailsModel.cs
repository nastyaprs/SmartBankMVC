using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class AccountDetailsModel
    {
        [DisplayName("ID рахунку")]
        public int Id { get; set; }

        [DisplayName("Кількість грошей")]
        public decimal AmountOfMoney { get; set; }

        [DisplayName("Валюта")]
        public string Currency { get; set; } = null!;

        [DisplayName("Дата створення")]
        public DateTime DateIn { get; set; }

        [DisplayName("Номер карти")]
        public string CardNumber { get; set; } = null!;

        [DisplayName("Витрати")]
        public List<ExpenseModel> Expenses { get; set; } = new List<ExpenseModel>();

        public string Token { get; set; } = string.Empty;
    }
}
