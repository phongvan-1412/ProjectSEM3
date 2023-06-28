using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Contestant
    {
        public class Req
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Cv { get; set; }
            public string Contact { get; set; }
            public DateTime DatePosted { get; set; }
            public int JobId { get; set; }
            public int ExamId { get; set; }
            public int Status { get; set; }
            public bool IsViewed { get; set; }
        }

        public class Res
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Cv { get; set; }
            public string Contact { get; set; }
            public int JobId { get; set; }
            public string JobTitle { get; set; }
            public string JobLink => $"https://localhost:44376/Career/GetJobById/{JobId}";
            public DateTime DatePosted { get; set; }
            public bool IsViewed { get; set; }
            public int Status { get; set; }
            public int ExamId { get; set; }
            public int LevelId { get; set; }
            public string LevelName { get; set; }
            public StatusType StatusType => GetStatus(Status);
        }

        public static StatusType GetStatus(int index)
        {
            StatusType result = null;
            switch (index)
            {
                case 1:
                    result = new StatusType("Pending", "badge bg-warning");
                    break;
                case 2:
                    result = new StatusType("Accepted", "badge bg-success", true);
                    break;
                case 3:
                default:
                    result = new StatusType("Rejected", "badge bg-danger", true);
                    break;
                case 4:
                    result = new StatusType("Passed", "badge bg-success", true);
                    break;
            }
            return result;
        }
    }

    public enum CvStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Passed = 4
    }
}