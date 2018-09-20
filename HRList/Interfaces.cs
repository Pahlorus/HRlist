using System;
using System.Data;
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
        string RequestedUser { get; }
        string SetRulesUser { set; }
        string Result { set; }
        DataTable Table { get; set; }
        Dictionary<string, string> Report { set; }
        Dictionary<string, string> EditUser { get; set; }
        Dictionary<string, string> UnitList { get; set; }
        Dictionary<string, string> SubUnitList { get; set; }
        Dictionary<string, string> PositiontList { get; set; }
        Dictionary<string, string> AccessRulesList { get; set; }
        void ColumnWidthSetup();
    }

    public interface IInputForm
    {
        string Login { get; }
        string Password { get; }
        void InputFormDispos();
        void InputFormShowDialog();
        void InputFormDialogResult();
        event EventHandler InputButtonClick;
    }

    public interface IMessageService
    {
        void ShowMessage(string MessageText);
        void ShowError(string ErrorText);
    }
}
