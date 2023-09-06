using NiteenNity_Case_SQL_API.Controller;
using NiteenNity_Case_SQL_API.Mode.Abstract;
using NiteenNity_Case_SQL_API.Mode.DataSet.DAO;
using side.Controller;
using side.DataSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace side
{
    public partial class Requirement4 : Form
    {
        DataSet_WithdrawDetail dataSet_WithdrawDetail;
        private const string WithdrawApiUrl = "http://localhost:5000/withdrawal/withdraw";
        private const string CancelApiUrl = "http://localhost:5000/withdrawal/cancel";

        public Requirement4()
        {
            InitializeComponent();
        }
        private void detailButton_Click(object sender, EventArgs e)
        {
            cancelButton.Enabled = true;
            dataSet_WithdrawDetail = new DataSet_WithdrawDetail
            {
                memberId =4,
                name = "cas1",
                bankData = new DataSet_WithdrawDetail.BankData
                {
                    bankName = "004-台灣銀行",
                    branch = "高雄分行",
                    passbookName = "00000000000000",
                    account = "00000000000000"
                },
                withdrawData = new DataSet_WithdrawDetail.WithdrawData
                {
                    value = "1924",
                    feeRatio = "0",
                    fee = "200",
                    available = "1256"
                },
                submissionTime = "2023-09-06 11:01:51.973"
            };
            memberIdTextBox.Text = Convert.ToString(dataSet_WithdrawDetail.memberId);

            textBox9.Text = dataSet_WithdrawDetail.name;
            textBox1.Text = dataSet_WithdrawDetail.bankData.bankName;
            textBox2.Text = dataSet_WithdrawDetail.bankData.branch;
            textBox3.Text = dataSet_WithdrawDetail.bankData.passbookName;
            textBox4.Text = dataSet_WithdrawDetail.bankData.account;

            amountTextBox.Text = dataSet_WithdrawDetail.withdrawData.value;
            textBox5.Text = dataSet_WithdrawDetail.withdrawData.feeRatio;
            textBox6.Text = dataSet_WithdrawDetail.withdrawData.fee;
            textBox7.Text = dataSet_WithdrawDetail.withdrawData.available;
            textBox8.Text = dataSet_WithdrawDetail.submissionTime;
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            SQL_ExcuteResult result = new SQL_ExcuteResult();
            string account = "cas1";
            string editor = " ";
            DataSet_CancelApplyValue dataSet_CancelApplyVaule = Init();

            CancelController cancelController = CancelController.getInstance();

            result = cancelController.CancelApplyValue(dataSet_CancelApplyVaule, account, editor);

            MessageBox.Show(string.Format("{0}\r\nReturn Data：{1}",result.FeedbackMsg,result.ReturnDataJson));
        }

        private DataSet_CancelApplyValue Init()
        {
            DataSet_CancelApplyValue dataSet_CancelApplyVaule = new DataSet_CancelApplyValue
            {
                memberId = dataSet_WithdrawDetail.memberId,
                name = dataSet_WithdrawDetail.name,
                bankData = new DataSet_CancelApplyValue.BankData
                {
                    bankName = dataSet_WithdrawDetail.bankData.bankName,
                    branch = dataSet_WithdrawDetail.bankData.branch,
                    passbookName = dataSet_WithdrawDetail.bankData.passbookName,
                    account = dataSet_WithdrawDetail.bankData.account
                },
                withdrawData = new DataSet_CancelApplyValue.WithdrawData
                {
                    value = dataSet_WithdrawDetail.withdrawData.value,
                    feeRatio = dataSet_WithdrawDetail.withdrawData.feeRatio,
                    fee = dataSet_WithdrawDetail.withdrawData.fee,
                    available = dataSet_WithdrawDetail.withdrawData.available
                },
                submissionTime = dataSet_WithdrawDetail.submissionTime
            };

            return dataSet_CancelApplyVaule;
        }
    }
}
