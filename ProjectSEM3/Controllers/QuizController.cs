using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using static ProjectSEM3.Models.Entities.Email;

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

                List<Models.Entities.Exam.Res> exam = DbContext.Instance.Exec<List<Models.Entities.Exam.Res>>(DbStore.GetExamnById, param);
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
            var param = new Dictionary<string, dynamic>
                {
                    { "@ContestId", contestantId},
                };

            var exam = DbContext.Instance.Exec<List<Models.Entities.Exam.Res>>(DbStore.GetExamnById, param);

            return RedirectToAction("Quiz", "Quiz", new { examId = examId, contestantId = contestantId, endTime = exam.FirstOrDefault().EndTime });
        }
        public ActionResult Quiz(int? examId, int? contestantId, DateTime? endTime)
        {
            try
            {
                if (examId == null || contestantId == null || endTime == null)
                {
                    TempData["loadExamDetailFailed"] = "Sorry! Page doesn't exist.";
                }

                ViewData["lstExam"] = GetData(examId);
                ViewData["contestantId"] = contestantId;
                ViewData["endTime"] = endTime;
                
            }
            catch (Exception)
            {
                TempData["loadExamDetailFailed"] = "Sorry! Page doesn't exist.";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Submit(ContestantExam result, int contestantId)
        {
            var examId = result.Math.FirstOrDefault().ExamId;
            int pointMath = CheckAnswer(result.Math);
            int pointKnowledge = CheckAnswer(result.Knowledge);
            int pointComputer = CheckAnswer(result.Computer);
         
            DbContext.Instance.Exec<ExamDetail.Res>(DbStore.UpdatePointForExam, new Dictionary<string, dynamic>
            {
                { "@ExamId", examId},
                { "@KnowledgePoint", pointKnowledge},
                { "@MathPoint", pointMath},
                { "@ComputerPoint", pointComputer},
            });

            int totalPoint = pointMath + pointKnowledge + pointComputer;

            // send email: pass/fail

            return RedirectToAction("TestResult", "Quiz");
        }
        public ActionResult TestResult()
        {
            return View();
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

        private int CheckAnswer(List<ExamDetail.Res> data)
        {
            var point = 0;

            foreach (var item in data)
            {
                DbContext.Instance.Exec<ExamDetail.Res>(DbStore.UpdateAnswerForExamDetail, new Dictionary<string, dynamic>
                {
                    { "@ExamId", item.ExamId},
                    { "@QuestionId", item.QuestionId},
                    { "@Answer",item.Answer},
                });

                if (item.TypeId == 1 && item.IsMultiAnwser)
                {
                    var answer = JsonConvert.DeserializeObject<List<string>>(item.Answer);
                    var correctAnswer = JsonConvert.DeserializeObject<List<string>>(item.CorrectAnwser);
                    answer.Sort();
                    correctAnswer.Sort();

                    var jsonAnswer = JsonConvert.SerializeObject(answer);
                    var jsonCorrectAnswer = JsonConvert.SerializeObject(correctAnswer);
                    if (jsonAnswer.Equals(jsonCorrectAnswer))
                        point += item.Point;
                }
                else
                {
                    if (item.Answer.Equals(item.CorrectAnwser))
                        point += item.Point;
                }
            }

            return point;
        }
    }
}
