﻿
namespace ProjectSEM3.Models.Entities
{
    public class Type
    {
        public class Res
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Status { get; set; }
        }

        public class Req
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Status { get; set; }
        }
    }
}