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
        public ActionResult Quiz_Knowledge()
        {
            ViewData["lstKnowledge"] = GetData();
            return View();
        }
        public ActionResult Quiz_Math()
        {
            List<Question.Req> lst = Models.DbContext.Instance.Exec<List<Question.Req>>("select * from question for json path");
            QuestionView temp;
            List<QuestionView> lstTemp = new List<QuestionView>();
            foreach (var item in lst)
            {
                List<string> a = item.Options.Split(',').ToList();
                List<string> b = new List<string>();
                foreach (var i in a)
                {
                    var c = i.Replace("[", "");
                    var d = c.Replace("\"", "");
                    var e = d.Replace("\\", "");
                    var f = e.Replace("]", "");

                    Console.WriteLine(f);
                    b.Add(f);
                }
                temp = new QuestionView
                {
                    Id = item.Id,
                    IdType = item.TypeId,
                    IdLevel = item.LevelId,
                    Content = item.Content,
                    Point = item.Point,
                    Options = b,
                    CorrectAnwser = item.CorrectAnwser,
                    IsMultiAnwser = item.IsMultiAnwser,
                    Status = item.Status,
                };
                lstTemp.Add(temp);
            }
            ViewData["lstMath"] = lstTemp; 
            return View(lstTemp);
        }
        public ActionResult Quiz_Computer()
        {
            ViewData["lstComputer"] = GetData();
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

        public JsonResult GetData()
        {
            var lstQuest = Models.DbContext.Instance.Exec<List<Question.Req>>(DbStore.GetQuestions);
            return Json(lstQuest);  
        }
    }
}