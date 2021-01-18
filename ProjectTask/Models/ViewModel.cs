using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTask.Models
{
    public class ViewModel
    {
        public IEnumerable<MsProject> msProjects { get; set; }
        public IEnumerable<MsUser> msUsers { get; set; }
    }
}