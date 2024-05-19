using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    //public class TransactionModel
    //{
    //    public int TransactionID { get; set; }
    //    public int UserID { get; set; }
    //    public int ProductID { get; set; }
    //    public int Quantity { get; set; }

    //    private static string connectionString = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

    //public int InsertTransaction(TransactionModel transaction)
    //{
    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        string sql = "INSERT INTO transactionTable (userID, productID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";
    //        SqlCommand cmd = new SqlCommand(sql, con);
    //        cmd.Parameters.AddWithValue("@UserID", transaction.UserID);
    //        cmd.Parameters.AddWithValue("@ProductID", transaction.ProductID);
    //        cmd.Parameters.AddWithValue("@Quantity", transaction.Quantity);

    //        try
    //        {
    //            con.Open();
    //            int rowsAffected = cmd.ExecuteNonQuery();
    //            return rowsAffected;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error inserting transaction: " + ex.Message);
    //        }
    //    }
    //}

    //public List<TransactionModel> GetUserTransactions(int userID)
    //{
    //    List<TransactionModel> transactions = new List<TransactionModel>();

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        string sql = "SELECT * FROM transactionTable WHERE userID = @UserID";
    //        SqlCommand cmd = new SqlCommand(sql, con);
    //        cmd.Parameters.AddWithValue("@UserID", userID);

    //        try
    //        {
    //            con.Open();
    //            SqlDataReader reader = cmd.ExecuteReader();
    //            while (reader.Read())
    //            {
    //                transactions.Add(new TransactionModel
    //                {
    //                    TransactionID = Convert.ToInt32(reader["TransactionID"]),
    //                    UserID = Convert.ToInt32(reader["UserID"]),
    //                    ProductID = Convert.ToInt32(reader["ProductID"]),
    //                    Quantity = Convert.ToInt32(reader["Quantity"])
    //                });
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error retrieving transactions: " + ex.Message);
    //        }
    //    }

    //    return transactions;
    //}

    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } // New property to store product name
        public decimal ProductPrice { get; set; } // New property to store product price

        private static string connectionString = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public int InsertTransaction(TransactionModel transaction)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO transactionTable (userID, productID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", transaction.UserID);
                cmd.Parameters.AddWithValue("@ProductID", transaction.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", transaction.Quantity);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inserting transaction: " + ex.Message);
                }
            }
        }

        public List<TransactionModel> GetUserTransactions(int userID)
        {
            List<TransactionModel> transactions = new List<TransactionModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Use the correct column names in the SQL query
                string sql = @"
                SELECT t.TransactionID, t.UserID, t.ProductID, t.Quantity, p.productName AS ProductName, p.productPrice AS ProductPrice
                FROM transactionTable t
                INNER JOIN productTable p ON t.ProductID = p.ProductID
                WHERE t.UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        transactions.Add(new TransactionModel
                        {
                            TransactionID = Convert.ToInt32(reader["TransactionID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            ProductName = reader["ProductName"].ToString(), // Ensure this matches your database column name
                            ProductPrice = Convert.ToDecimal(reader["ProductPrice"]) // Ensure this matches your database column name
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving transactions: " + ex.Message);
                }
            }

            return transactions;
        }
        public static void ClearUserTransactions(int userID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM transactionTable WHERE userID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clearing user transactions: " + ex.Message);
                }
            }
        }
    }
}

