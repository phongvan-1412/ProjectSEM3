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
            List<string> answerKnowledge;
            List<string> correctAnswerKnowledge;
            int pointMath = 0;
            int pointKnowledge = 0;
            int pointComputer = 0;
            int totalPoint = 0;

            foreach (var item in knowledge)
            {
                bool checkKnowledge = false;
                int checkMulti = 0;

                if(item.TypeId != 3)
                {
                    if(item.TypeId == 2)
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
                        answerKnowledge = JsonConvert.DeserializeObject<List<string>>(item.Answer);
                        correctAnswerKnowledge = JsonConvert.DeserializeObject<List<string>>(item.CorrectAnwser);

                        foreach (var ele in answerKnowledge)
                        {
                            foreach (var correctEle in correctAnswerKnowledge)
                            {
                                if (item.TypeId == 1)
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
                                    if (checkKnowledge || checkMulti == correctAnswerKnowledge.Count())
                                    {
                                        pointKnowledge += item.Point;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (var item in math)
            {
                bool checkMath = false;
                int checkMulti = 0;
                List<string> answerMath = JsonConvert.DeserializeObject<List<string>>(item.Answer);
                List<string> correctAnswerMath = JsonConvert.DeserializeObject<List<string>>(item.CorrectAnwser);

                foreach (var ele in answerMath)
                {
                    foreach (var correctEle in correctAnswerMath)
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
                            if (checkMath || checkMulti==correctAnswerMath.Count())
                            {
                                pointMath += item.Point;
                            }
                        }
                        else if(item.TypeId == 3)
                        {
                            if (item.Answer.Equals(item.CorrectAnwser))
                            {
                                checkMath = true;
                                pointMath += item.Point;
                            }
                        }
                    }
                }
            }
            
            foreach (var item in computer)
            {
                bool checkComputer = false;
                List<string> answerComputer = JsonConvert.DeserializeObject<List<string>>(item.Answer);
                List<string> correctAnswerComputer = JsonConvert.DeserializeObject<List<string>>(item.CorrectAnwser);
                //if (item.Answer.Equals(item.CorrectAnwser))
                //{
                //    checkComputer = true;
                //    pointComputer += item.Point;
                //}
            }

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