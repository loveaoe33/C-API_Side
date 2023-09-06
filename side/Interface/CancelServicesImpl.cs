using NiteenNity_Case_SQL_API.Mode.DataSet.DAO;
using side.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace side.ServicesImpl
{
    public interface ICancelServices
    {
        SQL_ExcuteResult getHasBankType(string account);
        string getFeeCategory(string bankType);
        string getMemberShip2UserIdAndValue(string account);
        string getWallet_WithdrawItem_Remark(int memberId, string date, string withdrawFeeRatio, string withdrawFee);
        SQL_ExcuteResult CancelApplyValue_UpdateWallet_WithdrawItem(int memberId, string oldValue, string increment, string date, string withdrawFeeRatio, string withdrawFee);
        SQL_ExcuteResult CancelApplyValue_UpdateWallet_WalletItem(int memberId, string oldValue, string value, string withdrawFeeRatio, string withdrawFee);
        SQL_ExcuteResult CancelApplyValue_InsertWallet_WalletRecordItem(int memberId, string oldValue, string value, string date, string editor, string withdrawFeeRatio, string withdrawFee, string remark);
    }
}
