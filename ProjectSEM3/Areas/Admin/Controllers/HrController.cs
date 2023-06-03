using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

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
        [Route("/admin/hr/newhr")]
        public string NewHr(Hr.Req hr)
        {
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrByEmail, new Dictionary<string, dynamic> { { "@Email", hr.Email } });
            if (result != null)
            {
                return JsonConvert.SerializeObject("Email already exists");
            }

            var param = new Dictionary<string, dynamic>
            {
                { "@Name", hr.Name },
                { "@Email", hr.Email},
                { "@Password", hr.Password },
                { "@Contact", hr.Contact },
                { "@Address", hr.Address },
                { "@Education", hr.Education },
                { "@Experience",  hr.Experience }
            };

            result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.InsertHr, param);
            return JsonConvert.SerializeObject("Create Hr successfull.");
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