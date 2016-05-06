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
    public class AdminDAL
    {
        public string connectionString = "Data Source=MORYA-PC\\SQLEXPRESS;Initial Catalog=tsa-db;Integrated Security=True;";
        public bool AddNewUserDAL(UserDetails NewUser)
        {
            bool isAdded = false;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO UserDetails(user_name, user_password,user_role_id,user_email) VALUES(" + "@uname, @upass, @urole, @uemail)", conn);
                cmd.Parameters.AddWithValue("@uname", NewUser.user_name);
                cmd.Parameters.AddWithValue("@upass", NewUser.user_password);
                cmd.Parameters.AddWithValue("@urole", NewUser.role_id);
                cmd.Parameters.AddWithValue("@uemail", NewUser.user_email);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isAdded = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return isAdded;
        }

        public bool UpdateUserDAL(UserDetails updateUser)
        {
            bool isUpdated = false;

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE UserDetails SET user_password=@upass "+ "where user_name=@uname", conn);
                cmd.Parameters.AddWithValue("@upass", updateUser.user_password);
                cmd.Parameters.AddWithValue("@uname",updateUser.user_name);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isUpdated = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return isUpdated;
        }

        public bool DeleteUserDAL(string uname)
        {
            bool isDeleted = false;

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE from UserDetails where user_name=@uname", conn);
                cmd.Parameters.AddWithValue("@uname", uname);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isDeleted = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return isDeleted;
        }

        public bool SearchUserDAL(string uname, UserDetails SearchedUser)
        {
            bool isFound = false;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from UserDetails where user_name=\'"+ uname +"\'", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                if ( reader.HasRows )
                {
                    while (reader.Read())
                    {
                        SearchedUser.user_id = Convert.ToInt32(reader["user_id"].ToString());
                        SearchedUser.user_name = reader["user_name"].ToString();
                        SearchedUser.role_id = Convert.ToInt32(reader["user_role_id"].ToString());
                        SearchedUser.user_email = reader["user_email"].ToString();
                        break;
                    }
                    isFound = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return isFound;
        }
    }
}
