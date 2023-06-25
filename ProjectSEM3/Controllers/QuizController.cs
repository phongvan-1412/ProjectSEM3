using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ProjectSEM3.Controllers
{
    public class QuizController : Controller
    {
        public ActionResult Exam(int contestant)
        {
            try
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "@ContestId", contestant}
                };

                List<Exam.Res> exam = DbContext.Instance.Exec<List<Exam.Res>>(DbStore.GetExamnById, param);
                ViewData["Exam"] = exam.FirstOrDefault();
                ViewData["Contestant"] = contestant;
            }
            catch (Exception)
            {
                TempData["loadExamFailed"] = "Sorry! Page doesn't exist.";
            }
            return View();
        }
        [HttpPost]
        public ActionResult StartQuiz(int? examId, int? contestantId)
        {
            return RedirectToAction("Quiz", "Quiz", new { examId = examId, contestantId = contestantId });
        }
        public ActionResult Quiz(int? examId, int? contestantId)
        {
            try
            {
                ViewData["lstExam"] = GetData(examId);
                ViewData["contestantId"] = contestantId;
            }
            catch (Exception)
            {
                TempData["loadExamDetailFailed"] = "Sorry! Page doesn't exist.";
            }
            return View();
        }


        [HttpPost]
        public JsonResult Submit(string result, int contestantId)
        {
            var json = JsonConvert.DeserializeObject(result);
            List<Result> results = JsonConvert.DeserializeObject<List<Result>>(json.ToString());
            return Json(new { redirectToUrl = Url.Action("Home", "Index") });
        }

        public ContestantExam GetData(int? id)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@ExamId", id},
            };

            var examDetails = DbContext.Instance.Exec<List<ExamDetail.Res>>(DbStore.GetExamnDetailById, param);
            var result = new ContestantExam(examDetails);
            return result;
        }
    }
}