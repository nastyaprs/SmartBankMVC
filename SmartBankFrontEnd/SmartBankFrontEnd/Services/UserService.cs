using Newtonsoft.Json;
using SmartBankFrontEnd.Helper.Constants;
using SmartBankFrontEnd.Helper.Dtos;
using SmartBankFrontEnd.Helper.Enums;
using SmartBankFrontEnd.Interfaces;
using SmartBankFrontEnd.Models;
using System.Net.Http.Headers;

namespace SmartBankFrontEnd.Services
{
    public class UserService: IUserService
    {
        public async Task<GetUnverifiedUsersResultDto?> GetUnverifiedUsers(string token)
        {
            string apiUrl = Routes.MainApiLink + Routes.AdminUserList;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<FullUserModel>? userList = JsonConvert.DeserializeObject<List<FullUserModel>>(jsonContent);

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
    }
}
