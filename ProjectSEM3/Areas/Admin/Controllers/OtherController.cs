using ProjectSEM3.Models.Entities;
using ProjectSEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    public class OtherController : Controller
    {
        // GET: Admin/Other
        public ActionResult Index()
        {
            ViewBag.ExamType = DbContext.Instance.Exec<List<ProjectSEM3.Models.Entities.Type.Res>>(DbStore.GetAllTypes);
            ViewBag.Level = DbContext.Instance.Exec<List<Level.Res>>(DbStore.GetAllLevels);
            ViewBag.QuestionType = DbContext.Instance.Exec<List<QuestionType.Res>>(DbStore.GetAllQuestionTypes);
            return View();
        }

        #region Exam Type
        [HttpPost]
        [Route("/admin/other/NewExamType")]
        public JsonResult NewExamType(Models.Entities.Type.Req req)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Name", req.Name },
            };

            var result = DbContext.Instance.Exec<List<Models.Entities.Type.Res>>(DbStore.InsertType, param).FirstOrDefault();
            return Json(new DbContext.Result<Models.Entities.Type.Res>
            {
                Data = result,
                Mes = "Create Exam Type successfull.",
                IsSuccess = true,
            });
        }

        [HttpPost]
        [Route("/admin/other/UpdateExamType")]
        public JsonResult UpdateExamType(Models.Entities.Type.Req req, int rowIndex)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", req.Id },
                { "@name", req.Name },
            };

            var ls = DbContext.Instance.Exec<List<Models.Entities.Type.Res>>(DbStore.UpdateType, param);
            if (ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Update Exam Type Fail.",
                    IsSuccess = false,
                });
            }
            var result = ls.FirstOrDefault();
            result.RowIndex = rowIndex;
            return Json(new DbContext.Result<Models.Entities.Type.Res>
            {
                Data = result,
                Mes = "Update Exam Type Successfull.",
                IsSuccess = true,
            });
        }

        [HttpPost]
        [Route("/admin/other/ChangeExamTypeStatus")]
        public JsonResult ChangeExamTypeStatus(Models.Entities.Type.Req req)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", req.Id },
                { "@Status", req.Status },
            };

            var ls = DbContext.Instance.Exec<List<Models.Entities.Type.Res>>(DbStore.ChangeTypeStatus, param);
            if (ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Update Exam Type Fail.",
                    IsSuccess = false,
                });
            }

            var result = ls.FirstOrDefault();

            return Json(new DbContext.Result<Models.Entities.Type.Res>
            {
                Data = result,
                Mes = "Update Exam Type Successfull.",
                IsSuccess = true,
            });
        }
        #endregion
        #region Level
        [HttpPost]
        [Route("/admin/other/NewLevel")]
        public JsonResult NewLevel(Level.Req req)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@name", req.Name },
            };

            var result = DbContext.Instance.Exec<List<Level.Res>>(DbStore.InsertLevel, param).FirstOrDefault();
            return Json(new DbContext.Result<Level.Res>
            {
                Data = result,
                Mes = "Create Level successfull.",
                IsSuccess = true,
            });
        }

        [HttpPost]
        [Route("/admin/other/UpdateLevel")]
        public JsonResult UpdateLevel(Level.Req req, int rowIndex)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", req.Id },
                { "@name", req.Name },
            };

            var ls = DbContext.Instance.Exec<List<Level.Res>>(DbStore.UpdateLevel, param);
            if (ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Update Level Fail.",
                    IsSuccess = false,
                });
            }
            var result = ls.FirstOrDefault();
            result.RowIndex = rowIndex;
            return Json(new DbContext.Result<Level.Res>
            {
                Data = result,
                Mes = "Update Level Successfull.",
                IsSuccess = true,
            });
        }

        [HttpPost]
        [Route("/admin/other/ChangeLevelStatus")]
        public JsonResult ChangeLevelStatus(Level.Req req)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", req.Id },
                { "@Status", req.Status },
            };
            var ls = DbContext.Instance.Exec<List<Level.Res>>(DbStore.ChangeLevelStatus, param);
            if (ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Update Level Fail.",
                    IsSuccess = false,
                });
            }

            var result = ls.FirstOrDefault();

            return Json(new DbContext.Result<Level.Res>
            {
                Data = result,
                Mes = "Update Level Successfull.",
                IsSuccess = true,
            });
        }
        #endregion
        #region Question Type
        [HttpPost]
        [Route("/admin/other/NewQuestionType")]
        public JsonResult NewQuestionType(QuestionType.Req req)
        {
            var param = new Dictionary<string, dynamic>
            {
                 { "@name", req.Name },
            };

            var result = DbContext.Instance.Exec<List<QuestionType.Res>>(DbStore.InsertQuestionType, param).FirstOrDefault();
            return Json(new DbContext.Result<QuestionType.Res>
            {
                Data = result,
                Mes = "Create QuestionType Type successfull.",
                IsSuccess = true,
            });
        }

        [HttpPost]
        [Route("/admin/other/UpdateQuestionType")]
        public JsonResult UpdateQuestionType(QuestionType.Req req, int rowIndex)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", req.Id },
                { "@name", req.Name },
            };

            var ls = DbContext.Instance.Exec<List<QuestionType.Res>>(DbStore.UpdateQuestionType, param);
            if (ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Update Exam Type Fail.",
                    IsSuccess = false,
                });
            }
            var result = ls.FirstOrDefault();
            result.RowIndex = rowIndex;
            return Json(new DbContext.Result<QuestionType.Res>
            {
                Data = result,
                Mes = "Update Question Type Type Successfull.",
                IsSuccess = true,
            });
        }

        [HttpPost]
        [Route("/admin/other/ChangeQuestionTypeStatus")]
        public JsonResult ChangeQuestionTypeStatus(QuestionType.Req req)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "@Id", req.Id },
                { "@Status", req.Status },
            };

            var ls = DbContext.Instance.Exec<List<QuestionType.Res>>(DbStore.ChangeQuestionTypeStatus, param);
            if (ls == null)
            {
                return Json(new DbContext.Result
                {
                    Mes = "Update Question Type Type Fail.",
                    IsSuccess = false,
                });
            }

            var result = ls.FirstOrDefault();

            return Json(new DbContext.Result<QuestionType.Res>
            {
                Data = result,
                Mes = "Update Question Type Type Successfull.",
                IsSuccess = true,
            });
        }
        #endregion
    }
}