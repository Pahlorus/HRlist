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
        private string ConnectionString = "Data Source = HR_DB.db; Version=3";
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
        #endregion

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
            string SqlQuery = @"SELECT p.ID_Human, p.FullName, p.DataStart, p.PersonalBonus, p.Supervisor, s1.Position, s1.BaseSalary, s1.CurrencyCode, s1.BonusRate_1, s1.BonusRate_2, s1.ExpBonusLimit, u.Name_Unit, s2.Name_SubUnit
            FROM PrivateList p 
	        INNER JOIN Staff s ON ( p.ID_Human = s.ID_Human  )  
		    INNER JOIN StaffRules s1 ON ( s.ID_Position = s1.ID_Position  )  
		    INNER JOIN Unit u ON ( s.ID_Unit = u.ID_Unit  )  
		    INNER JOIN SubUnit s2 ON ( s.ID_SubUnit = s2.ID_SubUnit  )    ";

            DBexemp.ConnectToDB(ConnectionString, SqlQuery);
            DataSet ds = DBexemp.dataset_1;
            Worker worker = new Worker();
            double salary_1 = worker.BaseSalary(activeuser, ds) + worker.BonusExperience(activeuser, ds) + worker.BonusSubbordinates(activeuser, ds);
            salary = salary_1.ToString();
            return salary;
        }

        // Авторизация в БД.
        public void InputDB() 
        {
            activeuser = "";
            DBConnect DBexemp = new DBConnect();
            string SqlQuery_input = @"SELECT  p.id_human, p.fullname, p.password, p.supervisor, s2.position, 
                                    u.name_unit, s3.name_subunit, a.rules
                                    FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                    WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND 
                                    s.id_unit=u.id_unit AND a.id_access=s.id_access";
            DBexemp.ConnectToDB(ConnectionString, SqlQuery_input);
            foreach (DataRow row in DBexemp.dataset_1.Tables[0].Rows)
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
                                SqlQuery_rules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, 
                        s3.name_subunit
                        FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                        WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND 
                        s.id_unit=u.id_unit AND a.id_access=s.id_access";
                                filter = string.Format("[FullName]='{0}'", log);
                            }
                            break;
                        case "Manager":
                            {
                                SqlQuery_rules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, 
                                     s3.name_subunit, s2.basesalary, s2.currencycode
                                     FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                     WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND 
                                     s.id_unit=u.id_unit AND a.id_access=s.id_access";
                                filter = string.Format("[Name_Unit]='{0}' AND [Name_SubUnit]='{1}'", nameunit, namesubunit);
                            }
                            break;
                        case "Salesman":
                            {
                                SqlQuery_rules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, 
                                     s3.name_subunit, s2.basesalary, s2.currencycode, s2.bonusrate_1, s2.bonusrate_2, 
                                     s2.expbonuslimit
                                     FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                     WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND 
                                     s.id_unit=u.id_unit AND a.id_access=s.id_access";
                                filter = string.Format("[Name_Unit]='{0}'", nameunit);
                            }
                            break;
                        case "HR_Manager":
                            {
                                SqlQuery_rules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, 
                                     s3.name_subunit, s2.basesalary, s2.currencycode, s2.bonusrate_1, s2.bonusrate_2, 
                                     s2.expbonuslimit
                                     FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                     WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND 
                                     s.id_unit=u.id_unit AND a.id_access=s.id_access";
                                filter = string.Empty;
                            }
                            break;
                        case "Administrator":
                            {
                                SqlQuery_rules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, 
                                     s3.name_subunit, s2.basesalary, s2.currencycode, s2.bonusrate_1, s2.bonusrate_2, 
                                     s2.expbonuslimit
                                     FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                     WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND 
                                     s.id_unit=u.id_unit AND a.id_access=s.id_access";
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
                    DBexemp.ConnectToDB(ConnectionString, SqlQuery_rules);
                    DBexemp.dataset_1.Tables[0].DefaultView.RowFilter = filter;
                    set = DBexemp.dataset_1.Tables[0];
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
