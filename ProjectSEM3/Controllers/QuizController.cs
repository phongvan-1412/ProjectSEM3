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
        public ActionResult Exam(int contestant)
        {
            try
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "@ContestId", contestant}
                };

                List<Exam.Res> exam = DbContext.Instance.Exec<List<Exam.Res>>(DbStore.GetExamnById, param);
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
            return RedirectToAction("Quiz", "Quiz", new { examId = examId, contestantId = contestantId });
        }
        public ActionResult Quiz(int? examId, int? contestantId)
        {
            try
            {
                ViewData["lstExam"] = GetData(examId);
                ViewData["contestantId"] = contestantId;
            }
            catch (Exception)
            {
                TempData["loadExamDetailFailed"] = "Sorry! Page doesn't exist.";
            }
            return View();
        }


        [HttpPost]
        public JsonResult Submit(ContestantExam result, int contestantId)
        {
            var math = result.Math;
            var knowledge = result.Knowledge;
            var computer = result.Computer;
            List<int> questions = new List<int>();
            List<string> answer;
            List<string> correctAnswer;
            int pointMath = 0;
            int pointKnowledge = 0;
            int pointComputer = 0;
            int totalPoint = 0;

            foreach (var item in knowledge)
            {
                bool checkKnowledge = false;
                int checkMulti = 0;

                DbContext.Instance.Exec<ExamDetail.Res>(DbStore.UpdateAnswerForExamDetail, new Dictionary<string, dynamic>
                 {
                     { "@ExamId", item.ExamId},
                     { "@QuestionId", item.QuestionId},
                     { "@Answer",item.Answer},
                 });

                if (item.TypeId != 1)
                {
                    if (item.Answer.Equals(item.CorrectAnwser))
                    {
                        checkKnowledge = true;
                    }
                    if (checkKnowledge)
                    {
                        pointKnowledge += item.Point;
                    }
                }
                else
                {
                    answer = JsonConvert.DeserializeObject<List<string>>(item.Answer);
                    correctAnswer = JsonConvert.DeserializeObject<List<string>>(item.CorrectAnwser);

                    foreach (var ele in answer)
                    {
                        foreach (var correctEle in correctAnswer)
                        {
                            if (!item.IsMultiAnwser)
                            {
                                if (ele.Equals(correctEle))
                                {
                                    checkKnowledge = true;
                                }
                            }
                            else
                            {
                                if (ele.Equals(correctEle))
                                {
                                    checkMulti += 1;
                                }
                            }
                            if (checkKnowledge || checkMulti == correctAnswer.Count())
                            {
                                pointKnowledge += item.Point;
                            }
                        }
                    }
                }
            }
            foreach (var item in math)
            {
                bool checkMath = false;
                int checkMulti = 0;
                DbContext.Instance.Exec<ExamDetail.Res>(DbStore.UpdateAnswerForExamDetail, new Dictionary<string, dynamic>
                 {
                     { "@ExamId", item.ExamId},
                     { "@QuestionId", item.QuestionId},
                     { "@Answer",item.Answer},
                 });

                if (item.TypeId != 1)
                {
                    if (item.Answer.Equals(item.CorrectAnwser))
                    {
                        checkMath = true;
                    }
                    if (checkMath)
                    {
                        pointMath += item.Point;
                    }
                }
                else
                {
                    answer = JsonConvert.DeserializeObject<List<string>>(item.Answer);
                    correctAnswer = JsonConvert.DeserializeObject<List<string>>(item.CorrectAnwser);

                    foreach (var ele in answer)
                    {
                        foreach (var correctEle in correctAnswer)
                        {
                            if (item.TypeId == 1)
                            {
                                if (!item.IsMultiAnwser)
                                {
                                    if (ele.Equals(correctEle))
                                    {
                                        checkMath = true;
                                    }
                                }
                                else
                                {
                                    if (ele.Equals(correctEle))
                                    {
                                        checkMulti += 1;
                                    }
                                }
                                if (checkMath || checkMulti == correctAnswer.Count())
                                {
                                    pointMath += item.Point;
                                }
                            }
                        }
                    }
                }
            }
            foreach (var item in computer)
            {
                bool checkComputer = false;
                int checkMulti = 0;
                DbContext.Instance.Exec<ExamDetail.Res>(DbStore.UpdateAnswerForExamDetail, new Dictionary<string, dynamic>
                 {
                     { "@ExamId", item.ExamId},
                     { "@QuestionId", item.QuestionId},
                     { "@Answer",item.Answer},
                 });

                if (item.TypeId != 1)
                {
                    if (item.Answer.Equals(item.CorrectAnwser))
                    {
                        checkComputer = true;
                    }
                    if (checkComputer)
                    {
                        pointComputer += item.Point;
                    }
                }
                else
                {
                    answer = JsonConvert.DeserializeObject<List<string>>(item.Answer);
                    correctAnswer = JsonConvert.DeserializeObject<List<string>>(item.CorrectAnwser);

                    foreach (var ele in answer)
                    {
                        foreach (var correctEle in correctAnswer)
                        {
                            if (item.TypeId == 1)
                            {
                                if (!item.IsMultiAnwser)
                                {
                                    if (ele.Equals(correctEle))
                                    {
                                        checkComputer = true;
                                    }
                                }
                                else
                                {
                                    if (ele.Equals(correctEle))
                                    {
                                        checkMulti += 1;
                                    }
                                }
                                if (checkComputer || checkMulti == correctAnswer.Count())
                                {
                                    pointComputer += item.Point;
                                }
                            }
                        }
                    }
                }
            }

            //foreach (var item in knowledge)
            //{
            //    DbContext.Instance.Exec<ExamDetail.Res>(DbStore.UpdateAnswerForExamDetail, new Dictionary<string, dynamic>
            //     {
            //         { "@ExamId", item.ExamId},
            //         { "@QuestionId", item.QuestionId},
            //         { "@Answer",item.Answer},
            //     });
            //}
            var point = DbContext.Instance.Exec<ExamDetail.Res>(DbStore.UpdatePointForExam, new Dictionary<string, dynamic>
            {
                { "@ExamId", math.FirstOrDefault().ExamId},
                { "@KnowledgePoint", pointKnowledge},
                { "@MathPoint", pointMath},
                { "@ComputerPoint", pointComputer},
            });

            totalPoint = pointMath + pointKnowledge + pointComputer;
            return Json(new { redirectToUrl = Url.Action("Home", "Index") });
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
    }
}