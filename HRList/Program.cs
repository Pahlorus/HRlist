using System;
using System.Windows.Forms;
using HRList_BL;

namespace HRList
{
   static class Program
    {

        ///
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainform = new MainForm();
            InputForm iform = new InputForm();
            HRBuisnessLogic buisnesslogic = new HRBuisnessLogic();
            MessageService meservice = new MessageService();
            MainPresenter presenter = new MainPresenter(mainform, iform, meservice, buisnesslogic);
            Application.Run(mainform);
        }
    }
}
