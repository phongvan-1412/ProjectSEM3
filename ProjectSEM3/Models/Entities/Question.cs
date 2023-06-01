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
            public int IdType { get; set; }
            public int IdLevel { get; set; }
            public string Content { get; set; }
            public int Point { get; set; }
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string CorrectAnwser { get; set; }
            public bool IsMultiAnwser { get; set; }
            public bool Status { get; set; }

            public Req() { }
            public Req(int id)
            {
                Id = id;
            }
        }

        public class Res
        {
            public int Id { get; set; }
            public int IdType { get; set; }
            public string TypeName { get; set; }
            public int IdLevel { get; set; }
            public string LevelName { get; set; }
            public string Content { get; set; }
            public int Point { get; set; }
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string CorrectAnwser { get; set; }
            public bool IsMultiAnwser { get; set; }
            public bool Status { get; set; }

            public Res() { }
            public Res(int id)
            {
                Id = id;
            }
        }
    }
}