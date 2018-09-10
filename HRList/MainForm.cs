
using System;
using System.Data;
using System.Windows.Forms;


namespace HRList
{
    public interface IMainForm
    {
        event EventHandler FormShown;
        event EventHandler InputBDMenuClick;
        event EventHandler ShowResult;
       
        string SetActiveUser { set; }
        string Result { set; }
        DataTable Set { set; }
    }
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            Shown += FormShown;
            InputBDMenuItem.Click += InputBDMenuItem_Click;
            ResultCalculateMenuItem.Click += ResultCalculateMenuItem_Click;
        }

        private string result;

        #region Properties
        public string SetActiveUser
        {
            set { lblUserName.Text = value; }
        }
        public string Result
        {
            set { result = value; }
        }
        public DataTable Set
        {
            set { DBView1.DataSource = value; }
        }

        #endregion

        #region Methods
        private void InputBDMenuItem_Click(object sender, EventArgs e)
        {
            InputBDMenuClick?.Invoke(this, EventArgs.Empty);
        }

        private void ResultCalculateMenuItem_Click(object sender, EventArgs e)
        {
            ShowResult?.Invoke(this, EventArgs.Empty);
            ResultFormcs resultform = new ResultFormcs();
            resultform.Show(this);
            resultform.StartPosition = FormStartPosition.CenterScreen;
            resultform.res = result;
            resultform.name = lblUserName.Text;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            FormShown?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Events
        public event EventHandler FormShown;
        public event EventHandler InputBDMenuClick;
        public event EventHandler ShowResult;
        #endregion
    }
}
