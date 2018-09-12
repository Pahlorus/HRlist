using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRList_BL
{
    // Класс реализован для удобства, чтобы собрать все запросы в одном месте. Далее требуется иная реализация хранения запросов (В БД, ХП, XML и т.п.) 
    // или формирование инструмента для редактирования запросов без перекомпилирования.
    class SQLQueries
    {
        private string _generalQuery = @"SELECT p.ID_Human, p.FullName, p.DataStart, p.PersonalBonus, p.Supervisor, s1.Position, s1.BaseSalary, s1.CurrencyCode, s1.BonusRate_1, s1.BonusRate_2, s1.ExpBonusLimit, u.Name_Unit, s2.Name_SubUnit
                                    FROM PrivateList p 
	                                INNER JOIN Staff s ON ( p.ID_Human = s.ID_Human)  
		                            INNER JOIN StaffRules s1 ON ( s.ID_Position = s1.ID_Position)  
                                    INNER JOIN Unit u ON ( s.ID_Unit = u.ID_Unit)  
                                    INNER JOIN SubUnit s2 ON ( s.ID_SubUnit = s2.ID_SubUnit  )";

        private string _inputQuery = @"SELECT  p.id_human, p.fullname, p.password, p.supervisor, s2.position, u.name_unit, s3.name_subunit, a.rules
                                    FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                    WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND s.id_unit=u.id_unit AND a.id_access=s.id_access";

        private string _employeeAccessRules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, s3.name_subunit
                                            FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                            WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND s.id_unit=u.id_unit AND a.id_access=s.id_access";

        private string _managerAccessRules =  @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, s3.name_subunit, s2.basesalary, s2.currencycode
                                             FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                             WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND s.id_unit=u.id_unit AND a.id_access=s.id_access";


        private string _salesmanAccessRules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, s3.name_subunit, s2.basesalary, s2.currencycode, s2.bonusrate_1, s2.bonusrate_2, s2.expbonuslimit
                                            FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                            WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND s.id_unit=u.id_unit AND a.id_access=s.id_access";

        private string _hrmanagerAccessRules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, s3.name_subunit, s2.basesalary, s2.currencycode, s2.bonusrate_1, s2.bonusrate_2, s2.expbonuslimit
                                            FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                            WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND s.id_unit=u.id_unit AND a.id_access=s.id_access";

        private string _administratorAccessRules = @"SELECT  p.id_human, p.fullname, p.supervisor, s2.position, u.name_unit, s3.name_subunit, s2.basesalary, s2.currencycode, s2.bonusrate_1, s2.bonusrate_2, s2.expbonuslimit
                                                FROM  privatelist p, staff s, staffrules s2, subunit s3, unit u, access_rights a
                                                WHERE  p.id_human=s.id_human AND s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND s.id_unit=u.id_unit AND a.id_access=s.id_access";

        public static SQLQueries Instance
        {
            get; 
            private set; 
        }

        public SQLQueries()
        {
            Instance = this;
        }

        #region Properties

        public string GeneralQuery
        {
            get { return _generalQuery; }
        }

        public string InputQuery
        {
            get { return _inputQuery; }
        }

        public string EmployeeAccessRules
        {
            get { return _employeeAccessRules; }
        }

        public string ManagerAccessRules
        {
            get { return _managerAccessRules; }
        }

        public string SalesmanAccessRules
        {
            get { return _salesmanAccessRules; }
        }

        public string HRManagerAccessRules
        {
            get { return _hrmanagerAccessRules; }
        }

        public string AdministratorAccessRules
        {
            get { return _administratorAccessRules; }
        }
        #endregion




    }
}
