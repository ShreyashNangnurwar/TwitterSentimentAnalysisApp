using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tsa_Models
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class User
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        public int role_id { get; set; }

        public User()
        {
        }
    }
}
