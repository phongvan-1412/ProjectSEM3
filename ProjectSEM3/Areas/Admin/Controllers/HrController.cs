using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class HrController : Controller
    {
        // GET: Admin/Hr
        public ActionResult Index()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Name", "" },
                { "@Email", ""},
                { "@Contact", "" },
                { "@Address", "" },
                { "@Education", "" },
                { "@Experience",  "" }
            };

            var hrs = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrs, param);

            return View(hrs);
        }

        [HttpPost]
        [Route("/admin/hr/newhr")]
        public JsonResult NewHr(Hr.Req hr)
        {
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrByEmail, new Dictionary<string, dynamic> { { "@Email", hr.Email } });
            if (result != null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Email already exists.",
                    IsErr = false,
                });
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
            return Json(new DbContext.Result
            {
                Mes = "Create Hr successfull.",
                IsErr = true,
            });
        }

        [HttpPost]
        [Route("/admin/hr/UpdateHR")]
        public JsonResult UpdateHr(Hr.Req hr,int rowIndex)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", hr.Id },
                { "@Name", hr.Name },
                { "@Email", hr.Email},
                { "@Contact", hr.Contact },
                { "@Address", hr.Address },
                { "@Education", hr.Education },
                { "@Experience",  hr.Experience }
            };
            var ls = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.UpdateHr, param);
            if(ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Fail.",
                    IsErr = false,
                });
            }
            var result = ls.FirstOrDefault();
            result.RowIndex = rowIndex;
            return Json(new DbContext.Result<Hr.Res>
            {
                Data = result,
                Mes = "Successfull.",
                IsErr = true,
            });
            //return PartialView(@"~/Areas/Admin/Views/Shared/Partials/Hr/_PartialRowHr.cshtml", result);
        }

        [HttpGet]
        public JsonResult GetHrById(int id)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", 1 },
            };
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrById, param).FirstOrDefault();

            return Json(result);
        }

        [HttpPost]
        [Route("/admin/hr/ChangeHrStatus")]
        public JsonResult ChangeHrStatus(Hr.Req hr)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", hr.Id },
                { "@Status", hr.Status },
            };
            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.ChangeHrStatus, param).FirstOrDefault();

            return Json(new DbContext.Result<Hr.Res>
            {
                Data = result,
                Mes = "Successfull.",
                IsErr = true,
            });
        }

        [HttpGet]
        public ActionResult GetHrs()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Name", "" },
                { "@Email", ""},
                { "@Contact", "" },
                { "@Address", "" },
                { "@Education", "" },
                { "@Experience",  "" }
            };

            var hrs = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.GetHrs, param);

            return RedirectToAction(nameof(Index));
        }
    }
}