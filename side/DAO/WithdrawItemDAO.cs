using NiteenNity_Case_SQL_API.Mode.Abstract;
using side.DataSet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace side.DAO
{
    internal class WithdrawItemDAO
    {
        /// <summary>
        /// 資料庫連接字串。
        /// </summary>
        public string ConnectionString { get; set; }

        private static readonly WithdrawItemDAO _instance = new WithdrawItemDAO();

        private static readonly ImplmentSQL _sqlImp = ImplmentSQL.getInstance();


        /// <summary>
        /// 取得執行實體。
        /// </summary>
        /// <returns><see cref="WithdrawItemDAO"/> 的執行實體。</returns>
        public static WithdrawItemDAO GetInstance()
        {
            return _instance;
        }

        public string GetNextCaseId()
        {
            return $"{DateTime.Now:yyyyMMddHHmmssfff}";
        }

        public DataSet_WithdrawItemQuerying GetDataByCaseId(string caseId)
        {
            if (string.IsNullOrEmpty(caseId))
            {
                return null;
            }

            List<DataSet_WidthdrawItem> items = new List<DataSet_WidthdrawItem>();
            using (SqlConnection conn = new SqlConnection(_sqlImp.conn_str))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        " SELECT *" +
                        " FROM Wallet_WithdrawItem" +
                        " WHERE CaseId >= @CaseId;";

                    cmd.Parameters.AddWithValue("@CaseId", caseId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        items.Add(new DataSet_WidthdrawItem
                        {
                            CaseId = $"{reader["CaseId"]}",
                            MemberId = Convert.ToInt32(reader["MemberId"]),
                            ToMemberId = Convert.ToInt32(reader["ToMemberId"]),
                            Id = Convert.ToInt32(reader["Id"]),
                            Value = Convert.ToDecimal(reader["Value"]),
                            FeeRatio = Convert.ToDecimal(reader["FeeRatio"]),
                            Fee1 = Convert.ToDecimal(reader["Fee1"]),
                            Fee2 = Convert.ToDecimal(reader["Fee2"]),
                            Available = Convert.ToDecimal(reader["Available"]),
                            State = $"{reader["State"]}",
                            UpdateTime = Convert.ToDateTime(reader["UpdateTime"]),
                            CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                        });
                    }
                }

                conn.Close();
            }

            return new DataSet_WithdrawItemQuerying
            {
                QueryBy = new { CaseId = caseId },
                WidthdrawItems = items,
            };
        }

        public DataSet_WithdrawItemQuerying GetDataByAccountAndMonth(string account, string date)
        {
            if (!DateTime.TryParse(date, out DateTime dt))
            {
                return null;
            }

            DateTime durationStart = new DateTime(dt.Year, dt.Month, 1);
            DateTime durationEnd = new DateTime(dt.Year + (dt.Month == 12 ? 1 : 0), dt.Month + 1, 1);

            List<DataSet_WidthdrawItem> items = new List<DataSet_WidthdrawItem>();
            using (SqlConnection conn = new SqlConnection(_sqlImp.conn_str))
            {
                //_sqlImp.conn_str = ConnectionString;
                var memberId = _sqlImp.GetMemberShipID(conn, account);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        " SELECT *" +
                        " FROM Wallet_WithdrawItem" +
                        " WHERE UpdateTime >= @Start" +
                        "   AND UpdateTime < @End" +
                        "   AND MemberId = @MemberId;";

                    cmd.Parameters.AddWithValue("@Start", durationStart);
                    cmd.Parameters.AddWithValue("@End", durationEnd);
                    cmd.Parameters.AddWithValue("@MemberId", memberId.ReturnDataJson);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        items.Add(new DataSet_WidthdrawItem
                        {
                            CaseId = $"{reader["CaseId"]}",
                            MemberId = Convert.ToInt32(reader["MemberId"]),
                            ToMemberId = Convert.ToInt32(reader["ToMemberId"]),
                            Id = Convert.ToInt32(reader["Id"]),
                            Value = Convert.ToDecimal(reader["Value"]),
                            FeeRatio = Convert.ToDecimal(reader["FeeRatio"]),
                            Fee1 = Convert.ToDecimal(reader["Fee1"]),
                            Fee2 = Convert.ToDecimal(reader["Fee2"]),
                            Available = Convert.ToDecimal(reader["Available"]),
                            State = $"{reader["State"]}",
                            UpdateTime = Convert.ToDateTime(reader["UpdateTime"]),
                            CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                        });
                    }
                }

                conn.Close();
            }

            return new DataSet_WithdrawItemQuerying
            {
                QueryBy = new { YearMonth = date, Account = account },
                WidthdrawItems = items,
            };
        }

        public void InitializeCaseId()
        {
            using (SqlConnection conn = new SqlConnection(_sqlImp.conn_str))
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            " UPDATE Wallet_WithdrawItem" +
                            " SET CaseId = CONVERT(VARCHAR, CreateTime, 112) + REPLACE(CONVERT(VARCHAR, CreateTime, 114), ':', '')" +
                            " WHERE ISNULL(CaseId, '') = '';";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
