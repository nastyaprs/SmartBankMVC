using SmartBankFrontEnd.Helper.Dtos;
using SmartBankFrontEnd.Models;

namespace SmartBankFrontEnd.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResultDto> GetUserByLogin(UserLoginModel userLoginModel);
    }
}
