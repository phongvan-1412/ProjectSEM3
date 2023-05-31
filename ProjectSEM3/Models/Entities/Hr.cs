using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Hr
    {
        public class Res
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Contact { get; set; }
            public string Address { get; set; }
            public string Education { get; set; }
            public string Experience { get; set; }

            public bool Status { get; set; }

            public Res()
            {

            }

            public Res(int id)
            {
                Id = id;
            }
        }

        public class Req
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Contact { get; set; }
            public string Address { get; set; }
            public string Education { get; set; }
            public string Experience { get; set; }

            public bool Status { get; set; }

            public Req()
            {

            }

            public Req(int id)
            {
                Id = id;
            }
        }
    }
}