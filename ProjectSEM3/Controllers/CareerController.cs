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
        public ActionResult Jobs(string lstCountries)
        {
            if(lstCountries != null)
            {
                string[] lst = lstCountries.Split(',');
                List<string> list = new List<string>(lst);
            }

            ViewData["lstCountries"] = Country();
            ViewData["lstLevels"] = Levels();
            ViewData["lstJobs"] = JobList();
            return View();
        }

        public ActionResult ApplyCV()
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
            return RedirectToAction("Jobs", "Career", new { lstCountries = lstCountries });
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