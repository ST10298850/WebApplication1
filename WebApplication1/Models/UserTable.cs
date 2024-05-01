using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using NuGet.Protocol.Plugins;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace WebApplication1.Models
{
    public class UserTable 
    {
        public static string con_string = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlConnection con = new SqlConnection(con_string);

        public string userName 
        {
            get; set; 
        }

        public string Email 
        { 
            get; set; 
        }
        public string Password
        {
            get; set;
        }



        public int insert_User(UserTable n)
        {
            try
            {
                string sql = "INSERT INTO UserTable (userName, userPassword, userEmail) VALUES(@userName, @Password, " +
                    "@Email)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@userName", n.userName);
                cmd.Parameters.AddWithValue("@Password", n.Password);
                cmd.Parameters.AddWithValue("@Email", n.Email);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch(Exception ex)
            {
                throw;
            }


        }

    }


}
