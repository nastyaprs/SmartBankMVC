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
        Task<List<AccountModel>> GetUsersAccounts(int userId, string token);
        Task<AccountDetailsModel?> GetAccountDetails(string token, int accountId);
        Task<bool> AddNewAccount(int userId, int currencyId, string token);
        Task<bool> AddMoneyToAccount(AccountMoneyModel accountMoneyModel);
        Task<bool?> AddExpense(ExpenseAddModel expenseAddModel);
        Task<List<ReportModel>?> GetReports(string token);
        Task<bool> CreateReport(AddReportModel addReportModel);
    }
}
