using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO
    {
        
        private string connectionString;
        public TransferSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public Transfer AddTransfer(Transfer transfer)
        {
            try
            {
                transfer.Type = 2;
                transfer.Status = 2;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string QUERY = "BEGIN TRANSACTION UPDATE accounts SET balance = balance - @amount " +
                        "WHERE account_id = @accountFrom UPDATE accounts SET balance = balance + @amount WHERE account_id = @accountTo" +
                        " INSERT transfers(transfer_type_id, transfer_status_id, account_from, account_to, amount)" +
                        "VALUES(@type, @status, @accountFrom, @accountTo, @amount) SELECT @@IDENTITY COMMIT TRANSACTION";
                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@type", transfer.Type);
                    cmd.Parameters.AddWithValue("@status", transfer.Status);
                    cmd.Parameters.AddWithValue("@accountFrom", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@accountTo", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    transfer.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    return transfer;
                }
            }
            catch
            {
                throw;
            }
        }
        public List<Transfer> GetSentTransfers(string userName)
        {
            List<Transfer> transfers = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string QUERY = @"SELECT uTO.username user_to, t.*, uFROM.username user_from
FROM transfers t
JOIN accounts aTO ON aTO.account_id = t.account_to
JOIN users uTO ON uTO.user_id = aTO.user_id
JOIN accounts aFROM ON aFROM.account_id = t.account_from
JOIN users uFROM ON uFROM.user_id = aFROM.user_id
WHERE @userName IN(uFROM.username)";

                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Transfer transfer = ReadToTransfer(reader);
                        transfers.Add(transfer);
                    }
                    return transfers;
                }
            }
            catch
            {
                throw;
            }
        }

        private Transfer ReadToTransfer(SqlDataReader reader)
        {
            return new Transfer
            {
                Id = Convert.ToInt32(reader["transfer_id"]),
                Type = Convert.ToInt32(reader["transfer_type_id"]),
                Status = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"]),
                UserTo = Convert.ToString(reader["user_to"]),
                UserFrom = Convert.ToString(reader["user_from"])
            };
        }
    }
}
