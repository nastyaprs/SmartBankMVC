using Newtonsoft.Json;
using NuGet.Common;
using SmartBankFrontEnd.Helper.Constants;
using SmartBankFrontEnd.Helper.Dtos;
using SmartBankFrontEnd.Helper.Enums;
using SmartBankFrontEnd.Interfaces;
using SmartBankFrontEnd.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SmartBankFrontEnd.Services
{
    public class UserService: IUserService
    {
        public async Task<GetUnverifiedUsersResultDto?> GetUnverifiedUsers(string token)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.AdminUserList;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<FullUserModel>? userList = JsonConvert.DeserializeObject<List<FullUserModel>>(jsonContent);

                    if(userList != null)
                    {
                        foreach (var item in userList)
                        {
                            item.Token = token;
                            item.Country = item.Address.Country;
                            item.City = item.Address.City;
                            item.AddressLine = item.Address.AddressLine;
                        }
                    }

                    return new GetUnverifiedUsersResultDto
                    {
                        ResponseResult = ResponseResultEnum.Ok,
                        FullUserModels = userList
                    };
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return new GetUnverifiedUsersResultDto
                    {
                        ResponseResult = ResponseResultEnum.WrongRole
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> VefiryUser(int id, string token)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.VerifyUser+id.ToString();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PutAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<FullUserModel?> GetUserProfile(string token)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.UserProfile;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    FullUserModel? user = JsonConvert.DeserializeObject<FullUserModel>(jsonContent);

                    if (user != null)
                    {
                        user.Token = token;
                        user.Country = user.Address.Country;
                        user.City = user.Address.City;
                        user.AddressLine = user.Address.AddressLine;

                        return user;
                    }
                }

                return null;
            }
        }

        public async Task<List<CategoryModel>> GetUsersCategories(int userId, string token)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.CategoryList + userId.ToString();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<CategoryModel> categories = JsonConvert.DeserializeObject<List<CategoryModel>>(jsonContent);

                    foreach(var cat in categories)
                    {
                        cat.Token = token;
                        cat.UserId = userId;
                    }

                    return categories;
                }

                return new List<CategoryModel>();
            }
        }

        public async Task<bool> AddNewCategory(CategoryModel categoryModel)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.CategoryAdd + categoryModel.UserId;
            categoryModel.Id = 0;

            var jsonBody = JsonConvert.SerializeObject(categoryModel);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", categoryModel.Token);

                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<List<AccountModel>> GetUsersAccounts(int userId, string token)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.AccountList + userId.ToString();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<AccountModel> accounts = JsonConvert.DeserializeObject<List<AccountModel>>(jsonContent);

                    foreach (var cat in accounts)
                    {
                        cat.Token = token;
                        cat.UserId = userId;
                    }

                    return accounts;
                }

                return new List<AccountModel>();
            }
        }

        public async Task<AccountDetailsModel?> GetAccountDetails(string token, int accountId)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.AccountDetails + accountId.ToString();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    AccountDetailsModel? account = JsonConvert.DeserializeObject<AccountDetailsModel>(jsonContent);

                    account!.Token = token;

                    return account;
                }

                return null;
            }
        }

        public async Task<bool> AddNewAccount(int userId, int currencyId, string token)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.AccountCreate + userId.ToString()+ "/" + currencyId.ToString();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> AddMoneyToAccount(AccountMoneyModel accountMoneyModel)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.AccountMoneyAdd;

            var jsonBody = JsonConvert.SerializeObject(accountMoneyModel);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accountMoneyModel.Token);

                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool?> AddExpense(ExpenseAddModel expenseAddModel)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.ExpenseAdd;

            var jsonBody = JsonConvert.SerializeObject(expenseAddModel);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", expenseAddModel.Token);

                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return false;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
