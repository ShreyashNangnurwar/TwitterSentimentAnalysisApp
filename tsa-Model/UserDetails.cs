using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tsa_Model
{
    public class UserDetails
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        public int role_id { get; set; }
        public string user_email { get; set; }

        public UserDetails()
        {
        }
    }
}
