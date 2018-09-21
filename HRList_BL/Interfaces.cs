using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRList_BL
{
    interface IDb
    {
        bool AuthorizationDB(string login, string password, out string rules, out int idRules);
        void GetUserData(string login, int idRules, DataTable table);
    }

    public interface IHRBuisnessLogic
    {
        string Login { set; }
        string Password { set; }
        string ActiveUser { get; }
        string RulesUser { get; }
        int IdRules { get; }
        DataTable Table{ get; }
        Dictionary<string, string> UnitList { get; set; }
        Dictionary<string, string> SubUnitList { get; set; }
        Dictionary<string, string> PositiontList { get; set; }
        Dictionary<string, string> AccessRulesList { get; set; }
        Dictionary<string, string> ReportCreate(string name, DataTable table);
        event EventHandler Message;
        void GetUserList();
        void GetSettings();
        void UserOutput();
        void UserInput(string login, string password);
        void NewUserCreate(Dictionary<string, string> newUser, DataTable table);
    }
}
