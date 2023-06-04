using Newtonsoft.Json;
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
        public ActionResult Login(string acc_name, string acc_password)
        {
            //reviewer accountExist = Service.Instance.GetReviewerByEmail(reviewer.rev_email);
            //if (accountExist == null || !DecryptPassword(accountExist.rev_password).Equals(reviewer.rev_password) || !accountExist.rev_email.Equals(reviewer.rev_email))
            //{
            //    TempData["accountFailed"] = "Email or password is invalid. Please try again!";
            //}
            //else
            //{
            //    TempData["accountVerified"] = "Log in successfully";
            //    Session["account"] = accountExist;
            //}
            if (!acc_name.Equals("1") && !acc_password.Equals("1"))
            {
                TempData["accountFailed"] = "Email or password is invalid. Please try again!";
            }
            else
            {
                TempData["accountVerified"] = "Log in successfully";
                //Session["account"] = accountExist;
            }
            return RedirectToAction("Quiz_Knowledge", "Quiz");
        }

        public ActionResult Quiz()
        {
            return View();
        }
    }
}