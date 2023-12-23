using SmartBankFrontEnd.Helper.Dtos;

namespace SmartBankFrontEnd.Interfaces
{
    public interface IUserService
    {
        Task<GetUnverifiedUsersResultDto?> GetUnverifiedUsers(string token);
    }
}
