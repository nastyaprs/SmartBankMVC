using Microsoft.AspNetCore.Mvc;
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
                //TODO: add view
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var 
        }
    }
}
