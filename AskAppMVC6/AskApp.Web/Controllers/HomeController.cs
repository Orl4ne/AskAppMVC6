using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskApp.Web.Models;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.Interfaces;
using AskApp.Identity;
using Microsoft.AspNetCore.Identity;

namespace AskApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAskUC _askUC;
        private readonly UserManager<AskAppIdentityUser> _userManager;
        private readonly SignInManager<AskAppIdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, IAskUC askUC, UserManager<AskAppIdentityUser> userManager, SignInManager<AskAppIdentityUser> signInManager)
        {
            _logger = logger;
            _askUC = askUC;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetQuestionsList()
        {
            var result = _askUC.ShowAllQuestions();
            return View(result);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
