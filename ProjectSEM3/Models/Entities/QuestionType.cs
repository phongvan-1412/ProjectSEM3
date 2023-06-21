using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class QuestionType
    {
        public class Req
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Status { get; set; }
        }
        public class Res
        {
            public int Id { get; set; }
            public int RowIndex { get; set; }
            public string Name { get; set; }
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