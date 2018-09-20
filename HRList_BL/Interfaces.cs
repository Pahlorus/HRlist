﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRList_BL
{
    interface IDb
    {

        void GetData(string query);
        bool AuthorizationDB(string login, string password, out string rules, out int idRules);
        void GetUserData(string login, int idRules);
    }

    public interface IHRBuisnessLogic
    {
        string Login { set; }
        string Password { set; }
        string ActiveUser { get; }
        string RulesUser { get; }
        int IdRules { get; }
        string Salary { get; }
        DataTable Table{ get; }
        event EventHandler Message;
        void GetUserList();
        void GetSettings();
        void UserInput(string login, string password);
        void UserOutput();
        void SalaryCalculation();
        Dictionary<string, string> ReportCreate(string name, DataTable table);
        Dictionary<string, string> UnitList { get; set; }
        Dictionary<string, string> SubUnitList { get; set; }
        Dictionary<string, string> PositiontList { get; set; }
        Dictionary<string, string> AccessRulesList { get; set; }

        void NewUserCreate(Dictionary<string, string> newUser, DataTable table);
    }
}