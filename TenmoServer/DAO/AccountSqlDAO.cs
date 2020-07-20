using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO
    {
        private string connectionString;
        public AccountSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public decimal GetBalance(string userName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string QUERY = "SELECT balance FROM accounts JOIN users ON accounts.user_id = users.user_id WHERE users.username = @userName";
                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    decimal balance = Convert.ToDecimal(cmd.ExecuteScalar());
                    return balance;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
