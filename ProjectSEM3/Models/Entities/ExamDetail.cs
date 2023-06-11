using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class ExamDetail
    {
        public class Res
        {
            public int Id { get; set; }
            public int QuestionId { get; set; }
            public int ExamId { get; set; }
            public string Answer { get; set; }
            public bool Status { get; set; }
        }

        public class Req
        {
            public int Id { get; set; }
            public int QuestionId { get; set; }
            public int ExamId { get; set; }
            public string Answer { get; set; }
            public bool Status { get; set; }
        }
    }
}