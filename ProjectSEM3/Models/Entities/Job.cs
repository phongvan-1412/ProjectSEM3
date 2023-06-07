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
            public string DatePosted { get; set; }
            public string EmpType { get; set; }
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
            public string DatePosted { get; set; }
            public string EmpType { get; set; }
            public string Content { get; set; }
            public bool Status { get; set; }
            public Res()
            {

            }
        }
    }
}