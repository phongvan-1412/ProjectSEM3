using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.IO;

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
        [Route("/admin/question/NewQuestion")]
        public JsonResult NewQuestion(Question.Req req)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@IdType", req.IdType },
                { "@IdLevel", req.IdLevel },
                { "@Content", req.Content },
                { "@Point", req.Point },
                { "@Options", req.Options },
                { "@CorrectAnwser", req.CorrectAnwser },
                { "@IsMultiAnwser", req.IsMultiAnwser },
            };

            var result = DbContext.Instance.Exec<List<Question.Res>>(DbStore.InsertQuestion, param).FirstOrDefault();
            return Json(new DbContext.Result<Question.Res>
            {
                Data = result,
                Mes = "Create Question successfull.",
                IsSuccess = true,
            });
        }


        [HttpPost]
        [Route("/admin/hr/UpdateQuestion")]
        public JsonResult UpdateQuestion(Question.Req req, int rowIndex)
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
     
            var ls = DbContext.Instance.Exec<List<Question.Res>>(DbStore.UpdateQuestion, param);
            if (ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Fail.",
                    IsSuccess = false,
                });
            }
            var result = ls.FirstOrDefault();
            result.RowIndex = rowIndex;
            return Json(new DbContext.Result<Question.Res>
            {
                Data = result,
                Mes = "Successfull.",
                IsSuccess = true,
            });
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