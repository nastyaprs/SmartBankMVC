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
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.AuthLogin;

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

        public async Task<bool?> RegisterNewUser(UserRegisterModel userRegisterModel)
        {
            string apiUrl = ApiRoutes.MainApiLink + ApiRoutes.AuthRegister;

            var jsonBody = JsonConvert.SerializeObject(userRegisterModel);


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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
