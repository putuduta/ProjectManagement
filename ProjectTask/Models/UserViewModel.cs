using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTask.Models
{
    public class UserViewModel
    {
        public MsProject msProject { get; set; }
        public IEnumerable<WorkItem> workItems { get; set; }
    }
}