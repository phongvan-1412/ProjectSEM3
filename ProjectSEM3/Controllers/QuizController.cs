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
        // GET: Quiz
        public ActionResult Quiz_Knowledge()
        {
            ViewData["lstQuest"] = GetData();
            List<Question.Req> temp = DbContext.Instance.Exec<List<Question.Req>>("select * from question for json path");
            return View(temp);
        }

        [HttpPost]
        public ActionResult Submit(string result)
        {
            var json = JsonConvert.DeserializeObject(result);
            List<Result> results = JsonConvert.DeserializeObject<List<Result>>(json.ToString());
           
            Console.WriteLine(results);
            return RedirectToAction("Index", "Home");
        }

        public JsonResult GetData()
        {
            var lstQuest = DbContext.Instance.Exec<List<Question.Req>>("select * from question for json path");
            return Json(lstQuest);
        }
    }
}