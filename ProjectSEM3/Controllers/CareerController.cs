using Newtonsoft.Json;
using ProjectSEM3.Models;
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
            ViewData["lstCountries"] = Country();
            return View();
        }
        public ActionResult Jobs(string lstCountries, string input, int experience = 0, string profession = "")
        {
            var result = JobList();
            
            if (input != null || lstCountries != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "@Title", input},
                    { "@Location", lstCountries },
                    { "@Content", input},
                    { "@Qualification", input},
                    { "@LevelId", experience},
                    { "@Position", profession}
                };

                result = DbContext.Instance.Exec<List<Job.Res>>(DbStore.GetJobs, param);
            }

            var sessionData = Session["JobById"] as List<Job.Res>;
            Session.Remove("JobById");
            if(sessionData != null)
            {
                result = sessionData;
            }
            ViewData["lstCountries"] = Country();
            ViewData["lstLevels"] = Levels();
            ViewData["lstJobs"] = result;

            return View();
        }
        public ActionResult ApplyCV()
        {
            return View();
        }
        public ActionResult ApplySuccess()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadCV(int jobId, string contName, string contEmail, string contPhone, HttpPostedFileBase cv)
        {
            var savePath = Path.Combine(Server.MapPath("~/Content/pdf"), cv.FileName);
            cv.SaveAs(savePath);
            var path = "https://localhost:44376/Content/pdf/" + cv.FileName;

            var date = DateTime.UtcNow;

            var param = new Dictionary<string, dynamic>
            {
                { "@JobId", jobId },
                { "@Name", contName},
                { "@Email", contEmail },
                { "@Contact", contPhone },
                { "@Cv", path },
                { "@PostedDate", date }
            };

            DbContext.Instance.Exec<List<Contestant.Req>>(DbStore.InsertCV, param);
            return RedirectToAction("ApplySuccess");
        }

        [HttpPost]
        public ActionResult Search(string input = "", string lstCountries = "", int experience = 0, string profession = "")
        {
          
            return RedirectToAction("Jobs", "Career", new { lstCountries = lstCountries, input = input, experience = experience, profession = profession });
        }

        public ActionResult GetJobById(int id)
        {
            var result = DbContext.Instance.Exec<List<Job.Res>>(DbStore.GetJobById, new Dictionary<string, dynamic>
            {
                { "@Id", id }
            });

            Session["JobById"]  = result;
            return RedirectToAction("Jobs");
        }

        public List<Country> Country()
        {
            string countryText = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Content/countries.json")));
            string countryJson = JsonConvert.SerializeObject(countryText);
            var countriesText = JsonConvert.DeserializeObject(countryJson);
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(countriesText.ToString());
            return countries;
        }

        public List<Job.Res> JobList()
        {
            var lstJobs = DbContext.Instance.Exec<List<Job.Res>>(DbStore.GetJobs);
            return lstJobs;
        }
        public List<Level.Res> Levels()
        {
            List<Level.Res> lstLevel = DbContext.Instance.Exec<List<Level.Res>>(DbStore.GetAllLevels);
            return lstLevel;
        }
    }
}