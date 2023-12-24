using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class ExpenseModel
    {
        [DisplayName("ID витрати")]
        public int Id { get; set; }

        [DisplayName("Дата витрати")]
        public DateTime DateIn { get; set; }

        [DisplayName("Категорія")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
