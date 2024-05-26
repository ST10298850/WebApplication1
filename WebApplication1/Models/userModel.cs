using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class userModel
    {
        public static string con_string = "Server=tcp:cloud-dev-db.database.windows.net,1433;Initial Catalog=cloud-dev-db;Persist Security Info=False;User ID=LukePetzer;Password=WhiteIceBeard44@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public int userID { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public userModel GetUserByID(int userID)
        {
            userModel user = null;
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM UserTable WHERE userID = @userID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@userID", userID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new userModel
                    {
                        userID = (int)reader["userID"],
                        userName = reader["userName"] != DBNull.Value ? (string)reader["userName"] : null,
                        Email = reader["userEmail"] != DBNull.Value ? (string)reader["userEmail"] : null,
                        Password = reader["userPassword"] != DBNull.Value ? (string)reader["userPassword"] : null
                    };
                }
            }
            return user;
        }


        public void UpdateUser(userModel user)
        {
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "UPDATE UserTable SET userName = @userName, userEmail = @Email, userPassword = @Password WHERE userID = @userID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@userID", user.userID);
                cmd.Parameters.AddWithValue("@userName", user.userName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int insert_User(userModel n)
        {
            try
            {
                string sql = "INSERT INTO UserTable (userName, userPassword, userEmail) VALUES(@userName, @Password, @Email)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@userName", n.userName);
                cmd.Parameters.AddWithValue("@Password", n.Password);
                cmd.Parameters.AddWithValue("@Email", n.Email);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public class orderModel
    {
        public int orderID { get; set; }
        public DateTime orderDate { get; set; }
        public decimal totalAmount { get; set; }

        public List<orderModel> GetUserOrders(int userID)
        {
            List<orderModel> orders = new List<orderModel>();
            using (SqlConnection con = new SqlConnection(userModel.con_string))
            {
                string sql = "SELECT transactionID, userID, productID, Quantity FROM transactionTable WHERE userID = @userID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@userID", userID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new orderModel
                    {
                        orderID = (int)reader["transactionID"], // Assuming transactionID is the equivalent of orderID in transactionTable
                                                                // Include other necessary fields here
                    });
                }
            }
            return orders;
        }

    }

}
