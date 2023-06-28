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
using static ProjectSEM3.Models.DbContext;
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
            // calculate: point
            var examId = result.Math.FirstOrDefault().ExamId;
            int pointMath = CheckAnswer(result.Math).SectionPoint;
            int pointKnowledge = CheckAnswer(result.Knowledge).SectionPoint;
            int pointComputer = CheckAnswer(result.Computer).SectionPoint;
            int totalPoint = CheckAnswer(result.Math).TotalPoint + CheckAnswer(result.Knowledge).TotalPoint + CheckAnswer(result.Computer).TotalPoint;
            int totalCorrectPoint = pointMath + pointKnowledge + pointComputer;
            float finalPoint = (totalCorrectPoint*100) / totalPoint;

            var paramContestant = new Dictionary<string, dynamic>
            {
                { "@Id", contestantId}
            };
            var contestant = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetContestantById, paramContestant);

            var paramExam = new Dictionary<string, dynamic>
            {
                { "@ContestId", contestantId}
            };
            var exam = DbContext.Instance.Exec<List<Models.Entities.Exam.Res>>(DbStore.GetExamnById, paramExam);

            DbContext.Instance.Exec<ExamDetail.Res>(DbStore.UpdatePointForExam, new Dictionary<string, dynamic>
            {
                { "@ExamId", examId},
                { "@KnowledgePoint", pointKnowledge},
                { "@MathPoint", pointMath},
                { "@ComputerPoint", pointComputer},
            });

            // send email: pass/fail
            var email = new Email();
            if (finalPoint >= 75)
            {
                var emailPassed = new Congratulations
                {
                    UserName = contestant.FirstOrDefault().Name,
                    Exam = exam.FirstOrDefault(),
                };
                email.SendCongratulations(emailPassed);
            }
            else
            {

                var emailReject = new QuizResult
                {
                    Name = contestant.FirstOrDefault().Name,
                    Email = contestant.FirstOrDefault().Email,
                    Point = finalPoint.ToString(),
                    JobLink = "https://localhost:44376/Career/GetJobById/" + contestant.FirstOrDefault().JobId,
                };

                email.SendFailedResult(emailReject);
            }

            var paramDeactive = new Dictionary<string, dynamic>
            {
                { "@Id", contestant.FirstOrDefault().Id },
                { "@Status", 3 },
            };

            DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.ChangeCvStatus, paramDeactive);

            return Json(new { redirectUrl = Url.Action("TestResult", "Quiz", new { finish = "finish" }) });
        }
        public ActionResult TestResult(string finish)
        {
            if(finish == null || finish == "")
            {
                TempData["pageNotExist"] = "Sorry! Page doesn't exist";
            }
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

        private Models.Entities.Result CheckAnswer(List<ExamDetail.Res> data)
        {
            var point = 0;
            var totalPoint = 0;
            Models.Entities.Result result;

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
                totalPoint += item.Point;
            }
            result = new Models.Entities.Result
            {
                SectionPoint = point,
                TotalPoint = totalPoint,
            };

            return result;
        }
    }
}
