using PharmacyManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementSystem.Services
{
    using System.Data.SqlClient;

    public class AuthService
    {
        public static bool Login(string ssn, string name)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM EMPLOYEES WHERE ssn=@ssn AND name=@name";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ssn", ssn);
                cmd.Parameters.AddWithValue("@name", name);

                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
    }
}
