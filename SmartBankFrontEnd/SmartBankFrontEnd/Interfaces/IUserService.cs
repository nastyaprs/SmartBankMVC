using SmartBankFrontEnd.Helper.Dtos;
using SmartBankFrontEnd.Models;

namespace SmartBankFrontEnd.Interfaces
{
    public interface IUserService
    {
        Task<GetUnverifiedUsersResultDto?> GetUnverifiedUsers(string token);
        Task<bool> VefiryUser(int id, string token);
        Task<FullUserModel?> GetUserProfile(string token);
        Task<List<CategoryModel>> GetUsersCategories(int userId, string token);
        Task<bool> AddNewCategory(CategoryModel categoryModel);
    }
}
