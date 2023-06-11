using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.EnterpriseServices.CompensatingResourceManager;
using ProjectSEM3.Utils;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class ContestantController : Controller
    {
        // GET: Admin/Contestant
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/admin/contestant/NewContestant")]
        public JsonResult NewContestant(Contestant.Req req, int levelId,int cvId)
        {
            try
            {
                var check = DbContext.Instance.Exec<List<IsExistsEmail>>(DbStore.IsEmailIsExsists, new Dictionary<string, dynamic> { { "@Email", req.Email } }).FirstOrDefault();
                List<Contestant.Res> contestResult = null;
                Contestant.Res contest = null;
                if (check.IsExists)
                {
                    contestResult = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.GetContestantByEmail, new Dictionary<string, dynamic> { { "@Email", req.Email } });

                    if (contestResult is null)
                    {
                        return Json(new DbContext.Result
                        {
                            Mes = "Duplicate Email.",
                            IsSuccess = false,
                        });
                    }
                    else
                        contest = contestResult.FirstOrDefault();
                }


                if (contest == null)
                {
                    var password = AppRandom.String().EncryptPassword();
                    var contestParam = new Dictionary<string, dynamic>
                {
                    { "@Name", req.Name },
                    { "@Email", req.Email},
                    { "@Password", password },
                    { "@Contact", req.Contact },
                    { "@Address", req.Address },
                    { "@Cv", req.Cv },
                };

                    contestResult = DbContext.Instance.Exec<List<Contestant.Res>>(DbStore.InsertContestant, contestParam);

                    if (contestResult is null)
                    {
                        return Json(new DbContext.Result
                        {
                            Mes = "Create contestant fail.",
                            IsSuccess = false,
                        });
                    }
                    else
                        contest = contestResult.FirstOrDefault();
                }

                var startTime = DateTime.UtcNow;
                var endTime = startTime.AddDays(1);
                var lateTime = startTime.AddMinutes(30);
                var examParam = new Dictionary<string, dynamic>
                {
                    { "@ContestantId", contest.Id },
                    { "@StartTime", startTime },
                    { "@EndTime", endTime },
                    { "@LateTime", lateTime }
                };

                var examResult = DbContext.Instance.Exec<List<Exam.Res>>(DbStore.InsertExam, examParam);
                var exam = examResult.FirstOrDefault();
                if (examResult is null)
                {
                    return Json(new DbContext.Result
                    {
                        Mes = "Create Exam fail.",
                        IsSuccess = true,
                    });
                }

                var questionParam = new Dictionary<string, dynamic>
                {
                    { "@IdLevel", levelId }
                };

                    var questionResult = DbContext.Instance.Exec<List<Question.Res>>(DbStore.GetQuestions, questionParam);

                    var examTypeParam = new Dictionary<string, dynamic>
                {
                    { "@Status", 1 }
                };

                var examTypeResult = DbContext.Instance.Exec<List<Models.Entities.Type.Res>>(DbStore.GetAllTypes, examTypeParam);

                foreach (var type in examTypeResult)
                {
                    var randomQuestions = questionResult.Where(x => x.LevelId == levelId && x.ExamTypeId == type.Id).Random(5);
                    foreach (var question in randomQuestions)
                    {
                        var examDetailParam = new Dictionary<string, dynamic>
                        {
                            { "@QuestionId",question.Id},
                            { "@ExamId", exam.Id }
                        };
                        DbContext.Instance.Exec<List<Models.Entities.Type.Res>>(DbStore.InsertExamDetail, examDetailParam);
                    }
                }

                var cvParam = new Dictionary<string, dynamic>
                {
                    { "@Id",cvId},
                    { "@Status", (int)CvStatus.Accepted }
                };

                var cvResult = DbContext.Instance.Exec<List<CV.Res>>(DbStore.UpdateCv, cvParam);

                if (cvResult is null)
                {
                    return Json(new DbContext.Result
                    {
                        Mes = "Update Exam fail.",
                        IsSuccess = true,
                    });
                }

                return Json(new DbContext.Result<CV.Res>
                {
                    Data = cvResult.FirstOrDefault(),
                    Mes = "Create Exam successfull.",
                    IsSuccess = true,
                });
            }
            catch (Exception)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Something Wrong in server.",
                    IsSuccess = true,
                });
            }
        }
    }
}