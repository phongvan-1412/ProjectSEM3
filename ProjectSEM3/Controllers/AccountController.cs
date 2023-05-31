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

        //[HttpPost]
        //public ActionResult Login(reviewer reviewer)
        //{
        //    reviewer accountExist = Service.Instance.GetReviewerByEmail(reviewer.rev_email);
        //    if (accountExist == null || !DecryptPassword(accountExist.rev_password).Equals(reviewer.rev_password) || !accountExist.rev_email.Equals(reviewer.rev_email))
        //    {
        //        TempData["accountFailed"] = "Email or password is invalid. Please try again!";
        //    }
        //    else
        //    {
        //        TempData["accountVerified"] = "Log in successfully";
        //        Session["account"] = accountExist;
        //    }

        //    return View();
        //}

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }
    }
}