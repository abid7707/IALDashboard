using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IALDashboard.Models
{
    public class User
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string user_area { get; set; }
    }
}