using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        private static string connectionString = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public int InsertTransaction(int? UserID, int ProductID, int Quantity)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO transactionTable (userID, productID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    throw new Exception("Error inserting transaction: " + ex.Message);
                }
            }
        }
    }
}
