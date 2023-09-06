using Newtonsoft.Json;
using NiteenNity_Case_SQL_API.Mode.Abstract;
using NiteenNity_Case_SQL_API.Mode.DataSet.DAO;
using side.DAO;
using side.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace side.Controller
{
    public class GameUserItemController : IConnStrSingleton
    {
        private readonly GameUserItemDAO _gameUserItemInstance = GameUserItemDAO.GetInstance();

        private readonly static GameUserItemController _instance = new GameUserItemController();

        public static GameUserItemController GetInstance()
        {
            return _instance;
        }

        /// <inheritdoc/>
        public void SetConnectionString()
        {
            ImplmentSQL _sqlImp = ImplmentSQL.getInstance();
            if (!string.IsNullOrEmpty(_sqlImp.conn_str))
            {
                _gameUserItemInstance.ConnectionString = _sqlImp.conn_str;
                return;
            }

            throw new Exception("尚未設定連線字串初始化");
        }

        /// <summary>
        /// 取得日期內的註冊人數
        /// </summary>
        /// <param name="date">指定要查詢的日期</param>
        /// <returns></returns>
        public SQL_ExcuteResult GetRegisterNum(string date)
        {
            bool isSucc = false;
            string feedback;
            string dataJson = "";
            try
            {
                feedback = "Success";
                isSucc = true;

                var data = _gameUserItemInstance.GetRegisterNumberOfPeople(date);
                dataJson = JsonConvert.SerializeObject(data);
            }
            catch (Exception ex)
            {
                feedback = $"Fail，{ex.Message}";
            }

            return new SQL_ExcuteResult
            {
                isSuccess = isSucc,
                FeedbackMsg = feedback,
                ReturnDataJson = dataJson,
            };
        }
    }
}
