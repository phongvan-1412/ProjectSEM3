using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class CV
    {
        public class Req
        {
            public int Id { get; set; }
            public int Job { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string FilePath { get; set; }
            public DateTime DatePosted { get; set; }
            public bool Status { get; set; }
            public Req() { }
        }

        public class Res
        {
            public int Id { get; set; }
            public int Job { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string FilePath { get; set; }
            public DateTime DatePosted { get; set; }

            public bool Status { get; set; }
            public Res() { }
        }
    }
}