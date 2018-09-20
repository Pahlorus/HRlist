using System;
using System.Windows;
using System.Windows.Forms;

namespace HRList
{


    public partial class InputForm : Form, IInputForm
    {
        public InputForm()
        {
            InitializeComponent();
        }

        #region Properties
        public string Login
        {
            get { return LoginBox.Text; }
        }

        public string Password
        {
            get { return PassBox.Text; }
        }

        public void InputFormShowDialog()
        {
            ShowDialog();
        }

        public void InputFormDispos()
        {
            Dispose();
        }
        #endregion

        #region Methods
        public void InputFormDialogResult() //сброс формы ввода
        {
            DialogResult = DialogResult.OK;
            LoginBox.Text = "";
            PassBox.Text = "";
        }

        private void InputButton_Click(object sender, EventArgs e)//
        { 
            InputButtonClick?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Events
        public event EventHandler InputButtonClick;
        #endregion

    }
}
