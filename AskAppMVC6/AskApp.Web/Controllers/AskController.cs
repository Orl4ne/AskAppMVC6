using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskApp.Common.Interfaces;
using AskApp.Common.TOs;
using AskApp.Identity;
using AskApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
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
            var userQuestions = new List<UserQuestionVM>();
            foreach (var qst in questions)
            {
                var userQuestionVM = new UserQuestionVM
                {
                    User = _userManager.FindByIdAsync(qst.AuthorId.ToString()).Result,
                    Question = qst,
                };
                userQuestions.Add(userQuestionVM);
            }
            return View(userQuestions);
        }

        // GET: AskController/Details/5
        public ActionResult Details(int id)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var userQuestionVM = new UserQuestionVM
            {
                Question = _askUC.ShowThisQuestion(id),
                Answers = _askUC.GetAnswersByQuestion(id),
                User = currentUser,
            };

            return View(userQuestionVM);
        }

        // GET: AskController/Create
        [Authorize]
        public ActionResult CreateQuestion()
        {
            return View();
        }

        // POST: AskController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(QuestionTO question)
        {
            try
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                question.AuthorId = currentUser.Id;
                _askUC.AskAQuestion(question);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: AskController/Delete/5
        [HttpGet]
        public ActionResult DeleteQuestion(int id)
        {
            var question = _askUC.ShowThisQuestion(id);
            var userQuestionVM = new UserQuestionVM
            {
                Question = question,
            };
            return View(userQuestionVM);
        }

        // POST: AskController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(int id, UserQuestionVM userQuestionVM)
        {
            try
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                var question = _askUC.ShowThisQuestion(id);
                userQuestionVM.Question = question;
                var questionUserId = question.AuthorId;
                if (currentUser.Id == userQuestionVM.Question.AuthorId)
                {
                    _askUC.DeletingQuestion(currentUser.Id, userQuestionVM.Question);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: AskController/CreateAnswer
        [Authorize]
        public ActionResult Answering()
        {
            return View();
        }

        // POST: AskController/CreateAnswer
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Answering(int id, AnswerQuestionVM answerVM)
        {
            try
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                var associatedQuestion = _askUC.ShowThisQuestion(id);
                var answerTO = new AnswerTO
                {
                    Message = answerVM.Answer.Message,
                    AssociatedQuestion = associatedQuestion,
                    AuthorId = currentUser.Id,
                };
                _askUC.AnsweringQuestion(id, answerTO);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
