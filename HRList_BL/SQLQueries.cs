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
        private string _generalQuery = @"SELECT  s.id_human, s.fullname, s.supervisor, s2.position, u.name_unit, s3.name_subunit, s2.basesalary, s2.currencycode, s.DataStart, s2.bonusrate_1, s2.bonusrate_2, s2.expbonuslimit
                                    FROM  StaffList s, StaffRules s2, SubUnit s3, Unit u, Access_Rights a
                                    WHERE s.id_position=s2.id_position AND s.id_subunit=s3.id_subunit AND s.id_unit=u.id_unit AND a.id_access=s.id_access";

        private string _userListQuery = @"SELECT s.FullName, s.Password, s.ID_Access, a.Rules
                                    FROM  StaffList s, Access_Rights a
                                    WHERE a.ID_Access=s.ID_Access";


        private string _subunitQuery = @"SELECT s2.ID_SubUnit, s2.Name_SubUnit
                                        FROM   SubUnit s2";

        private string _unitQuery = @"SELECT  u.ID_Unit, u.Name_Unit
                                    FROM   Unit u";

        private string _accessRulesQuery = @"SELECT  a.ID_Access, a.Rules
                                           FROM   Access_Rights a";

        private string _positionQuery = @"SELECT  s2.ID_Position, s2.Position
                                        FROM   StaffRules s2";



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

        public string UserListQuery
        {
            get { return _userListQuery; }
        }


        public string SubunitQuery
        {
            get { return _subunitQuery; }
        }

        public string UnitQuery
        {
            get { return _unitQuery; }
        }

        public string AccessRulesQuery
        {
            get { return _accessRulesQuery; }
        }

        public string PositionQuery
        {
            get { return _positionQuery; }
        }

        #endregion

        public string UserRulesFilterCreate(int idRule, string login, string nameunit, string namesubunit)
        {
            string filter;
            switch (idRule)
            {
                case 1:
                    {
                        filter = string.Format("[FullName]='{0}'", login);
                    }
                    break;
                case 2:
                    {
                        filter = string.Format("[Name_Unit]='{0}' AND [Name_SubUnit]='{1}'", nameunit, namesubunit);
                    }
                    break;
                case 3:
                    {
                        filter = string.Format("[Name_Unit]='{0}'", nameunit);
                    }
                    break;
                case 4:
                    {
                        filter = string.Empty;
                    }
                    break;
                case 5:
                    {

                        filter = string.Empty;
                    }
                    break;
                default:
                    {
                        filter = "";
                    }
                    break;
            }

            return filter;
        }
    }
}
