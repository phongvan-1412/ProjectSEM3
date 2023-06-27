using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Job
    {
        public class Req
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Location { get; set; }
            public DateTime PostedDate { get; set; }
            public DateTime EndDate { get; set; }
            public string LevelId { get; set; }
            public string SalaryMin { get; set; }
            public string SalaryMax { get; set; }
            public string Icon { get; set; }
            public string Content { get; set; }
            public string Qualification { get; set; }
            public bool Status { get; set; }
        }
        public class Res
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Location { get; set; }
            public DateTime PostedDate { get; set; }
            public string PostedDateTimeStamp => PostedDate.ToString();
            public DateTime EndDate { get; set; }
            public string Qualification { get; set; }
            public string EndDateTimeStamp => EndDate.ToString();
            public int LevelId { get; set; }
            public string LevelName { get; set; }
            public decimal SalaryMin { get; set; }
            public decimal SalaryMax { get; set; }
            public string Salary { get => "$" + SalaryMin + " - $" + SalaryMax; }
            public string Icon { get; set; }
            public string Content { get; set; }
            public int RowIndex { get; set; }
            public string JobLink { get => "https://localhost:44376/Career?id=" + Id; }
            public bool Status { get; set; }
            public StatusType StatusType => GetStatus(Status);
        }
        public static StatusType GetStatus(bool index)
        {
            StatusType result = null;
            switch (index)
            {
                case true:
                    result = new StatusType("Active", "badge bg-success");
                    break;
                case false:
                default:
                    result = new StatusType("Deleted", "badge bg-danger", true);
                    break;
            }
            return result;
        }
    }
}