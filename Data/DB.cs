using System;
using System.Data.SqlClient;

namespace PharmacyManagementSystem.Data
{
    internal static class DB
    {
        public static SqlConnection GetConnection()
        {
            string connStr =
                "Server=localhost;Database=pharmacy_db;Trusted_Connection=True;";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            return conn;
        }
    }
}