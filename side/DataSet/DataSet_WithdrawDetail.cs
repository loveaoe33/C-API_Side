using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace side.DataSet
{
    public class DataSet_WithdrawDetail
    {
        // 顧客 ID
        public int memberId { get; set; }
        // 顧客姓名
        public string name { get; set; }
        // 銀行帳戶
        public BankData bankData { get; set; }
        // 數量
        public WithdrawData withdrawData { get; set; }
        // 提交時間
        public string submissionTime { get; set; }

        public class BankData
        {
            // 銀行
            public string bankName { get; set; }
            // 分行
            public string branch { get; set; }
            // 戶名
            public string passbookName { get; set; }
            // 帳號
            public string account { get; set; }
        }
        public class WithdrawData
        {
            // 金額
            public string value { get; set; }
            // 百分比 - 手續費
            public string feeRatio { get; set; }
            // 固定 - 手續費
            public string fee { get; set; }
            // 實際可得
            public string available { get; set; }
        }
    }
}
