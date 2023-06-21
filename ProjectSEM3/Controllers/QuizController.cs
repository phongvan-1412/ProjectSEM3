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

                List<Exam.Res> exam = DbContext.Instance.Exec<List<Exam.Res>>(DbStore.GetExamnById, param);
                ViewData["Exam"] = exam.FirstOrDefault();
            }
            catch (Exception)
            {
                TempData["loadExamFailed"] = "Sorry! Page doesn't exist.";
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult StartQuiz(int id)
        {
            return RedirectToAction("Quiz_Knowledge", new { examId = id });
        }
        public ActionResult Quiz_Knowledge(int examId)
        {
            ViewData["lstKnowledge"] = GetData(examId);
            return View();
        }
        public ActionResult Quiz_Math()
        {
            //List<Question.Req> lst = Models.DbContext.Instance.Exec<List<Question.Req>>("select * from question for json path");
            //QuestionView temp;
            //List<QuestionView> lstTemp = new List<QuestionView>();
            //foreach (var item in lst)
            //{
            //    List<string> a = item.Options.Split(',').ToList();
            //    List<string> b = new List<string>();
            //    foreach (var i in a)
            //    {
            //        var c = i.Replace("[", "");
            //        var d = c.Replace("\"", "");
            //        var e = d.Replace("\\", "");
            //        var f = e.Replace("]", "");

            //        Console.WriteLine(f);
            //        b.Add(f);
            //    }
            //    temp = new QuestionView
            //    {
            //        Id = item.Id,
            //        IdType = item.TypeId,
            //        IdLevel = item.LevelId,
            //        Content = item.Content,
            //        Point = item.Point,
            //        Options = b,
            //        CorrectAnwser = item.CorrectAnwser,
            //        IsMultiAnwser = item.IsMultiAnwser,
            //        Status = item.Status,
            //    };
            //    lstTemp.Add(temp);
            //}
            //ViewData["lstMath"] = lstTemp; 
            //return View(lstTemp);
            //ViewData["lstMath"] = GetData();
            return View();
        }
        public ActionResult Quiz_Computer()
        {
            ViewData["lstComputer"] = GetData(1);
            return View();
        }


        [HttpPost]
        public JsonResult SubmitKnowledge(string result)
        {
            var json = JsonConvert.DeserializeObject(result);
            List<Result> results = JsonConvert.DeserializeObject<List<Result>>(json.ToString());

            return Json("success");
        }

        [HttpPost]
        public JsonResult SubmitMath(string result)
        {
            var json = JsonConvert.DeserializeObject(result);
            List<Models.Entities.Result> results = JsonConvert.DeserializeObject<List<Models.Entities.Result>>(json.ToString());

            Console.WriteLine(results);
            return Json("success");
        }

        [HttpPost]
        public JsonResult SubmitComputer(string result)
        {
            var json = JsonConvert.DeserializeObject(result);
            List<Models.Entities.Result> results = JsonConvert.DeserializeObject<List<Models.Entities.Result>>(json.ToString());

            Console.WriteLine(results);
            return Json("success");
        }

        public JsonResult GetData(int id)
        {
            //var lstQuest = Models.DbContext.Instance.Exec<List<Question.Req>>("select * from question");

            var param = new Dictionary<string, dynamic>
            {
                { "@ExamId", id},
            };

            var examDetails = DbContext.Instance.Exec<List<ExamDetail.Res>>(DbStore.GetExamnDetailById, param);
            var result = new ContestantExam(examDetails);
            //Session["result"] = result;
            return Json(result);
        }
    }
}