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
            ViewData["lstMath"] = GetData();
            return View();
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
            List<Result> results = JsonConvert.DeserializeObject<List<Result>>(json.ToString());

            Console.WriteLine(results);
            return Json("success");
        }

        [HttpPost]
        public JsonResult SubmitComputer(string result)
        {
            var json = JsonConvert.DeserializeObject(result);
            List<Result> results = JsonConvert.DeserializeObject<List<Result>>(json.ToString());

            Console.WriteLine(results);
            return Json("success");
        }

        public JsonResult GetData()
        {
            var lstQuest = DbContext.Instance.Exec<List<Question.Req>>("select * from question for json path");
            return Json(lstQuest);
        }
    }
}