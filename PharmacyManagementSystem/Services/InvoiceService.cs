using PharmacyManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PharmacyManagementSystem.Services
{
    internal class InvoiceService
    {
        public static int CreateInvoice(int customerId, string empSSN)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                string query = @"
                    INSERT INTO INVOICE(customer_id, employee_ssn, total_amount)
                    OUTPUT INSERTED.inv_id
                    VALUES(@c,@e,0)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@c", customerId);
                cmd.Parameters.AddWithValue("@e", empSSN);

                return (int)cmd.ExecuteScalar();
            }
        }

        public static void AddItem(int invId, int itemNo, int drugCode, int quantity, int customerId)
        {
            using (SqlConnection conn = DB.GetConnection())
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT price, stock FROM DRUGS WHERE code=@c",
                        conn, trans);

                    cmd.Parameters.AddWithValue("@c", drugCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                        throw new Exception("Drug not found");

                    double price = Convert.ToDouble(reader["price"]);
                    int stock = Convert.ToInt32(reader["stock"]);
                    reader.Close();

                    if (stock < quantity)
                        throw new Exception("Not enough stock");

                    cmd = new SqlCommand(
                          "INSERT INTO INVOICE_ITEM(inv_id, item_no, drug_code, quantity) VALUES(@i,@no,@d,@q)",
                          conn, trans);

                    cmd.Parameters.AddWithValue("@i", invId);
                    cmd.Parameters.AddWithValue("@no", itemNo);
                    cmd.Parameters.AddWithValue("@d", drugCode);
                    cmd.Parameters.AddWithValue("@q", quantity);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(
                        "UPDATE DRUGS SET stock = stock - @q WHERE code=@c",
                        conn, trans);

                    cmd.Parameters.AddWithValue("@q", quantity);
                    cmd.Parameters.AddWithValue("@c", drugCode);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(
                        "UPDATE INVOICE SET total_amount = total_amount + @t WHERE inv_id=@i",
                        conn, trans);

                    cmd.Parameters.AddWithValue("@t", price * quantity);
                    cmd.Parameters.AddWithValue("@i", invId);
                    cmd.ExecuteNonQuery();


                    int points = (int)(price * quantity / 10);

                    SqlCommand getCust = new SqlCommand(
                        "SELECT customer_id FROM INVOICE WHERE inv_id=@i",
                        conn, trans);

                    getCust.Parameters.AddWithValue("@i", invId);

                    SqlCommand cmd2 = new SqlCommand(
                        "UPDATE CUSTOMERS SET loyalty_points = ISNULL(loyalty_points,0) + @p WHERE id=@id",
                        conn, trans);

                    cmd2.Parameters.AddWithValue("@p", points);
                    cmd2.Parameters.AddWithValue("@id", customerId);

                    cmd2.ExecuteNonQuery();

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public static void Delete(int invId)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT drug_code, quantity FROM INVOICE_ITEM WHERE inv_id=@i",
                    conn);

                cmd.Parameters.AddWithValue("@i", invId);

                SqlDataReader reader = cmd.ExecuteReader();

                List<(int drug, int quantity)> items = new List<(int, int)>();

                while (reader.Read())
                {
                    items.Add((
                        Convert.ToInt32(reader["drug_code"]),
                        Convert.ToInt32(reader["quantity"])
                    ));
                }

                reader.Close();

                foreach (var item in items)
                {
                    cmd = new SqlCommand(
                        "UPDATE DRUGS SET stock = stock + @q WHERE code=@c",
                        conn);

                    cmd.Parameters.AddWithValue("@q", item.quantity);
                    cmd.Parameters.AddWithValue("@c", item.drug);
                    cmd.ExecuteNonQuery();
                }

                cmd = new SqlCommand("DELETE FROM INVOICE_ITEM WHERE inv_id=@i", conn);
                cmd.Parameters.AddWithValue("@i", invId);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM INVOICE WHERE inv_id=@i", conn);
                cmd.Parameters.AddWithValue("@i", invId);
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetAll()
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM INVOICE", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}