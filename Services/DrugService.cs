using PharmacyManagementSystem.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PharmacyManagementSystem.Services
{
    internal class DrugService
    {
        public static DataTable GetAll()
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DRUGS", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void Add(int code, string name, double price, int stock, string expiryDate, int categoryId)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query =
                    "INSERT INTO DRUGS(code, name, price, stock, expiry_date, category_id) " +
                    "VALUES(@c, @n, @p, @s, @e, @cat)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@c", code);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@p", price);
                cmd.Parameters.AddWithValue("@s", stock);
                cmd.Parameters.AddWithValue("@e", expiryDate);
                cmd.Parameters.AddWithValue("@cat", categoryId);

                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(int code, string name, double price, int stock, string expiryDate, int categoryId)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query =
                    "UPDATE DRUGS SET name=@n, price=@p, stock=@s, expiry_date=@e, category_id=@cat " +
                    "WHERE code=@c";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@p", price);
                cmd.Parameters.AddWithValue("@s", stock);
                cmd.Parameters.AddWithValue("@e", expiryDate);
                cmd.Parameters.AddWithValue("@cat", categoryId);
                cmd.Parameters.AddWithValue("@c", code);

                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int code)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query = "DELETE FROM DRUGS WHERE code=@c";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@c", code);

                cmd.ExecuteNonQuery();
            }
        }
    }
}