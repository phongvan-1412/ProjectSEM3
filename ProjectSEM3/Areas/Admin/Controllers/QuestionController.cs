using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Admin/Question
        public ActionResult Index()
        {
            var result = DbContext.Instance.Exec<List<Question.Res>>(DbStore.GetQuestions);

            var param = new Dictionary<string, dynamic>
            {
                { "@Status", 1 }
            };
            ViewBag.Types = DbContext.Instance.Exec<List<Type.Res>>(DbStore.GetAllTypes, param);
            ViewBag.Levels = DbContext.Instance.Exec<List<Level.Res>>(DbStore.GetAllLevels, param);
            return View(result);
        }

        [HttpPost]
        public ActionResult NewQuestion(Question.Req quest)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@IdType", 1 },
                { "@IdLevel", 1 },
                { "@Content", "111111" },
                { "@Point", 1 },
                { "@A", "111111" },
                { "@B", "111111" },
                { "@C", "111111" },
                { "@D", "111111" },
                { "@CorrectAnwser", "A" },
                { "@IsMultiAnwser", 0 },
            };

            var result = DbContext.Instance.Exec<List<Question.Res>>(DbStore.InsertQuestion, param).FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult UpdateQuestion(Question.Req hr)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", 1 },
                { "@IdType", 1 },
                { "@IdLevel", 1 },
                { "@Content", "111111" },
                { "@Point", 1 },
                { "@A", "111111" },
                { "@B", "111111" },
                { "@C", "111111" },
                { "@D", "111111" },
                { "@CorrectAnwser", "A" },
                { "@IsMultiAnwser", 0 },
                { "@Status", 1 },
            };
            var result = DbContext.Instance.Exec<List<Question.Res>>(DbStore.UpdateQuestion, param).FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult GetQuestById(int id)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", 1 },
            };
            var result = DbContext.Instance.Exec<List<Question.Res>>(DbStore.GetQuestionById, param).FirstOrDefault();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult GetHrs(Question.Req hr)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@IdType", 1 },
                { "@IdLevel", 1 },
                { "@Content", "111111" },
                { "@Point", 1 },
                { "@IsMultiAnwser", 0 },
            };
            var result = DbContext.Instance.Exec<List<Question.Res>>(DbStore.GetQuestions, param);

            return RedirectToAction(nameof(Index));
        }
    }
}