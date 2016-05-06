using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tsa_DAL;
using tsa_Model;

namespace tsa_BL
{
    public class AdminBL
    {
        AdminDAL AdminDL = new AdminDAL();
        public bool AddNewUserBL(UserDetails NewUser)
        {
            bool isAdded = false;

            isAdded = AdminDL.AddNewUserDAL(NewUser);

            return isAdded;
        }

        public bool UpdateUserBL(UserDetails updateUser)
        {
            bool isupdated = false;

            isupdated = AdminDL.UpdateUserDAL(updateUser);

            return isupdated;
        }

        public bool DeleteUserBL(string uname)
        {
            bool isDeleted = false;

            isDeleted = AdminDL.DeleteUserDAL(uname);

            return isDeleted;
        }

        public bool SearchUserBL(string uname, UserDetails SearchedUser)
        {
            bool isFound = false;

            isFound = AdminDL.SearchUserDAL(uname, SearchedUser);

            return isFound;
        }
    }
}
