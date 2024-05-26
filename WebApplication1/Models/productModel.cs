using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class productTable
    {
        public static string con_string = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }
        public int ProductOwnerID { get; set; }
        public string ProductImageUrl { get; set; } // New field

        public int insert_product(productTable p)
        {
            try
            {
                string sql = "INSERT INTO productTable (productName, productPrice, productCategory, productAvailability, productOwnerID, productImageUrl) " +
                    "VALUES (@Name, @Price, @Category, @Availability, @ProductOwnerID, @ProductImageUrl)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Category", p.Category);
                cmd.Parameters.AddWithValue("@Availability", p.Availability);
                cmd.Parameters.AddWithValue("@ProductOwnerID", p.ProductOwnerID);
                cmd.Parameters.AddWithValue("@ProductImageUrl", p.ProductImageUrl); // New parameter

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<productTable> GetAllProducts()
        {
            List<productTable> products = new List<productTable>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM productTable";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    productTable product = new productTable
                    {
                        ProductID = Convert.ToInt32(rdr["productID"]),
                        Name = rdr["productName"].ToString(),
                        Price = rdr["productPrice"].ToString(),
                        Category = rdr["productCategory"].ToString(),
                        Availability = rdr["productAvailability"].ToString(),
                        ProductOwnerID = Convert.ToInt32(rdr["productOwnerID"]),
                        ProductImageUrl = rdr["productImageUrl"].ToString() // New field
                    };
                    products.Add(product);
                }
            }

            return products;
        }
    }
}
