using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartBankFrontEnd.Models
{
    public class AddReportModel
    {
        public int UserId { get; set; }

        [DisplayName("Дата початку звіту")]
        [Required(ErrorMessage ="Введіть значення")]
        public DateTime DateFrom { get; set; }

        [DisplayName("Дата кінця звіту")]
        [Required(ErrorMessage = "Введіть значення")]
        public DateTime DateTo { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
