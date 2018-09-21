using System;
using System.Data;
using System.Collections.Generic;


namespace HRList_BL
{
    public class HRBuisnessLogic : IHRBuisnessLogic
    {
        #region Fields
        private string _activeUser;
        private string _rulesUser;
        private string _password;
        private string _login;
        private int _idRules;
        private bool _isInputSuccessful;
        private DataTable _table;
        private CountingModul _countingModul;
        private DBConnect _dbExemplar;
        private Dictionary<string, string> _unitList;
        private Dictionary<string, string> _subUnitList;
        private Dictionary<string, string> _positionList;
        private Dictionary<string, string> _accessRulesList;
        #endregion

        public HRBuisnessLogic()
        {
            _countingModul = new CountingModul();
            _table = new DataTable();
            _dbExemplar = new DBConnect();
            _unitList = new Dictionary<string, string>();
            _subUnitList = new Dictionary<string, string>();
            _positionList = new Dictionary<string, string>();
            _accessRulesList = new Dictionary<string, string>();
        }

        #region Events
        public event EventHandler Message;
        #endregion

        #region Properties

        public string Login
        {
            set { _login = value; }
        }

        public string Password
        {
            set { _password = value; }
        }

        public string ActiveUser
        {
            get { return _activeUser; }
        }

        public string RulesUser
        {
            get { return _rulesUser; }
        }

        public int IdRules
        {
            get { return _idRules; }
        }

        public DataTable Table
        {
            get { return _table; }
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


        #endregion

        public Dictionary<string, string> ReportCreate(string name, DataTable table)
        {
            string filter = string.Format("FullName='{0}'", name);
            DataRow[] row = table.Select(filter);

            Dictionary<string, string> report = new Dictionary<string, string>();
            report.Add("ФИО Работника: ", name);
            report.Add("Должность: ", row[0].Field<String>("Position"));
            report.Add("Количество подчиненых: ", _countingModul.CountSubordinates(name, table).ToString());
            report.Add("Стаж, лет: ", _countingModul.Experience(name, table).ToString());
            report.Add("Оклад, " + row[0].Field<String>("CurrencyCode") + ": ", row[0].Field<Double>("BaseSalary").ToString());
            report.Add("Надбавка за стаж, %: ", row[0].Field<Double>("BonusRate_1").ToString());
            report.Add("Надбавка за подчиненных, %: ", row[0].Field<Double>("BonusRate_2").ToString());
            report.Add("Заработная плата, начисленная, " + row[0].Field<String>("CurrencyCode") + ": ", (_countingModul.SalaryCalculation(name, table)).ToString());
            return report;
        }

        public void NewUserCreate(Dictionary<string, string> newUser, DataTable table)
        {
            if (_rulesUser == "Administrator" || _rulesUser == "HRManager")
            {
                DataRow row = table.NewRow();
                row["FullName"] = newUser["ФИО Работника: "];
                row["Supervisor"] = Convert.ToBoolean(newUser["Начальник: "]);
                row["Position"] = newUser["Должность: "];
                row["Name_Unit"] = newUser["Отдел: "];
                row["Name_SubUnit"] = newUser["Группа: "];
                row["DataStart"] = DateTime.Today;
                table.Rows.Add(row);
                _dbExemplar.SetData(newUser, _unitList, _subUnitList, _positionList, _accessRulesList, table);
                GetUserList();
            }

        }

        public void GetSettings()
        {
            _dbExemplar.GetSettingsAll(_unitList, _subUnitList, _positionList, _accessRulesList);
        }

        public void UserInput(string login, string password)
        {
            if (_dbExemplar.AuthorizationDB(login, password, out string rulesUser, out int idRules))
            {
                _login = login;
                _password = password;
                _activeUser = login;
                _rulesUser = rulesUser;
                _idRules = idRules;
                _isInputSuccessful = true;
            }
            else
            {
                Message(this, EventArgs.Empty);
            }
        }
        public void UserOutput()
        {
            _login = string.Empty;
            _password = string.Empty;
            _rulesUser = string.Empty;
            _idRules = 0;
            _table.Clear();
            _isInputSuccessful = false;
            _activeUser = string.Empty;
        }

        public void GetUserList()
        {
            if (_isInputSuccessful)
            {
                _table.Clear();
                _dbExemplar.GetUserData(_login, IdRules, _table);
            }

        }
    }
}
