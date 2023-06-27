using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Exam
    {
        public class Res
        {
            public int Id { get; set; }
            public int ContestId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public DateTime LateTime { get; set; }
            public int TotalPoint { get; set; }
            public int Status { get; set; }
            public string Email { get; set; }
            public int KnowledgePoint { get; set; }
            public int MathPoint { get; set; }
            public int ComputerPoint { get; set; }

        }

        public class Req
        {
            public int Id { get; set; }
            public int ContestId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public DateTime LateTime { get; set; }
            public int Status { get; set; }
        }
    }
}