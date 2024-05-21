using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class CartModel
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        private static string connectionString = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public int AddToCart(CartModel cartItem)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO tempCartTable (UserID, ProductID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", cartItem.UserID);
                cmd.Parameters.AddWithValue("@ProductID", cartItem.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", cartItem.Quantity);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding to cart: " + ex.Message);
                }
            }
        }

        public List<CartModel> GetUserCartItems(int userID)
        {
            List<CartModel> cartItems = new List<CartModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT c.CartID, c.UserID, c.ProductID, c.Quantity, p.productName AS ProductName, p.productPrice AS ProductPrice
                FROM tempCartTable c
                INNER JOIN productTable p ON c.ProductID = p.ProductID
                WHERE c.UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cartItems.Add(new CartModel
                        {
                            CartID = Convert.ToInt32(reader["CartID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            ProductName = reader["ProductName"].ToString(),
                            ProductPrice = Convert.ToDecimal(reader["ProductPrice"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving cart items: " + ex.Message);
                }
            }

            return cartItems;
        }

        public int UpdateCartItem(int cartID, int quantity)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "UPDATE tempCartTable SET Quantity = @Quantity WHERE CartID = @CartID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@CartID", cartID);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating cart item: " + ex.Message);
                }
            }
        }

        public void ClearUserCart(int userID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM tempCartTable WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clearing user cart: " + ex.Message);
                }
            }
        }
    }
}
