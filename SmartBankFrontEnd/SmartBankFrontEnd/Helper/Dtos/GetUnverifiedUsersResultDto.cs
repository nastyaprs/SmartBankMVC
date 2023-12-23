using SmartBankFrontEnd.Helper.Enums;
using SmartBankFrontEnd.Models;

namespace SmartBankFrontEnd.Helper.Dtos
{
    public class GetUnverifiedUsersResultDto
    {
        public ResponseResultEnum ResponseResult { get; set; }
        public List<FullUserModel>? FullUserModels { get; set; }
    }
}
