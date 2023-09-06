using NiteenNity_Case_SQL_API.Mode.DataSet.DAO;
using side.DAO;
using side.DataSet;
using side.ServicesImpl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using side.Controller;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Runtime.Remoting.Messaging;

namespace side.Services
{
    public class CancelServices : ICancelServices
    {
        CancelDAO cancelDAO = CancelDAO.getInstance();
        private static CancelServices instance = new CancelServices();
        public static CancelServices getInstance()
        {
            return instance;
        }
        public SQL_ExcuteResult getHasBankType(string account)
        {
            SQL_ExcuteResult result = new SQL_ExcuteResult();
            string bankType = cancelDAO.getHasBankType(account);
            if (string.IsNullOrEmpty(bankType))
            {
                result.isSuccess = false;
                result.FeedbackMsg = bankType;
                result.ReturnDataJson = "沒有合作銀行 - " + bankType;
            }
            else
            {
                result.isSuccess = true;
            }
            return result;
        }
        public string getFeeCategory(string bankType)
        {
            return cancelDAO.getFeeCategory(bankType);
        }
        public string getMemberShip2UserIdAndValue(string account)
        {
            string ans = cancelDAO.getMemberShip2UserIdAndValue(account);
            if (ans.Contains(","))
            {
                return ans;
            }
            else if ("0".Equals(ans) || "-1".Equals(ans))
            {
                MessageBox.Show("MemberShip2_User / Wallet_WalletItem / Config_SystemConfigItem 找不到同個Id的人");
                return "-1,";
            }
            else
            {
                return "-2,";
            }
        }
        public string getWallet_WithdrawItem_Remark(int memberId, string date, string withdrawFeeRatio, string withdrawFee)
        {
            var aa = GetTimeRange(InputDateTimeFormat(date));
            return cancelDAO.getWallet_WithdrawItem_Remark(memberId, aa.startTime.ToString("yyyy-MM-dd HH:mm:ss"), aa.endTime.ToString("yyyy-MM-dd HH:mm:ss"), withdrawFeeRatio, withdrawFee);
        }
        public SQL_ExcuteResult CancelApplyValue_UpdateWallet_WithdrawItem(int memberId, string oldValue, string increment, string date, string withdrawFeeRatio, string withdrawFee)
        {
            SQL_ExcuteResult result = new SQL_ExcuteResult();
            var aa = GetTimeRange(InputDateTimeFormat(date));
            try
            {
                int step = cancelDAO.CancelApplyValue_UpdateWallet_WithdrawItem(memberId, aa.startTime.ToString("yyyy-MM-dd HH:mm:ss"), aa.endTime.ToString("yyyy-MM-dd HH:mm:ss"), withdrawFeeRatio, withdrawFee);

                if (step == 1)
                {
                    result.isSuccess = true;
                    result.FeedbackMsg = "Step1 更新成功 提領紀錄 Wallet_WithdrawItem";
                    result.ReturnDataJson = "{\"memeberId\":\"" + memberId + "\",\"異動後金額\":\"" + Convert.ToString((int)decimal.Parse(oldValue) + (int)decimal.Parse(increment)) + "\",\"異動金額\":\"" + increment + "\",\"提交時間\":\"" + date + "\"}";
                }
                else if (step > 1)
                {
                    result.isSuccess = false;
                    result.FeedbackMsg = "Step1 更新失敗 資料筆數 > 1, Wallet_WithdrawItem";
                    result.ReturnDataJson = "{\"functionEroor\":\"Step1 cancelDAO.UpdateWallet_WithdrawItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + increment + "\",\"提交時間\":\"" + date + "\"}";
                }
                else
                {
                    result.isSuccess = false;
                    result.FeedbackMsg = "Step1 更新失敗 找不到資料 Wallet_WithdrawItem";
                    result.ReturnDataJson = "{\"functionEroor\":\"Step1 cancelDAO.UpdateWallet_WithdrawItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + increment + "\",\"提交時間\":\"" + date + "\"}";
                }

                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                result.isSuccess = false;
                result.FeedbackMsg = "Step1 更新失敗 提領紀錄 Wallet_WithdrawItem";
                result.ReturnDataJson = "{\"functionEroor\":\"Step1 cancelDAO.UpdateWallet_WithdrawItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + increment + "\",\"提交時間\":\"" + date + "\"}";

                return result;
            }
        }
        public SQL_ExcuteResult CancelApplyValue_UpdateWallet_WalletItem(int memberId, string oldValue, string value, string withdrawFeeRatio, string withdrawFee)
        {
            SQL_ExcuteResult result = new SQL_ExcuteResult();
            try
            {
                int step = cancelDAO.CancelApplyValue_UpdateWallet_WalletItem(memberId, value);

                if (step == 1)
                {
                    result.isSuccess = true;
                    result.FeedbackMsg = "Step2 更新成功 原始金額 Wallet_WalletItem";
                    result.ReturnDataJson = "{\"memeberId\":\"" + memberId + "\",\"異動後金額\":\"" + Convert.ToString((int)decimal.Parse(oldValue) + (int)decimal.Parse(value)) + "\",\"異動金額\":\"" + value + "\"}";
                }
                else if (step > 1)
                {
                    result.isSuccess = false;
                    result.FeedbackMsg = "Step2 更新失敗 資料筆數 > 1, Wallet_WalletItem";
                    result.ReturnDataJson = "{\"functionEroor\":\"Step2 cancelDAO.UpdateWallet_WalletItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + value + "\"}";
                }
                else
                {
                    result.isSuccess = false;
                    result.FeedbackMsg = "Step2 更新失敗 找不到資料 Wallet_WalletItem";
                    result.ReturnDataJson = "{\"functionEroor\":\"Step2 cancelDAO.UpdateWallet_WalletItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + value + "\"}";
                }

                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                result.isSuccess = false;
                result.FeedbackMsg = "Step2 更新失敗 原始金額 Wallet_WithdrawItem";
                result.ReturnDataJson = "{\"functionEroor\":\"Step2 cancelDAO.UpdateWallet_WalletItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + value + "\"}";

                return result;
            }
        }
        public SQL_ExcuteResult CancelApplyValue_InsertWallet_WalletRecordItem(int memberId, string oldValue, string value, string date, string editor, string withdrawFeeRatio, string withdrawFee, string remark)
        {
            var aa = GetTimeRange(InputDateTimeFormat(date));
            SQL_ExcuteResult result = new SQL_ExcuteResult();
            try
            {
                int step = cancelDAO.CancelApplyValue_InsertWallet_WalletRecordItem(memberId, value, aa.startTime.ToString("yyyy-MM-dd HH:mm:ss"), aa.endTime.ToString("yyyy-MM-dd HH:mm:ss"), editor, withdrawFeeRatio, withdrawFee, remark);

                if (step == 1)
                {
                    result.isSuccess = true;
                    result.FeedbackMsg = "Step3 新增成功 提領紀錄 Wallet_WalletRecordItem";
                    result.ReturnDataJson = "{\"memeberId\":\"" + memberId + "\",\"異動後金額\":\"" + Convert.ToString((int)decimal.Parse(oldValue) + (int)decimal.Parse(value)) + "\",\"異動金額\":\"" + value + "\",\"提交時間\":\"" + date + "\"}";
                }
                else if (step > 1)
                {
                    result.isSuccess = false;
                    result.FeedbackMsg = "Step3 新增失敗 資料筆數 > 1, Wallet_WalletRecordItem";
                    result.ReturnDataJson = "{\"functionEroor\":\"Step3 cancelDAO.InsertWallet_WalletRecordItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + value + "\",\"提交時間\":\"" + date + "\"}";
                }
                else
                {
                    result.isSuccess = false;
                    result.FeedbackMsg = "Step3 新增失敗 找不到資料 Wallet_WalletRecordItem";
                    result.ReturnDataJson = "{\"functionEroor\":\"Step3 cancelDAO.InsertWallet_WalletRecordItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + value + "\",\"提交時間\":\"" + date + "\"}";
                }

                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                result.isSuccess = false;
                result.FeedbackMsg = "Step3 新增失敗 歷史紀錄 Wallet_WalletRecordItem";
                result.ReturnDataJson = "{\"functionEroor\":\"Step3 cancelDAO.InsertWallet_WalletRecordItem\",\"memeberId\":\"" + memberId + "\",\"原始金額\":\"" + oldValue + "\",\"異動金額\":\"" + value + "\",\"提交時間\":\"" + date + "\"}";

                return result;
            }
        }
        private DateTime InputDateTimeFormat(string date)
        {
            return new DateTime(Convert.ToInt32(date.Substring(0, 4))
               , Convert.ToInt32(date.Substring(5, 2))
               , Convert.ToInt32(date.Substring(8, 2))
               , Convert.ToInt32(date.Substring(11, 2))
               , Convert.ToInt32(date.Substring(14, 2))
               , Convert.ToInt32(date.Substring(17, 2)));
        }
        public static (DateTime startTime, DateTime endTime) GetTimeRange(DateTime time)
        {
            var startTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
            var endTime = startTime.AddSeconds(1);

            return (startTime, endTime);
        }
    }
}
