using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
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
            return View();
        }

        [HttpPost]
        public ActionResult Login(Hr.Req hr)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Name", "hr13 name" },
                { "@Password", "111111" }
            };
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrByEmailPass, param).FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}