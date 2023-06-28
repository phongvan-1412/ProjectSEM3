using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using ProjectSEM3.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Status", 1},
                { "@IsViewed", 0},
            };

            ViewBag.PendingCv = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetCvByStatus, param);
            ViewBag.Admin = Session["Admin"];
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.ResultLogin = Session["ResultLogin"];
            Session.Remove("ResultLogin");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Hr.Req hr)
        {
            var tet = hr.Password.EncryptPassword();
            var param = new Dictionary<string, dynamic>
            {
                { "@Email", hr.Email },
                { "@Password", hr.Password.EncryptPassword() }
            };
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrByEmailPass, param);

            if(result is null)
            {
                Session["ResultLogin"] = "Wrong Email or password. Please Try Again.";
                return RedirectToAction(nameof(Login));
            }
            Session["Admin"] = result.FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Logout()
        {
            Session.Remove("Admin");
            return RedirectToAction(nameof(Login));
        }
    }
}