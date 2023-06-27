using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class ContestantExam
    {
        public List<ExamDetail.Res> Math { get; set; }
        public List<ExamDetail.Res> Computer { get; set; }
        public List<ExamDetail.Res> Knowledge { get; set; }

        public ContestantExam() { }

        public ContestantExam(List<ExamDetail.Res> data)
        {
            Knowledge = new List<ExamDetail.Res>();
            Knowledge.AddRange(data.Where(x => x.ExamTypeId == 34).ToList());
            Math = new List<ExamDetail.Res>();
            Math.AddRange(data.Where(x => x.ExamTypeId == 35).ToList());
            Computer = new List<ExamDetail.Res>();
            Computer.AddRange(data.Where(x => x.ExamTypeId == 36).ToList());
        }
    }
}