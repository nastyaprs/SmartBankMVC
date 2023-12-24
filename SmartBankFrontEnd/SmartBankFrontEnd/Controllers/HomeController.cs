using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using SmartBankFrontEnd.Helper.Constants;
using SmartBankFrontEnd.Helper.Enums;
using SmartBankFrontEnd.Interfaces;
using SmartBankFrontEnd.Models;
using System.Diagnostics;

namespace SmartBankFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public HomeController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            UserLoginModel userLoginModel = new UserLoginModel();
            return View(userLoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.GetUserByLogin(userLoginModel);

                    if (result.IsSuccess)
                    {
                        return RedirectToAction("Index", new { token = result.Response});
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(userLoginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string token)
        {
            if(string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var callAdminPage = await _userService.GetUnverifiedUsers(token);
            
            if(callAdminPage?.ResponseResult == ResponseResultEnum.Ok)
            {
                return View(callAdminPage.FullUserModels);
            }
            else if(callAdminPage?.ResponseResult == ResponseResultEnum.WrongRole)
            {
                return RedirectToAction("Main", new { token = token });
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var userRegisterModel = new UserRegisterModel();
            return View(userRegisterModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel userRegisterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.RegisterNewUser(userRegisterModel);

                    if (result == true)
                    {
                        return RedirectToAction("Login");
                    }
                    else if(result == false)
                    {
                        userRegisterModel.ErrorMessage = ErrorMessages.EmailIsTaken;
                        return View(userRegisterModel);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(userRegisterModel);
        }

        [HttpGet]
        public IActionResult Verify(FullUserModel fullUserModel)
        {
            return View(fullUserModel);
        }

        [HttpPost]
        public async Task<IActionResult> Verify(int id, string token)
        {
            try
            {
                var result = await _userService.VefiryUser(id, token);

                if (result)
                {
                    return RedirectToAction("Index", new { token = token });
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("Index", new { token = token });
        }

        [HttpGet]
        public async Task<IActionResult> Main(string token)
        {
            var result = await _userService.GetUserProfile(token);

            if(result == null)
            {
                return RedirectToAction("Login");
            }

            return View(result);
        }

        [HttpGet]       
        public async Task<IActionResult> GetCategories(string token, int id)
        {
            var result =await _userService.GetUsersCategories(id, token);

            return View(result);
        }

        [HttpGet]
        public IActionResult AddCategory(string token, int userId)
        {
            var category = new CategoryModel()
            {
                Token = token,
                UserId = userId
            };

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryModel categoryModel)
        {
            try
            {
                var result = await _userService.AddNewCategory(categoryModel);

                if (result)
                {
                   return RedirectToAction("Main", new { token = categoryModel.Token });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("AddCategory", new { token = categoryModel.Token, userId = categoryModel.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts(int userId, string token)
        {
            var accounts = await _userService.GetUsersAccounts(userId, token);

            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountDetails(int accountId, string token)
        {
            var accountDetails = await _userService.GetAccountDetails(token, accountId);

            return View(accountDetails);
        }

        [HttpGet]
        public IActionResult AddAccount(int userId, string token)
        {
            var currency = new CurrencyModel()
            {
                UserId = userId,
                Token = token
            };

            return View(currency);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(CurrencyModel currencyModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.AddNewAccount(currencyModel.UserId, currencyModel.Id, currencyModel.Token);

                    if (result)
                    {
                        return RedirectToAction("GetAccounts", new { userId = currencyModel.UserId, token = currencyModel.Token });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("AddAccount", new { userId = currencyModel.UserId, token = currencyModel.Token });
        }
    }
}
