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
            public DateTime DatePosted { get; set; }
            public DateTime EndDate { get; set; }
            public int LevelId { get; set; }
            public string Content { get; set; }
            public bool Status { get; set; }
            public Req()
            {

            }
        }
        public class Res
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Location { get; set; }
            public DateTime DatePosted { get; set; }
            public DateTime EndDate { get; set; }
            public int LevelId { get; set; }
            public int LevelName { get; set; }
            public string Content { get; set; }
            public bool Status { get; set; }
            public Res()
            {

            }
        }
    }
}