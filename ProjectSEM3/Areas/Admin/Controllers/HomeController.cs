using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Name", "hr12 name" },
                { "@Email", "hremail1@gmail.com" },
                { "@Password", "111111" }
            };
            DbContext.Instance.Exec(DbStore.InsertHr, param);
            return View();
        }
    }
}