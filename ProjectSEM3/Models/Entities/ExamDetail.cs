using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class ExamDetail
    {
        public class Req
        {
            public int Id { get; set; }
            public int QuestionId { get; set; }
            public int ExamId { get; set; }
            public string Answer { get; set; }
            public bool Status { get; set; }
        }

        public class Res
        {
            public int Id { get; set; }
            public int QuestionId { get; set; }
            public int ExamId { get; set; }
            public string Answer { get; set; }
            public string Contest { get; set; }
            public int Point { get; set; }
            public string Options { get; set; }
            public string CorrectAnwser { get; set; }
            public int TypeId { get; set; }
            public string TypeName { get; set; }
            public int LevelId { get; set; }
            public string LevelName { get; set; }
            public int ExamTypeId { get; set; }
            public string ExamTypeName { get; set; }
            public bool IsMultiAnwser { get; set; }
            public bool Status { get; set; }
        }
    }
}