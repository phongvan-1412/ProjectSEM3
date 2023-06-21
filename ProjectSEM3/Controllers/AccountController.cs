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
            var param = new Dictionary<string, dynamic>
            {
                { "@Email", contestant.Email},
                { "@Password", contestant.Password.EncryptPassword() },
            };

            var account = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetContestantByEmailPass, param);
            Session["Contestant"] = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetContestantByEmailPass, param);

            if(account.Count() == 0)
            {
                TempData["accountFailed"] = "Your email or password is invalid. Please try again!";
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Exam", "Quiz");
            }
        }
    }
}