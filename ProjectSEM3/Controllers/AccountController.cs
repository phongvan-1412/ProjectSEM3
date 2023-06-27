using Newtonsoft.Json;
using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using ProjectSEM3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ProjectSEM3.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Contestant.Res contestant)
        {
            var paramAccount = new Dictionary<string, dynamic>
            {
                { "@Email", contestant.Email},
                { "@Password", contestant.Password.EncryptPassword() },
            };
            var account = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetContestantByEmailPass, paramAccount);

            var paramExam = new Dictionary<string, dynamic>
            {
                { "@ContestId", account.FirstOrDefault().Id}
            };

            var exam = DbContext.Instance.Exec<List<Exam.Res>>(DbStore.GetExamnById, paramExam);
            var endTime = exam.FirstOrDefault().LateTime;
            var now = DateTime.UtcNow;

            if (account.Count() == 0)
            {
                TempData["accountFailed"] = "Your email or password is invalid. Please try again!";
                return RedirectToAction("Login");
            }
            else if (endTime <= now)
            {
                var paramDeactive = new Dictionary<string, dynamic>
                {
                    { "@Id", account.FirstOrDefault().Id },
                    { "@Status", 3 },
                };
                DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.ChangeCvStatus, paramDeactive);

                TempData["accountEndTime"] = "Your account has expired because it's over 30 mins from start time!";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Exam", "Quiz", new { contestant = account.FirstOrDefault().Id });
            }
        }
    }
}