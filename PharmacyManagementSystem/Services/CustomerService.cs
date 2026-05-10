using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyManagementSystem.Data;
using System.Data;
using System.Data.SqlClient;

namespace PharmacyManagementSystem.Services
{
    internal class CustomerService
    {
        public static DataTable GetAll()
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CUSTOMERS", conn);
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
                    "INSERT INTO CUSTOMERS(name, phone) VALUES(@n,@p)";

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
                string query = "UPDATE CUSTOMERS SET name=@n, phone=@p WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@p", phone);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query = "DELETE FROM CUSTOMERS WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
