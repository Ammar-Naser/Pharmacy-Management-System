using PharmacyManagementSystem.Data;
using System.Data;
using System.Data.SqlClient;

namespace PharmacyManagementSystem.Services
{
    internal class SupplierService
    {
        public static DataTable GetAll()
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter("SELECT * FROM SUPPLIER", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public static void Add(string name, string phone)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query =
                    "INSERT INTO SUPPLIER(name, phone) VALUES(@n,@p)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@p", phone);

                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(int id, string name, string phone)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query =
                    "UPDATE SUPPLIER SET name=@n, phone=@p WHERE supplier_id=@i";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@i", id);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@p", phone);

                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query =
                    "DELETE FROM SUPPLIER WHERE supplier_id=@i";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@i", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}