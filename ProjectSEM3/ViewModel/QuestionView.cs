using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class QuestionView
    {
        public int Id { get; set; }
        public int IdType { get; set; }
        public int IdLevel { get; set; }
        public string Content { get; set; }
        public int Point { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnwser { get; set; }
        public bool IsMultiAnwser { get; set; }
        public bool Status { get; set; }

        public QuestionView() { }
        public QuestionView(int id)
        {
            Id = id;
        }
    }
}