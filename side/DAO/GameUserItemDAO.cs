using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace side.DAO
{
    internal class GameUserItemDAO
    {
        /// <summary>
        /// 資料庫連接字串。
        /// </summary>
        public string ConnectionString { get; set; }

        private static readonly GameUserItemDAO _instance = new GameUserItemDAO();


        /// <summary>
        /// 取得執行實體。
        /// </summary>
        /// <returns><see cref="GameUserItemDAO"/> 的執行實體。</returns>
        public static GameUserItemDAO GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 取得指定日期下的註冊人數
        /// </summary>
        /// <param name="date">要查詢的日期</param>
        /// <returns></returns>
        public int GetRegisterNumberOfPeople(string date)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                DateTime dtStart = Convert.ToDateTime(date).Date;
                DateTime dtEnd = Convert.ToDateTime(date).AddDays(1).Date;

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        " SELECT COUNT(*)" +
                        " FROM MemberShip2_User" +
                        " WHERE CreateTime >= @StartDate" +
                        "   AND CreateTime < @EndDate;";

                    cmd.Parameters.AddWithValue("@StartDate", dtStart);
                    cmd.Parameters.AddWithValue("@EndDate", dtEnd);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dt.Rows[0][0]);
                    }
                }

                conn.Close();
            }

            return result;
        }
    }
}
