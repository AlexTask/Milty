using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milty.Models
{
    public class DetailsRepositoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<UserTask> tasks { get; set; }
    }
}