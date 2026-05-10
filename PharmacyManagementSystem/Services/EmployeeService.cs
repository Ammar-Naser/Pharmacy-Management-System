using PharmacyManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementSystem.Services
{
    internal class EmployeeService
    {
        public static DataTable GetAll()
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EMPLOYEES", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void Add(string ssn, string name, double salary)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query = "INSERT INTO EMPLOYEES(ssn, name, salary) VALUES(@s,@n,@sa)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@s", ssn);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.Add("@sa", System.Data.SqlDbType.Float).Value = salary;

                cmd.ExecuteNonQuery();  
            }
        }

        public static void Update(string ssn, string name, double salary)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query = "UPDATE EMPLOYEES SET name=@n, salary=@s WHERE ssn=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@s", salary);
                cmd.Parameters.AddWithValue("@id", ssn);

                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(string ssn)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query = "DELETE FROM EMPLOYEES WHERE ssn=@s";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@s", ssn);

                cmd.ExecuteNonQuery();
            }
        }
    }
}