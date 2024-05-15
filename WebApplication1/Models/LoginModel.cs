using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class LoginModel 
    {
        public static string con_string = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlConnection con = new SqlConnection(con_string);

        public string userName { get; set; }
        public string password { get; set; }

        public int SelectUser(string userName, string password)
        {
            int userID = -1; // Default value if user is not found
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT userID FROM UserTable WHERE userName = @userName AND userPassword = @userPassword";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@userPassword", password);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userID = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    // For now, rethrow the exception
                    throw ex;
                }
            }
            return userID;
        }
    }
}
