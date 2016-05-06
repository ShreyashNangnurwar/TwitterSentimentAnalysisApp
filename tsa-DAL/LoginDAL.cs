using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using tsa_Model;

namespace tsa_DAL
{
    public class LoginDAL
    {
        string connectionString = "Data Source=MORYA-PC\\SQLEXPRESS;Initial Catalog=tsa-db;Integrated Security=True;";
        // logic for login validation
        public bool ValidateLoginDetailsDAL(UserDetails user)
        {
            bool isDetailsValid = false;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from UserDetails where user_name=\'"+user.user_name + "\'",conn);
            //SqlCommand cmd = new SqlCommand("select * from UserDetails" , conn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string pass = reader["user_password"].ToString();
                        if (user.user_password == reader["user_password"].ToString())
                        {
                            user.role_id = Convert.ToInt32(reader["user_role_id"].ToString());
                            isDetailsValid = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return isDetailsValid;
        }
    }
}
