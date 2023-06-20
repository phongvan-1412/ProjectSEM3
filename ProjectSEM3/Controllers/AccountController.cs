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
            var contestant = Session["Contestant"] as Contestant.Res;
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", contestant.Id},
            };

            ViewBag.Exam = DbContext.Instance.Exec<List<Exam.Res>>(DbStore.GetExamnById, param);

            return View(contestant);
        }

        [HttpPost]
        public ActionResult Login(Contestant.Req contestant)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Email", contestant.Email},
                { "@Password", contestant.Password.EncryptPassword() },
            };

            Session["Contestant"] = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetContestantById, param);
            return RedirectToAction(nameof(Login));
        }

        public ActionResult Quiz()
        {
            return View();
        }
    }
}