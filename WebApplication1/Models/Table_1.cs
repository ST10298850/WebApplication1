using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using NuGet.Protocol.Plugins;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace WebApplication1.Models
{
    public class Table_1 
    {
        public static string con_string = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlConnection con = new SqlConnection(con_string);

        public string Name 
        {
            get; set; 
        }

        public string Surname 
        { 
            get; set; 
        }

        public string Email 
        { 
            get; set; 
        }

        public int insert_User(Table_1 n)
        {
            try
            {
                string sql = "INSERT INTO Table_1 (userName, userSurname, userEmail) VALUES(@Name, @Surname, @Email)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", n.Name);
                cmd.Parameters.AddWithValue("@Surname", n.Surname);
                cmd.Parameters.AddWithValue("@Email", n.Email);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch(Exception ex)
            {
                throw ex;
            }


        }

    }


}
