using Newtonsoft.Json;
using SmartBankFrontEnd.Helper.Constants;
using SmartBankFrontEnd.Helper.Dtos;
using SmartBankFrontEnd.Interfaces;
using SmartBankFrontEnd.Models;
using System.Text;

namespace SmartBankFrontEnd.Services
{
    public class AuthService: IAuthService
    {
        public async Task<LoginResultDto> GetUserByLogin(UserLoginModel userLoginModel)
        {
            string apiUrl = Routes.MainApiLink + Routes.AuthLogin;

            string jsonBody = JsonConvert.SerializeObject(userLoginModel);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return new LoginResultDto
                    {
                        IsSuccess = true,
                        Response = responseContent
                    };
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return new LoginResultDto
                    {
                        IsSuccess = false,
                        Response = responseContent
                    };
                }
            }
        }
    }
}
