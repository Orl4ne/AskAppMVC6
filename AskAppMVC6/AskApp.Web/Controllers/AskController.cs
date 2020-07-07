using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskApp.Common.Interfaces;
using AskApp.Common.TOs;
using AskApp.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AskApp.Web.Controllers
{
    public class AskController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAskUC _askUC;
        private readonly UserManager<AskAppIdentityUser> _userManager;
        private readonly SignInManager<AskAppIdentityUser> _signInManager;

        public AskController(ILogger<HomeController> logger, IAskUC askUC, UserManager<AskAppIdentityUser> userManager, SignInManager<AskAppIdentityUser> signInManager)
        {
            _logger = logger;
            _askUC = askUC;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: AskController
        [HttpGet]
        public IActionResult Index()
        {
            var questions = _askUC.ShowAllQuestions();
            return View(questions);
        }

        // GET: AskController/Details/5
        public ActionResult Details(int id)
        {
            var question = _askUC.ShowThisQuestion(id);
            return View(question);
        }

        // GET: AskController/Create
        public ActionResult CreateQuestion()
        {
            return View();
        }

        // POST: AskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(QuestionTO question)
        {
            //try
            //{
                var currentUser = _userManager.GetUserAsync(User).Result;
                question.AuthorId = currentUser.Id;
                _askUC.AskAQuestion(question);
                return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: AskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
