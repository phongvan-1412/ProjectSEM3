using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class CVController : Controller
    {
        public ActionResult Index()
        {
            var lst = DbContext.Instance.Exec<List<CV.Res>>(DbStore.GetCVs);
            return View(lst);
        }

        [HttpPost]
        [Route("/admin/cv/ChangeCvStatus")]
        public JsonResult ChangeCvStatus(CV.Req cv)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", cv.Id },
                { "@Status", cv.Status },
            };
            var result = DbContext.Instance.Exec<List<CV.Res>>(DbStore.ChangeCvStatus, param).FirstOrDefault();

            return Json(new DbContext.Result<CV.Res>
            {
                Data = result,
                Mes = "Successfull.",
                IsSuccess = true,
            });
        }
    }
}