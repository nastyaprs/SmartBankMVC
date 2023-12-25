using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class ReportModel
    {
        [DisplayName("Унікальний номер звіту")]
        public int Id { get; set; }

        [DisplayName("Дата створення")]
        public DateTime DateIn { get; set; }

        [DisplayName("Звіт:")]
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }

        public string Token { get; set; } = string.Empty;

        [DisplayName("Дата початку")]
        public DateTime DateFrom { get; set; }
        [DisplayName("Дата кінця")]
        public DateTime DateTo { get; set; }
    }
}
