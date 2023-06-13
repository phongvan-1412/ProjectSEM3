using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using ProjectSEM3.Utils;

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

            ViewBag.PendingCv = DbContext.Instance.Exec<List<CV.Res>>(DbStore.GetCvByStatus, new Dictionary<string, dynamic>
            {
                { "@Status", 1},
            });

            return View(hrs);
        }

        [HttpPost]
        [Route("/admin/hr/newhr")]
        public JsonResult NewHr(Hr.Req hr)
        {
            var check = DbContext.Instance.Exec<List<IsExistsEmail>>(DbStore.IsEmailIsExsists, new Dictionary<string, dynamic> { { "@Email", hr.Email } }).FirstOrDefault();
            if (check.IsExists)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Email already exists.",
                    IsSuccess = false,
                });
            }

            var param = new Dictionary<string, dynamic>
            {
                { "@Name", hr.Name },
                { "@Email", hr.Email},
                { "@Password", hr.Password.EncryptPassword() },
                { "@Contact", hr.Contact },
                { "@Address", hr.Address },
                { "@Education", hr.Education },
                { "@Experience",  hr.Experience }
            };

            var result = DbContext.Instance.Exec<List<Hr.Res>>(DbStore.InsertHr, param);
            if(result is null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Something Wrong in server.",
                    IsSuccess = true,
                });
            }

            return Json(new DbContext.Result<Hr.Res>
            {
                Data = result.FirstOrDefault(),
                Mes = "Create Hr successfull.",
                IsSuccess = true,
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
                    IsSuccess = false,
                });
            }
            var result = ls.FirstOrDefault();
            result.RowIndex = rowIndex;
            return Json(new DbContext.Result<Hr.Res>
            {
                Data = result,
                Mes = "Successfull.",
                IsSuccess = true,
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
                IsSuccess = true,
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