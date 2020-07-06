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

namespace AskApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAskUC _askUC;
        

        public HomeController(ILogger<HomeController> logger, IAskUC askUC)
        {
            _logger = logger;
            _askUC = askUC;
        }

        public IActionResult Index()
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
