using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milty.Models
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Roles { get; set; }
    }
}