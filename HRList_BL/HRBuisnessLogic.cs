using System;
using System.Data;
using System.Collections.Generic;


namespace HRList_BL
{
    public class HRBuisnessLogic : IHRBuisnessLogic
    {
        #region Fields

        // Пользователь входящий в систему.
        private string _activeUser;
        private string _rulesUser;
        private bool _isInputSuccessful;
        // зарплата.
        private string salary;
        private string _password;
        private string _login;
        // Права в БД.
        private int _idRules;
        private string filter;
        private string SqlQuery_rules;
        // Поле руководитель в БД.
        private string supervisor;
        // Имя отдела.
        private string nameunit;
        // Имя группы.
        private string namesubunit;
        private DataTable _table;
        private CountingModul _countingModul;
        private HRModul _hrModul;
        private SQLQueries _sqlQueries;
        DBConnect _dbExemplar;
        private Worker _worker;
        private Dictionary<string, string> _unitList;
        private Dictionary<string, string> _subUnitList;
        private Dictionary<string, string> _positionList;
        private Dictionary<string, string> _accessRulesList;


        #endregion

        public HRBuisnessLogic()
        {
            _countingModul = new CountingModul();
            _hrModul = new HRModul();
            _sqlQueries = new SQLQueries();
            _dbExemplar = new DBConnect();
            _unitList = new Dictionary<string, string>();
            _subUnitList = new Dictionary<string, string>();
            _positionList = new Dictionary<string, string>();
            _accessRulesList = new Dictionary<string, string>();

    }

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

        public string Salary
        {
            get { return salary; }
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


        public void SalaryCalculation()
        {
          //  _worker.Experience = _countingModul.Experience(_activeUser, Table);

          //  double salary_1 = _countingModul.BaseSalary(_activeUser, ds) + _countingModul.BonusExperience(_activeUser, ds) + _countingModul.BonusSubbordinates(_activeUser, ds);


        }

        public Dictionary<string, string> ReportCreate(string name, DataTable table)
        {
            Worker workerCard = new Worker(name);
            string filter = string.Format("FullName='{0}'", name);
            DataRow[] rows = table.Select(filter);
            workerCard.OfficePosition = rows[0].Field<String>("Position");
            workerCard.StartTime = rows[0].Field<DateTime>("DataStart");
            workerCard.Experience = _countingModul.Experience(name, table);
            workerCard.CountSubbordinates = _countingModul.CountSubordinates(name, table);
            workerCard.BaseRate = rows[0].Field<Double>("BaseSalary");
            workerCard.BonusPercent_1 = rows[0].Field<Double>("BonusRate_1");
            workerCard.BonusPercent_2 = rows[0].Field<Double>("BonusRate_2");

            Dictionary<string, string> report = new Dictionary<string, string>();
            report.Add("ФИО Работника: ", workerCard.Name);
            report.Add("Должность: ", workerCard.OfficePosition);
            report.Add("Количество подчиненых: ", workerCard.CountSubbordinates.ToString());
            report.Add("Стаж, лет: ", workerCard.Experience.ToString());
            report.Add("Оклад, " + rows[0].Field<String>("CurrencyCode")+": ", workerCard.BaseRate.ToString());
            report.Add("Надбавка за стаж, %: ", workerCard.BonusPercent_1.ToString());
            report.Add("Надбавка за подчиненных, %: ", workerCard.BonusPercent_2.ToString());
            report.Add("Заработная плата, начисленная, " + rows[0].Field<String>("CurrencyCode") + ": ", (_countingModul.BaseSalary(name, table) + _countingModul.BonusExperience(name, table) + _countingModul.BonusSubbordinates(name, table)).ToString());
            return report;
        }


        public void NewUserCreate(Dictionary<string, string> newUser, DataTable table)
        {
            DataRow row = table.NewRow();
            row["FullName"] = newUser["ФИО Работника: "];
            row["Supervisor"] = Convert.ToBoolean(newUser["Начальник: "]);
            row["Position"] = newUser["Должность: "];
            row["Name_Unit"] = newUser["Отдел: "];
            row["Name_SubUnit"] = newUser["Группа: "];
            //row["BaseSalary"] = Convert.ToDouble(newUser["Оклад: "]);
           // row["CurrencyCode"] = "$";
            row["DataStart"] = DateTime.Today;
           // row["BonusRate_1"] = Convert.ToDouble(newUser["Надбавка за стаж: "]);
            //row["BonusRate_2"] = Convert.ToDouble(newUser["Надбавка за подчиненных: "]);
            //row["ExpBonusLimit"] = 30;
            table.Rows.Add(row);
            _dbExemplar.Table = table;
            _dbExemplar.SetData(newUser, _unitList, _subUnitList, _positionList, _accessRulesList);
            _dbExemplar.GetUserData(_login, IdRules);
        }







        public void GetSettings()
        {
            _dbExemplar.GetSettingsAll(_unitList, _subUnitList, _positionList, _accessRulesList);
        }






        public void UserInput(string login, string password)
        {
            string rulesUser;
            int  idRules;
            if (_dbExemplar.AuthorizationDB(login, password, out rulesUser, out idRules))
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
            _table.Clear();
            _isInputSuccessful = false;
            _activeUser = "Вход не выполнен";
        }

        public void GetUserList() 
        {
            if (_isInputSuccessful)
            {
                _dbExemplar.GetUserData(_login,IdRules);
                _table = _dbExemplar.Table;
            }
     
        }





        #region Events
        public event EventHandler Message;
        #endregion
    }
}
