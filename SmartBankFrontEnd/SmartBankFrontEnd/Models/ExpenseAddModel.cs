using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class ExpenseAddModel
    {
        public int AccountId { get; set; }

        [DisplayName("Кількість грошей для витрати")]
        public decimal Money { get; set; }

        [DisplayName("ID категорії")]
        public int CategoryId { get; set; }

        public string Token { get; set; }
        public int UserId { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
