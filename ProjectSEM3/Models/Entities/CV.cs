using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Security;

namespace ProjectSEM3.Models.Entities
{
    public class CV
    {
        public class Req
        {
            public int Id { get; set; }
            public int JobId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string FilePath { get; set; }
            public DateTime DatePosted { get; set; }
            public int Status { get; set; }
            public int LevelId { get; set; }
            public Req() { }
        }

        public class Res
        {
            public int Id { get; set; }
            public int JobId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string FilePath { get; set; }
            public string JobTitle { get; set; }
            public DateTime DatePosted { get; set; }
            public int Status { get; set; }
            public int LevelId { get; set; }
            public string LevelName { get; set; }
            public StatusType StatusType => GetStatus(Status);

            public Res() { }
        }

        public static StatusType GetStatus(int index)
        {
            StatusType result = null;
            switch (index)
            {
                case 1:
                    result =  new StatusType("Pending", "badge bg-warning");
                    break;
                case 2:
                    result = new StatusType("Accepted", "badge bg-success");
                    break;
                case 3:
                default:
                    result = new StatusType("Rejected", "badge bg-danger", true);
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
    }
}