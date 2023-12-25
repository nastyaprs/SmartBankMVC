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

        [HttpGet]
        public IActionResult AddMoneyToAccount(string token, int userId, int accountId)
        {
            var model = new AccountMoneyModel()
            {
                Token = token,
                UserId = userId,
                Id = accountId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMoneyToAccount(AccountMoneyModel accountMoneyModel)
        {
            try
            {
                var result = await  _userService.AddMoneyToAccount(accountMoneyModel);

                if (result)
                {
                    return RedirectToAction("GetAccounts", new { userId = accountMoneyModel.UserId, token = accountMoneyModel.Token });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("AddMoneyToAccount", new { token = accountMoneyModel.Token, userId = accountMoneyModel.UserId, accountId = accountMoneyModel.Id });
        }

        [HttpGet]
        public IActionResult AddExpense(int userId, string token, int accountId, string? errorMessage)
        {
            var expense = new ExpenseAddModel()
            {
                UserId = userId,
                Token = token,
                AccountId = accountId,
                ErrorMessage = errorMessage
            };

            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(ExpenseAddModel expenseAddModel)
        {
            try
            {
                var result = await _userService.AddExpense(expenseAddModel);

                if(result == true)
                {
                    return RedirectToAction("GetAccounts", new { userId = expenseAddModel.UserId, token = expenseAddModel.Token });
                }
                else if(result == false)
                {
                    return RedirectToAction("AddExpense", new
                    {
                        userId = expenseAddModel.UserId,
                        token = expenseAddModel.Token,
                        accountId = expenseAddModel.AccountId,
                        errorMessage = "На вашому рахунку недостатньо грошей"
                    });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("AddExpense", new
            {
                userId = expenseAddModel.UserId,
                token = expenseAddModel.Token,
                accountId = expenseAddModel.AccountId,
                errorMessage = ""
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetReports(string token)
        {
            var reports = await _userService.GetReports(token);

            return View(reports);
        }

        [HttpGet]
        public IActionResult GetReportDetails(ReportModel reportModel)
        {
            return View(reportModel);
        }

        [HttpGet]
        public IActionResult CreateReport(string token, int userId)
        {
            var addReport = new AddReportModel()
            {
                Token = token,
                UserId = userId
            };

            return View(addReport);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(AddReportModel addReportModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.CreateReport(addReportModel);

                    if (result)
                    {
                        return RedirectToAction("GetReports", new { token = addReportModel.Token });
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("CreateReport", new { token = addReportModel.Token, userId = addReportModel.UserId });
        }
    }
}
