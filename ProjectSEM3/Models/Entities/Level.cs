using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Level
    {
        public class Res
        {
            public int Id { get; set; }
            public string Name { get; set; }
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