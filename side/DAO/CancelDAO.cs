using Newtonsoft.Json.Linq;
using NiteenNity_Case_SQL_API.Mode.Abstract;
using NiteenNity_Case_SQL_API.Mode.DataSet.DAO;
using side.DataSet;
using side.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace side.DAO
{
    public class CancelDAO
    {
        // 資料庫連接字串
        //private string _connectionString = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        //ImplmentSQL sql = ImplmentSQL.getInstance();
        private static CancelDAO instance = new CancelDAO();
        private string conn_str = "Data Source=localhost;Initial Catalog=tw_Casino1;UID=loveaoe33;PWD=love20720";
        public static CancelDAO getInstance()
        {
            return instance;
        }

        public static void Set_conn_str(String conn_str_out)
        {
            CancelDAO static_Cancel = CancelDAO.getInstance();
            static_Cancel.conn_str = conn_str_out;
        }

        public string getHasBankType(string account)
        {
            using (SqlConnection conn = new SqlConnection(conn_str))
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // 拿到 MemberId和 原始金額 和 比例手續費，固定手續費
                SqlCommand cmd = new SqlCommand("SELECT distinct SUBSTRING(D.Remark, CHARINDEX('銀行：', D.Remark)+3, CHARINDEX('<br/>', D.Remark)-(CHARINDEX('銀行：', D.Remark)+3)) AS bankType " +
                    "FROM MemberShip2_User A " +
                    "LEFT JOIN Wallet_WithdrawItem D ON D.MemberId = A.Id " +
                    "WHERE A.Account = @account", conn);

                // 將資料塞入 SQL 指令中
                cmd.Parameters.AddWithValue("@account", account);

                // 開啟資料庫連線，並執行 SQL 指令
                conn.Open();
                string bankType = "";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bankType = Convert.ToString(reader["bankType"]);
                }
                conn.Close();
                reader.Close();

                return bankType;
            }
        }
        public string getFeeCategory(string bankType)
        {
            using (SqlConnection conn = new SqlConnection(conn_str))
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // 拿到 比例手續費，固定手續費
                SqlCommand cmd = new SqlCommand("SELECT WithdrawFeeRatio, WithdrawFee " +
                    "FROM Config_SystemConfigItem " +
                    "WHERE BankType like '%" + bankType + "%'", conn);

                // 開啟資料庫連線，並執行 SQL 指令
                conn.Open();
                string ratio = "";
                string fee = "";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ratio = Convert.ToString(reader["WithdrawFeeRatio"]);
                    fee = Convert.ToString(reader["WithdrawFee"]);
                }
                conn.Close();
                reader.Close();

                return ratio + "," + fee;
            }
        }
        public string getMemberShip2UserIdAndValue(string account)
        {
            using (SqlConnection conn = new SqlConnection(conn_str))
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // 拿到 MemberId和 原始金額 和 比例手續費，固定手續費
                SqlCommand cmd = new SqlCommand("SELECT A.Id, B.Value " +
                    "FROM MemberShip2_User A " +
                    "LEFT JOIN Wallet_WalletItem B ON A.Id = B.MemberId " +
                    "WHERE A.Account = @account", conn);

                // 將資料塞入 SQL 指令中
                cmd.Parameters.AddWithValue("@account", account);

                // 開啟資料庫連線，並執行 SQL 指令
                conn.Open();
                int id = -1;
                string value = "-1";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["Id"]);
                    value = Convert.ToString(reader["Value"]);
                }
                conn.Close();
                reader.Close();

                return Convert.ToString(id) + "," + value;
            }
        }
        public string getWallet_WithdrawItem_Remark(int memberId, string startTime, string endTime, string withdrawFeeRatio, string withdrawFee)
        {
            using (SqlConnection conn = new SqlConnection(conn_str))
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // 拿到 Remark
                SqlCommand cmd = new SqlCommand("SELECT Value, FeeRatio, Fee1, Remark " +
                    "FROM Wallet_WithdrawItem " +
                    "where memberId = @memberId AND State = '已處理'" +
                        "AND ( CreateTime between '" + startTime + "'" +
                        "and '" + endTime + "' ) " +
                        ";", conn);

                // 將資料塞入 SQL 指令中
                cmd.Parameters.AddWithValue("@memberId", memberId);

                // 開啟資料庫連線，並執行 SQL 指令
                conn.Open();
                string value = "";
                string ratio = "";
                string fee = "";
                string remark = "";
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    value = Convert.ToString(reader["Value"]);
                    ratio = Convert.ToString(reader["FeeRatio"]);
                    fee = Convert.ToString(reader["Fee1"]);
                    remark = Convert.ToString(reader["Remark"]);
                }
                conn.Close();
                reader.Close();

                if (!string.IsNullOrEmpty(remark) || !string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(ratio) || !string.IsNullOrEmpty(fee))
                {
                    return "1," + remark + "," + value + "," + ratio + "," + fee;
                }
                else
                {
                    return "0";
                }
            }
        }
        public int CancelApplyValue_UpdateWallet_WithdrawItem(int memberId, string startTime, string endTime, string withdrawFeeRatio, string withdrawFee)
        {
            int ans = 0;
            using (SqlConnection conn = new SqlConnection(conn_str))
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                ans = judge(conn, memberId, startTime, endTime);
                if (ans == 1)
                {
                    // 更新提領狀態 [Wallet_WithdrawItem]
                    SqlCommand cmd = new SqlCommand("update Wallet_WithdrawItem " +
                        "set State = '已取消', UpdateTime = GETDATE() " +
                        ", Available = Value - Value * CAST(" + withdrawFeeRatio + " AS DECIMAL(18, 2)) / 100 - CAST(" + withdrawFee + " AS DECIMAL(18, 2)) " +
                        "where memberId = @memberId AND State = '已處理'" +
                        "AND ( CreateTime between '" + startTime + "'" +
                        "and '" + endTime + "' ) " +
                        ";", conn);

                    // 將資料塞入 SQL 指令中
                    cmd.Parameters.AddWithValue("@memberId", memberId);

                    // 開啟資料庫連線，並執行 SQL 指令
                    conn.Open();
                    ans = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return ans;
            }
        }
        public int CancelApplyValue_UpdateWallet_WalletItem(int memberId, string value)
        {
            int ans = 0;
            using (SqlConnection conn = new SqlConnection(conn_str))
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                ans = judge(conn, memberId);
                if (ans == 1)
                {
                    // 更新原始金額 [Wallet_WithdrawItem]
                    SqlCommand cmd = new SqlCommand("update Wallet_WalletItem " +
                    "set Value = Value + CAST(" + Convert.ToInt32(value) + " AS DECIMAL(18, 2))" +
                    ", UpdateTime = GETDATE() " +
                    "where memberId = @memberId" +
                    ";", conn);
                    // 將資料塞入 SQL 指令中
                    cmd.Parameters.AddWithValue("@memberId", memberId);

                    // 開啟資料庫連線，並執行 SQL 指令
                    conn.Open();
                    ans = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return ans;
            }
        }
        public int CancelApplyValue_InsertWallet_WalletRecordItem(int memberId, string value, string startTime, string endTime, string editor, string withdrawFeeRatio, string withdrawFee, string remark)
        {
            int ans = 0;
            using (SqlConnection conn = new SqlConnection(conn_str))
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // 記錄一筆 取消提領 [Wallet_WalletRecordItem]
                SqlCommand cmd = new SqlCommand("INSERT INTO Wallet_WalletRecordItem " +
                    "(WalletId, Id, Reason, Old, Increment, New, Remark, IsHide, CreateTime, Editor, UpdateTime)" +
                    " select MemberId, (select MAX(Id) + 1 from Wallet_WalletRecordItem), '取消提領'" +
                    ", (select Value from Wallet_WalletItem where MemberId = @memberId) - CAST(" + Convert.ToInt32(value) + " AS DECIMAL(18, 2))" +
                    ", Value" +
                    ", (select Value from Wallet_WalletItem where MemberId = @memberId)" +
                    ", '" + remark + "' " +
                    ",'0' ,GETDATE() ,'" + editor + "' ,UpdateTime " +
                    "from Wallet_WithdrawItem " +
                    "where MemberId = @memberId AND State = '已取消' AND Type = '提領' " +
                    "AND ( CreateTime between '" + startTime + "'" +
                    "and '" + endTime + "' ); ", conn);

                // 將資料塞入 SQL 指令中
                cmd.Parameters.AddWithValue("@memberId", memberId);

                // 開啟資料庫連線，並執行 SQL 指令
                conn.Open();
                ans = cmd.ExecuteNonQuery();

                // 記錄一筆 取消提領手續費 [Wallet_WalletRecordItem]
                cmd = new SqlCommand("INSERT INTO Wallet_WalletRecordItem " +
                    "(WalletId, Id, Reason, Old, Increment, New, Remark, IsHide, CreateTime, Editor, UpdateTime)" +
                    " select MemberId, (select MAX(Id) + 1 from Wallet_WalletRecordItem), '取消提領手續費'" +
                    ", 0" +
                    ", CAST(" + value + " AS decimal) * CAST(" + withdrawFeeRatio + " AS decimal) / 100 + CAST(" + withdrawFee + " AS decimal)" +
                    ", CAST(" + value + " AS decimal) * CAST(" + withdrawFeeRatio + " AS decimal) / 100 + CAST(" + withdrawFee + " AS decimal)" +
                    ", '" + remark + "','0' ,GETDATE() ,'" + editor + "' ,UpdateTime " +
                    "from Wallet_WithdrawItem " +
                    "where MemberId = @memberId AND State = '已取消' AND Type = '提領' " +
                    "AND ( CreateTime between '" + startTime + "'" +
                    "and '" + endTime + "' ); ", conn);

                // 將資料塞入 SQL 指令中
                cmd.Parameters.AddWithValue("@memberId", memberId);

                ans = cmd.ExecuteNonQuery();

                conn.Close();

                return ans;
            }
        }
        private int judge(SqlConnection conn, int memberId, string startTime, string endTime)
        {
            // 拿到 MemberId和 原始金額
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) AS count " +
                "FROM Wallet_WithdrawItem " +
                "where memberId = @memberId AND State = '已處理'" +
                        "AND ( CreateTime between '" + startTime + "'" +
                        "and '" + endTime + "' ) " +
                        ";", conn);

            // 將資料塞入 SQL 指令中
            cmd.Parameters.AddWithValue("@memberId", memberId);

            // 開啟資料庫連線，並執行 SQL 指令
            conn.Open();
            int count = -1;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                count = Convert.ToInt32(reader["count"]);
            }
            conn.Close();
            reader.Close();

            if (count == 1)
            {
                return count;
            }
            else if (count > 1)
            {
                return count;
            }
            else
            {
                return count;
            }
        }
        private int judge(SqlConnection conn, int memberId)
        {
            // 拿到 MemberId和 原始金額
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) AS count " +
                "FROM Wallet_WalletItem " +
                "where memberId = @memberId ;", conn);

            // 將資料塞入 SQL 指令中
            cmd.Parameters.AddWithValue("@memberId", memberId);

            // 開啟資料庫連線，並執行 SQL 指令
            conn.Open();
            int count = -1;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                count = Convert.ToInt32(reader["count"]);
            }
            conn.Close();
            reader.Close();

            if (count == 1)
            {
                return count;
            }
            else if (count > 1)
            {
                return count;
            }
            else
            {
                return count;
            }
        }
    }
}
