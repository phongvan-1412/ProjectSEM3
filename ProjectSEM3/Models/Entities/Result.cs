using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<string> Answer { get; set; }
        public bool IsMultiAnswer { get; set; }
        public List<string> CorrectAnswer { get; set; }
    }
}