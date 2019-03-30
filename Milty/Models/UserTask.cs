using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milty.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string Tag { get; set; }
        public string Repository { get; set; }
    }
}