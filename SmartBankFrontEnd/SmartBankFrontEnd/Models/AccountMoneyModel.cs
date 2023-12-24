using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartBankFrontEnd.Models
{
    public class AccountMoneyModel
    {
        public int Id { get; set; }

        [DisplayName("Кількість грошей, що буде додана до поточної суми")]
        [Required(ErrorMessage ="Введіть значення")]
        public decimal AmountOfMoneyToAdd { get; set; }

        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
