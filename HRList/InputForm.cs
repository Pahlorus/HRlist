﻿using System;
using System.Windows;

using System.Windows.Forms;

namespace HRList
{
    public interface IInputForm
    {
        string Login { get; }
        string Password { get; }
        void InputFormDispos();
        void InputFormShowDialog();
        void InputFormDialogResult();
        event EventHandler InputButtonClick;
    }

    public partial class InputForm : Form, IInputForm
    {
        public InputForm()
        {
            InitializeComponent();
        }

        #region Публичные свойства
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



        public event EventHandler InputButtonClick;


    }
}
