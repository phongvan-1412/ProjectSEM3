using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSEM3.Utils;
using Newtonsoft.Json;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class JobController : Controller
    {
        // GET: Admin/Job
        public ActionResult Index()
        {
            var jobs = DbContext.Instance.Exec<List<Job.Res>>(DbStore.GetJobs);
            ViewBag.Levels = DbContext.Instance.Exec<List<Level.Res>>(DbStore.GetAllLevels);
            ViewBag.PendingCv = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetCvByStatus, new Dictionary<string, dynamic>
            {
                { "@Status", 1},
                { "@IsViewed", 0},
            });

            return View(jobs);
        }

        [HttpPost]
        [Route("/admin/job/NewJob")]
        public JsonResult NewJob(Job.Req job)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Title", job.Title },
                { "@Location", job.Location},
                { "@Content", job.Content },
                { "@DatePosted", job.PostedDate },
                { "@EndDate", job.EndDate },
                { "@SalaryMin", job.SalaryMin },
                { "@SalaryMax",  job.SalaryMax },
                { "@LevelId",  job.LevelId }
            };
            var result = DbContext.Instance.Exec<List<Job.Res>>(DbStore.InsertJob, param);
            if (result is null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Something Wrong in server.",
                    IsSuccess = true,
                });
            }

            return Json(new DbContext.Result<Job.Res>
            {
                //Data = result.FirstOrDefault(),
                Mes = "Create job successfull.",
                IsSuccess = true,
            });
        }

        [HttpPost]
        [Route("/admin/job/UpdateJob")]
        public JsonResult UpdateJob(Job.Req job, int rowIndex)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", job.Id },
                { "@Title", job.Title },
                { "@Location", job.Location},
                { "@Content", job.Content },
                { "@EndDate", job.EndDate },
                { "@SalaryMin", job.SalaryMin },
                { "@SalaryMax",  job.SalaryMax },
                { "@LevelId",  job.LevelId }
            };
            var ls = DbContext.Instance.Exec<List<Job.Res>>(DbStore.UpdateJob, param);
            if (ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Update Job Fail.",
                    IsSuccess = false,
                });
            }
            var result = ls.FirstOrDefault();
            result.RowIndex = rowIndex;
            return Json(new DbContext.Result<Job.Res>
            {
                Data = result,
                Mes = "Update Job Successfull.",
                IsSuccess = true,
            });
            //return PartialView(@"~/Areas/Admin/Views/Shared/Partials/job/_PartialRowjob.cshtml", result);
        }

        [HttpGet]
        public JsonResult GetJobById(int id)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", 1 },
            };
            var result = DbContext.Instance.Exec<List<Job.Res>>(DbStore.GetJobById, param).FirstOrDefault();

            return Json(result);
        }

        [HttpPost]
        [Route("/admin/job/ChangeJobStatus")]
        public JsonResult ChangeJobStatus(Job.Req job)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", job.Id },
                { "@Status", job.Status },
            };
            var result = DbContext.Instance.Exec<List<Job.Res>>(DbStore.ChangeJobStatus, param).FirstOrDefault();

            return Json(new DbContext.Result<Job.Res>
            {
                Data = result,
                Mes = "Successfull.",
                IsSuccess = true,
            });
        }
    }
}