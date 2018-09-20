
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;


namespace HRList
{
    public interface IMainForm
    {
        event EventHandler FormShown;
        event EventHandler InputBDMenuClick;
        event EventHandler OutputBDMenuClick;
        event EventHandler ShowResult;
        event EventHandler OnAddUser;

        string SetActiveUser { set; }
        string SetRulesUser { set; }
        string Result { set; }
        DataTable Table { set; }
        Dictionary<string, string> Report { set; }
        Dictionary<string, string> EditUser { get;  set; }
       // Dictionary<string, string> ReportCreate(string name, DataTable table);
        Dictionary<string, string> UnitList { get; set; }
        Dictionary<string, string> SubUnitList { get; set; }
        Dictionary<string, string> PositiontList { get; set; }
        Dictionary<string, string> AccessRulesList { get; set; }
        void ColumnWidthSetup();
    }
    public partial class MainForm : Form, IMainForm
    {
        private UserEditForm _editForm;
        private string _result;
        private DataRow row;
        private Dictionary<string, string> _report;
        private Dictionary<string, string> _editUser;
        private Dictionary<string, string> _unitList;
        private Dictionary<string, string> _subUnitList;
        private Dictionary<string, string> _positionList;
        private Dictionary<string, string> _accessRulesList;



        public MainForm()
        {
            InitializeComponent();
            Shown += FormShown;
            InputBDMenuItem.Click += InputBDMenuItem_Click;
            ResultCalculateMenuItem.Click += ResultCalculateMenuItem_Click;
            _editUser = new Dictionary<string, string>();
            _unitList = new Dictionary<string, string>();
            _subUnitList = new Dictionary<string, string>();
            _positionList = new Dictionary<string, string>();
            _accessRulesList = new Dictionary<string, string>();

    }


        #region Properties
        public string SetActiveUser
        {
            set { lblUserName.Text = value; }
        }
        public string SetRulesUser
        {
            set { lblUserRules.Text = value; }
        }

        public string Result
        {
            set { _result = value; }
        }

        public Dictionary<string, string> Report
        {
            set { _report = value; }
        }

        public Dictionary<string, string> EditUser
        {
            get { return _editUser; }
            set { _editUser = value; }
        }

        public Dictionary<string, string> UnitList
        {
            get { return _unitList; }
            set { _unitList = value; }
        }

        public Dictionary<string, string> SubUnitList
        {
            get { return _subUnitList; }
            set { _subUnitList = value; }
        }


        public Dictionary<string, string> PositiontList
        {
            get { return _positionList; }
            set { _positionList = value; }
        }

        public Dictionary<string, string> AccessRulesList
        {
            get { return _accessRulesList; }
            set { _accessRulesList = value; }
        }




        public string  SelectedName
        {

            get { return DBView1.CurrentRow.Cells[1].Value.ToString();}
        }

        public DataTable Table
        {
            set { DBView1.DataSource = value; }
        }

        #endregion

        #region Methods
        private void InputBDMenuItem_Click(object sender, EventArgs e)
        {
            InputBDMenuClick?.Invoke(this, EventArgs.Empty);
        }

        private void OutputDBMenuItem_Click(object sender, EventArgs e)
        {
            OutputBDMenuClick?.Invoke(this, EventArgs.Empty);
        }

        private void ResultCalculateMenuItem_Click(object sender, EventArgs e)
        {
            ShowResult?.Invoke(this, EventArgs.Empty);
            ResultFormcs resultform = new ResultFormcs
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            resultform.Show();
            int count = 0;
            foreach (KeyValuePair<string, string> keyValue in _report)
            {
                count = count + 25;
                Label lbl = new Label
                {
                    AutoSize = true,
                    Text = keyValue.Key + keyValue.Value,
                    Parent = resultform,
                    Left = 20,
                    Top = count,
                    Visible = true
                };
            }

        }

        private void WorkerFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _editForm = new UserEditForm
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            _editForm.OnAddUser += Edittform_OnAddUser;
            _editForm.Shown += _editForm_Shown;

            _editForm.Show();
        }

        private void _editForm_Shown(object sender, EventArgs e)

        {

            foreach (KeyValuePair<string, string> keyValue in PositiontList)
            {
                _editForm.Position.Items.Add(keyValue.Key);
            }
            foreach (KeyValuePair<string, string> keyValue in UnitList)
            {
                _editForm.Unit.Items.Add(keyValue.Key);
            }
            foreach (KeyValuePair<string, string> keyValue in SubUnitList)
            {
                _editForm.SubUnit.Items.Add(keyValue.Key);
            }

            _editForm.Position.SelectedIndex = 0;
            _editForm.Unit.SelectedIndex = 0;
            _editForm.SubUnit.SelectedIndex = 0;


        }

        private void Edittform_OnAddUser(object sender, EventArgs e)
        {
            _editUser.Clear();
            _editUser.Add("ФИО Работника: ", _editForm.UserName);
            _editUser.Add("Начальник: ", _editForm.IsSupervisor.ToString());
            _editUser.Add("Должность: ", _editForm.Position.Text);
            _editUser.Add("Отдел: ", _editForm.Unit.Text);
            _editUser.Add("Группа: ", _editForm.SubUnit.Text);
           // _editUser.Add("Оклад: ", _editForm.BaseRate);
           // _editUser.Add("Надбавка за стаж: ", _editForm.BonusExperience);
           // _editUser.Add("Надбавка за подчиненных: ", _editForm.BonusSubbordinates);
            OnAddUser?.Invoke(this, EventArgs.Empty);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            FormShown?.Invoke(this, EventArgs.Empty);
        }

        public void ColumnWidthSetup()
        {
            DBView1.Columns[1].Width = 300;
            DBView1.Columns[8].Visible = false;
            DBView1.Columns[9].Visible = false;
            DBView1.Columns[10].Visible = false;
        }

        #endregion

        #region Events
        public event EventHandler FormShown;
        public event EventHandler InputBDMenuClick;
        public event EventHandler OutputBDMenuClick;
        public event EventHandler ShowResult;
        public event EventHandler OnAddUser;



        #endregion

    }
}
