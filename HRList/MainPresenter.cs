using System;
using HRList_BL;


namespace HRList
{
    public class MainPresenter
    {
        private readonly IMainForm _mainform;
        private readonly IInputForm _iform;
        private readonly IMessageService _mservice;
        private readonly IHRBuisnessLogic _buisnesslogic;
    
        public MainPresenter (IMainForm mainform, IInputForm iform, IMessageService mservice, IHRBuisnessLogic buisnesslogic)
        {
            _mainform = mainform;
            _iform = iform;
            _mservice = mservice;
            _buisnesslogic = buisnesslogic;
            _mainform.FormShown += _mainform_FormShown;
            _iform.InputButtonClick += _iform_InputButtonClick;
            _mainform.InputBDMenuClick += _mainform_InputBDMenuClick;
            _mainform.OutputBDMenuClick += _mainform_OutputBDMenuClick;
            _mainform.ShowResult += _mainform_ShowResult;
            _mainform.OnAddUser += _mainform_OnAddUser;
            _mainform.SetActiveUser = "Вход не выполнен";
            _buisnesslogic.Message += _buisnesslogic_Message;
        }

        private void _mainform_OnAddUser(object sender, EventArgs e)
        {
            _buisnesslogic.NewUserCreate(_mainform.EditUser, _buisnesslogic.Table);
        }

        private void _mainform_ShowResult(object sender, EventArgs e)
        {
            _mainform.Report = _buisnesslogic.ReportCreate(_buisnesslogic.ActiveUser, _buisnesslogic.Table);
            // _mainform.Result = _buisnesslogic.Worker.Experience.ToString();
        }

        private void _buisnesslogic_Message(object sender, EventArgs e)
        {
            _mservice.ShowMessage("Неверное имя  или пароль");
            return;
        }

        private void _mainform_InputBDMenuClick(object sender, EventArgs e)
        {
            _iform.InputFormShowDialog();
        }
        private void _mainform_OutputBDMenuClick(object sender, EventArgs e)
        {
            _buisnesslogic.UserOutput();
            _mainform.Table= _buisnesslogic.Table;
            _mainform.SetActiveUser = "Вход не выполнен";
            _mainform.SetRulesUser = string.Empty;
        }

        private void _iform_InputButtonClick(object sender, EventArgs e)
        {
            _buisnesslogic.UserInput(_iform.Login, _iform.Password);
            _buisnesslogic.GetUserList();
            _mainform.Table = _buisnesslogic.Table;
            _mainform.ColumnWidthSetup();
            _mainform.SetActiveUser = _buisnesslogic.ActiveUser;
            _mainform.SetRulesUser = _buisnesslogic.RulesUser;
            if (!String.IsNullOrEmpty(_buisnesslogic.ActiveUser)) _iform.InputFormDialogResult();
        }

        private void _mainform_FormShown(object sender, EventArgs e) 
        {
            _buisnesslogic.GetSettings();
            _mainform.UnitList = _buisnesslogic.UnitList;
            _mainform.SubUnitList = _buisnesslogic.SubUnitList;
            _mainform.PositiontList = _buisnesslogic.PositiontList;
            _mainform.AccessRulesList = _buisnesslogic.AccessRulesList;
            _iform.InputFormShowDialog();
        }
    }
}
