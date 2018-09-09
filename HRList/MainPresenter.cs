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
            _mainform.ShowResult += _mainform_ShowResult;
            _mainform.SetActiveUser = "Вход не выполнен";
            _buisnesslogic.Message += _buisnesslogic_Message;
        }

        private void _mainform_ShowResult(object sender, EventArgs e)
        {
            _buisnesslogic.SalaryCalculation();
            _mainform.Result = _buisnesslogic.Salary;
         
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

        private void _iform_InputButtonClick(object sender, EventArgs e)
        {
            _buisnesslogic.Login = _iform.Login;
            _buisnesslogic.Password = _iform.Password;
            _buisnesslogic.InputDB();
            _mainform.Set = _buisnesslogic.Set;
            _mainform.SetActiveUser = _buisnesslogic.ActiveUser;
            if (!String.IsNullOrEmpty(_buisnesslogic.ActiveUser)) _iform.InputFormDialogResult();
        }

        private void _mainform_FormShown(object sender, EventArgs e) 
        {
            _iform.InputFormShowDialog();
        }




    }
}
