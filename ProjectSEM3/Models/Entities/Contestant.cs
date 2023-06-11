using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Contestant
    {
        public class Res
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string IpDevice { get; set; }
            public string Cv { get; set; }
            public string Contact { get; set; }
            public string Address { get; set; }
            public bool Status { get; set; }
        }

        public class Req
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string IpDevice { get; set; }
            public string Cv { get; set; }
            public string Contact { get; set; }
            public string Address { get; set; }
            public bool Status { get; set; }
        }
    }
}