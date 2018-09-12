using System;
using System.Data;


namespace HRList_BL
{
    public interface IHRBuisnessLogic
    {
        string Login { set; }
        string Password { set; }
        string ActiveUser { get; }
        string Rules { get; }
        string Salary { get; }
        DataTable Set { get; }
        event EventHandler Message;
        void InputDB();
        string SalaryCalculation();
    }

    public class HRBuisnessLogic: IHRBuisnessLogic
    {
    
        #region Fields
       
        // Пользователь входящий в систему.
        private string activeuser;
        // зарплата.
        private string salary; 
        private string pass;
        private string log;
        // Права в БД.
        private string rules;
        private string filter; 
        private string SqlQuery_rules;
        // Поле руководитель в БД.
        private string supervisor;
        // Имя отдела.
        private string nameunit;
        // Имя группы.
        private string namesubunit;
        private DataTable set;
        private CountingModul _countingModul;
        private HRModul _hrModul;
        private SQLQueries _sqlQueries;
        #endregion

        public HRBuisnessLogic()
        {
            _countingModul = new CountingModul();
            _hrModul = new HRModul();
            _sqlQueries = new SQLQueries();

        }

        #region Properties

        public string Login
        {
            set { log = value; }
        }

        public string Password
        {
            set { pass = value; }
        }

        public string ActiveUser
        {
            get { return activeuser; }
        }

        public string Rules
        {
            get { return rules; }
        }

        public string Salary
        {
            get { return salary; }
        }

        public DataTable Set
        {
            get { return set; }
        }

        #endregion

        #region Methods
        public string SalaryCalculation()
        {
            DBConnect DBexemp = new DBConnect();
            string SqlQuery = SQLQueries.Instance.GeneralQuery;
            DBexemp.ConnectToDB(SqlQuery);
            DataSet ds = DBexemp.DataSet;
            Worker worker = new Worker();
            double salary_1 = _countingModul.BaseSalary(activeuser, ds) + _countingModul.BonusExperience(activeuser, ds) + _countingModul.BonusSubbordinates(activeuser, ds);
            salary = salary_1.ToString();
            return salary;
        }

        // Авторизация в БД.
        public void InputDB() 
        {
            activeuser = "";
            DBConnect DBexemp = new DBConnect();
            string SqlQuery_input = SQLQueries.Instance.InputQuery;
            DBexemp.ConnectToDB(SqlQuery_input);
            foreach (DataRow row in DBexemp.DataSet.Tables[0].Rows)
            {
                if (log == row[1].ToString() && pass == row[2].ToString())
                {
                    activeuser = log;
                    rules = row[7].ToString();
                    supervisor = row[3].ToString();
                    nameunit = row[5].ToString();
                    namesubunit = row[6].ToString();
                    switch (rules)
                    {
                        case "Employee":
                            {
                                SqlQuery_rules = SQLQueries.Instance.EmployeeAccessRules;
                                filter = string.Format("[FullName]='{0}'", log);
                            }
                            break;
                        case "Manager":
                            {
                                SqlQuery_rules = SQLQueries.Instance.ManagerAccessRules;
                                filter = string.Format("[Name_Unit]='{0}' AND [Name_SubUnit]='{1}'", nameunit, namesubunit);
                            }
                            break;
                        case "Salesman":
                            {
                                SqlQuery_rules = SQLQueries.Instance.SalesmanAccessRules;
                                filter = string.Format("[Name_Unit]='{0}'", nameunit);
                            }
                            break;
                        case "HR_Manager":
                            {
                                SqlQuery_rules = SQLQueries.Instance.HRManagerAccessRules;
                                filter = string.Empty;
                            }
                            break;
                        case "Administrator":
                            {
                                SqlQuery_rules = SQLQueries.Instance.AdministratorAccessRules;
                                filter = string.Empty;
                            }
                            break;
                        default:
                            {
                                SqlQuery_rules = "";
                                filter = "";
                            }
                            break;
                    }
                    DBexemp.ConnectToDB(SqlQuery_rules);
                    DBexemp.DataSet.Tables[0].DefaultView.RowFilter = filter;
                    set = DBexemp.DataSet.Tables[0];
                    break;
                }
            }
            if (activeuser == "") Message(this, EventArgs.Empty);
        }
        #endregion

        #region Events
        public event EventHandler Message;
        #endregion
    }
}
