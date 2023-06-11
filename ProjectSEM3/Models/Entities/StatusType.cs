using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class StatusType
    {
        public string Name { get; set; }
        public string BadgeCss { get; set; }
        public bool IsHidden { get; set; }

        public StatusType(string Name, string BadgeCss, bool IsHidden = false)
        {
            this.Name = Name;
            this.BadgeCss = BadgeCss;
            this.IsHidden = IsHidden;
        }
    }
}