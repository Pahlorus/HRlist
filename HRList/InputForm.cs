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
        public event EventHandler InputButtonClick;

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

        public void InputFormDialogResult()
        {
            DialogResult = DialogResult.OK;
            LoginBox.Text = "";
            PassBox.Text = "";
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            InputButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
