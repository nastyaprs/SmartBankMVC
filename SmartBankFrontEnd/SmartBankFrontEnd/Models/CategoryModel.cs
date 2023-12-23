using System.ComponentModel;

namespace SmartBankFrontEnd.Models
{
    public class CategoryModel
    {
        [DisplayName("ID категорії")]
        public int? Id { get; set; }

        [DisplayName("Назва категорії")]
        public string Name { get; set; }

        public string Token { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}
