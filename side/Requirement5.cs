using Newtonsoft.Json;
using NiteenNity_Case_SQL_API.Mode.Abstract;
using side.Controller;
using System;
using System.Windows.Forms;

namespace side
{
    public partial class Requirement5 : Form
    {
        private readonly WithdrawItemController controller = WithdrawItemController.GetInstance();
        private readonly GameUserItemController gameUserItemController = GameUserItemController.GetInstance();

        public Requirement5()
        {
            InitializeComponent();
        }

        private void BtnGetNewCaseId_Click(object sender, EventArgs e)
        {
            string newCaseId = controller.GetNewCaseId();

            TxtNewCaseId.Text = newCaseId;
        }

        private void BtnCopyCaseId_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtNewCaseId.Text))
            {
                Clipboard.SetText(TxtNewCaseId.Text);

                MessageBox.Show("已複製");
            }
        }

        private void InitConnectionString()
        {
            ImplmentSQL sqlImp = ImplmentSQL.getInstance();
            if (!string.IsNullOrEmpty(TxtConnectionString.Text))
            {
                sqlImp.conn_str = TxtConnectionString.Text;
                return;
            }
            sqlImp.conn_str = "data source=DESKTOP-C8S8A02\\MSSQLSERVER01;initial catalog=bu_test_Local;integrated security=True;";
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            InitConnectionString();

            if (string.IsNullOrEmpty(TxtMonth.Text) || !DateTime.TryParse(TxtMonth.Text, out DateTime _))
            {
                MessageBox.Show("請輸入正確的年月");
                return;
            }

            if (string.IsNullOrEmpty(TxtAccount.Text))
            {
                MessageBox.Show("請輸入帳號");
                return;
            }

            var result = controller.GetQuerying(TxtAccount.Text, TxtMonth.Text);

            TxtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        private void BtnETL_Click(object sender, EventArgs e)
        {
            InitConnectionString();

            try
            {
                controller.ETL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnQryByCaseId_Click(object sender, EventArgs e)
        {
            InitConnectionString();

            if (string.IsNullOrEmpty(TxtCaseId.Text))
            {
                MessageBox.Show("請輸入單號");
                return;
            }

            var result = controller.GetQuerying(TxtCaseId.Text);

            TxtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        private void BtnRegisterNum_Click(object sender, EventArgs e)
        {
            InitConnectionString();

            if (string.IsNullOrEmpty(TxtRegistDate.Text) || !DateTime.TryParse(TxtRegistDate.Text, out DateTime _))
            {
                MessageBox.Show("請輸入正確的日期");
                return;
            }

            var result = gameUserItemController.GetRegisterNum(TxtRegistDate.Text);

            NudRegisterNum.Value = Convert.ToDecimal(result.ReturnDataJson);
        }
    }
}
