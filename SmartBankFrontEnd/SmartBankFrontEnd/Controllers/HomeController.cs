using Microsoft.AspNetCore.Mvc;
using SmartBankFrontEnd.Interfaces;
using SmartBankFrontEnd.Models;
using System.Diagnostics;

namespace SmartBankFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;

        public HomeController(IAuthService authService)
        {
            _authService = authService;
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
                        return RedirectToAction("Index");
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
