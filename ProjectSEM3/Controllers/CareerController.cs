﻿using ProjectSEM3.Models;
using ProjectSEM3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace ProjectSEM3.Controllers
{
    public class CareerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Jobs(string lstCountries)
        {
            return View();
        }

        public ActionResult ApplyCV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadCV(string job, string contName, string contEmail, string contPhone, HttpPostedFileBase cv)
        {
            var savePath = Path.Combine(Server.MapPath("~/Content/pdf"), cv.FileName);
            cv.SaveAs(savePath);
            var path = "https://localhost:44376/Content/pdf/" + cv.FileName;

            var date = DateTime.UtcNow;

            var param = new Dictionary<string, dynamic>
            {
                { "@JobId", 1 },
                { "@Name", contName},
                { "@Email", contEmail },
                { "@Contact", contPhone },
                { "@Cv", path },
                { "@PostedDate", date }
            };

            DbContext.Instance.Exec<List<Contestant.Req>>(DbStore.InsertCV, param);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string lstCountries)
        {
            return RedirectToAction("Jobs", lstCountries);
        }
    }
}