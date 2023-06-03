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
        public ActionResult Index()
        {
            ViewData["lstQuest"] = GetData();
            List<Question.Req> temp = DbContext.Instance.Exec<List<Question.Req>>("select * from question for json path");
            return View(temp);
        }

        [HttpPost]
        public JsonResult NextQuestion(string question, string answer)
        {
            var quest2 = DbContext.Instance.Exec<List<Question.Req>>("select * from question where cast(Content as varchar(max)) !=" + "'" + question + "'" + "for json path");

            return Json(quest2);
        }

        public JsonResult GetData()
        {
            var lstQuest = DbContext.Instance.Exec<List<Question.Req>>("select * from question for json path");
            return Json(lstQuest);
        }
    }
}