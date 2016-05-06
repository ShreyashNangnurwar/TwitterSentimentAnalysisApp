using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tsa_DAL;
using tsa_Model;

namespace tsa_BL
{
    public class LoginBL
    {
        LoginDAL LoginDAL = new LoginDAL();

        public bool ValidateLoginDetailsBL(UserDetails user)
        {
            bool isDetailsValid = false;

            isDetailsValid = LoginDAL.ValidateLoginDetailsDAL(user); 

            return isDetailsValid;
        }
    }
}
