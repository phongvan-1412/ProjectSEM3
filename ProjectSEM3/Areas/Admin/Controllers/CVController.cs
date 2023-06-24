using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class CVController : Controller
    {
        public ActionResult Index()
        {
            var lst = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetCVs);
            var param = new Dictionary<string, dynamic>
            {
                { "@Status", 1},
                { "@IsViewed", 0},
            };
            ViewBag.PendingCv = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetCvByStatus, param);
            var cvs = Session["ViewCv"] as List<Contestant.Res>;
            Session.Remove("ViewCv");
            if (cvs != null)
            {
                lst.Clear();
                lst.AddRange(cvs);
            }

            return View(lst);
        }

        [HttpPost]
        [Route("/admin/cv/GetPendingCv")]
        public JsonResult GetPendingCv()
        {
            var result = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetCvByStatus, new Dictionary<string, dynamic>
            {
                { "@Status", 1},
                { "@IsViewed", 0},
            });

            return Json(new DbContext.Result<List<Contestant.Res>>
            {
                Data = result,
                Mes = "",
                IsSuccess = true,
            });
        }

        public ActionResult View(int id)
        {
            var cvs = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.UpdateViewedCv, new Dictionary<string, dynamic>
            {
                { "@Id", id},
            });

            Session["ViewCv"] = cvs;

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [Route("/admin/cv/Reject")]
        public JsonResult Reject(Contestant.Req cv)
        {
            try
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "@Id", cv.Id },
                    { "@Status", cv.Status },
                };
                var result = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.ChangeCvStatus, param);

                if (result == null)
                {
                    return Json(new DbContext.Result
                    {
                        Mes = "Update Cv Fail.",
                        IsSuccess = false,
                    });
                }

                var resultCv = result.FirstOrDefault();
                var email = new Email();
                var emailReject = new Email.Reject
                {
                    Name = resultCv.Name,
                    UserName = resultCv.Email,
                    JobLink = resultCv.JobLink
                };

                email.SendReject(emailReject);

                return Json(new DbContext.Result<Contestant.Res>
                {
                    Data = resultCv,
                    Mes = "Update Cv Successfull.",
                    IsSuccess = true,
                });
            }
            catch (Exception)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Something wrong in server.",
                    IsSuccess = false,
                });
            }
        }
    }
}