using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Question
    {
        public class Req
        {
            public int Id { get; set; }
            public int TypeId { get; set; }
            public int ExamTypeId { get; set; }
            public int LevelId { get; set; }
            public string Content { get; set; }
            public int Point { get; set; }
            public string Options { get; set; }
            public string CorrectAnwser { get; set; }
            public bool IsMultiAnwser { get; set; }
            public bool Status { get; set; }
        }

        public class Res
        {
            public int Id { get; set; }
            public int TypeId { get; set; }
            public int ExamTypeId { get; set; }
            public string ExamTypeName { get; set; }
            public string TypeName { get; set; }
            public int LevelId { get; set; }
            public string LevelName { get; set; }
            public string Content { get; set; }
            public int Point { get; set; }
            public string Options { get; set; }
            public string CorrectAnwser { get; set; }
            public bool IsMultiAnwser { get; set; }
            public int RowIndex { get; set; }
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