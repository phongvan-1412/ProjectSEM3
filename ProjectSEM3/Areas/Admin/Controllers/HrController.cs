using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class HrController : Controller
    {
        // GET: Admin/Hr
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewHr(Hr.Req hr)
        {
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.InsertHr, new Dictionary<string, dynamic> { { "@Email", "hremail1@gmail.com" } }).FirstOrDefault();

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var param = new Dictionary<string, dynamic>
            {
                { "@Name", "hr12 name" },
                { "@Email", "hremail1@gmail.com" },
                { "@Password", "111111" },
                { "@Contact", "111111" },
                { "@Address", "111111" },
                { "@Education", "111111" },
                { "@Experience", "111111" }
            };

            result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.InsertHr, param).FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult UpdateHr(Hr.Req hr)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Name", "hr13 name" },
                { "@Email", "hremai131@gmail.com" },
                { "@Password", "111111" }
            };
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.UpdateHr, param).FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult GetHrById(int id)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", 1 },
            };
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrById, param).FirstOrDefault();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult GetHrs(Hr.Req hr)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Name", "hr13 name" },
                { "@Email", "hremai131@gmail.com" },
                { "@Contact", "111111" },
                { "@Address", "111111" },
                { "@Education", "111111" },
                { "@Experience", "111111" }
            };
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrs, param);

            return RedirectToAction(nameof(Index));
        }
    }
}