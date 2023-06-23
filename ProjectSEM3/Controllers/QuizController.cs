using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using static ProjectSEM3.Models.Entities.Email;

namespace ProjectSEM3.Controllers
{
    public class QuizController : Controller
    {
        public ActionResult Exam()
        {
            var contestants = Session["Contestant"] as List<Contestant.Res>;
            try
            {
                Contestant.Res contestant = contestants.FirstOrDefault();

                var param = new Dictionary<string, dynamic>
                {
                    { "@ContestId", contestant.Id}
                };

                List<Models.Entities.Exam.Res> exam = DbContext.Instance.Exec<List<Models.Entities.Exam.Res>>(DbStore.GetExamnById, param);
                ViewData["Exam"] = exam.FirstOrDefault();
            }
            catch (Exception)
            {
                TempData["loadExamFailed"] = "Sorry! Page doesn't exist.";
            }
            return View();
        }

        [HttpPost]
        public JsonResult StartQuiz(int id)
        {
            return Json(new { redirectToUrl = Url.Action("Quiz_Knowledge", "Quiz"), examId = id });
        }
        public ActionResult Quiz_Knowledge(int? examId)
        {
            try
            {
                ViewData["lstKnowledge"] = GetData(examId).Knowledge;
                ViewData["examId"] = examId;
            }
            catch (Exception)
            {
                TempData["loadExamDetailFailed"] = "Sorry! Page doesn't exist.";
            }
            return View();
        }

        public ActionResult Quiz_Math(int? examId)
        {
            try
            {
                ViewData["lstMath"] = GetData(examId).Math;
                ViewData["examId"] = examId;
            }
            catch (Exception)
            {
                TempData["loadExamDetailFailed"] = "Sorry! Page doesn't exist.";
            }
            return View();
        }
        public ActionResult Quiz_Computer(int? examId)
        {
            try
            {
                ViewData["lstComputer"] = GetData(examId).Computer;
                ViewData["examId"] = examId;
            }
            catch (Exception)
            {
                TempData["loadExamDetailFailed"] = "Sorry! Page doesn't exist.";
            }
            return View();
        }


        [HttpPost]
        public JsonResult SubmitKnowledge(string result, int examId)
        {
            var json = JsonConvert.DeserializeObject(result);
            List<Result> results = JsonConvert.DeserializeObject<List<Result>>(json.ToString());

            return Json(new { redirectToUrl = Url.Action("Quiz_Math", "Quiz"), examId = examId });
        }

        [HttpPost]
        public JsonResult SubmitMath(string result, int examId)
        {
            //var json = JsonConvert.DeserializeObject(result);
            //List<Models.Entities.Result> results = JsonConvert.DeserializeObject<List<Models.Entities.Result>>(json.ToString());

            //Console.WriteLine(results);
            return Json(new { redirectToUrl = Url.Action("Quiz_Computer", "Quiz"), examId = examId });
        }

        [HttpPost]
        public JsonResult SubmitComputer(string result)
        {
            var json = JsonConvert.DeserializeObject(result);
            List<Models.Entities.Result> results = JsonConvert.DeserializeObject<List<Models.Entities.Result>>(json.ToString());

            Console.WriteLine(results);
            return Json("success");
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

        //public ActionResult ViewQuiz(int id = 60)
        //{

        //    var param = new Dictionary<string, dynamic>
        //    {
        //        { "@ExamId", id},
        //    };

        //    var examDetails = DbContext.Instance.Exec<List<ExamDetail.Res>>(DbStore.GetExamnDetailById, param);
        //    var result = new ContestantExam(examDetails);
        //    return View(result);
        //}
    }
}