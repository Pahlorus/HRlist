using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRList
{
    public partial class UserEditForm : Form
    {
        public UserEditForm()
        {
            InitializeComponent();
        }

        public string UserName
        {
            get { return textBoxName.Text; }
        }

        public bool IsSupervisor
        {
            get { return checkSupervisorBox.Checked; }
        }

        public ComboBox Position
        {
            get { return userPositionCBox; }
            set { userPositionCBox = value; }
        }

        public ComboBox Unit
        {
            get { return unitCBox; }
            set { unitCBox = value; }
        }

        public ComboBox SubUnit
        {
            get { return subUnitCBox; }
            set { subUnitCBox = value; }
        }
/*
        public string BaseRate
        {
            get { return textBoxBaseRate.Text; }
        }

        public string BonusExperience
        {
            get { return textBonusExperience.Text; }
        }

        public string BonusSubbordinates
        {
            get { return textBoxBonusSubbordinates.Text; }
        }
      */ 
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WriteUserButton_Click(object sender, EventArgs e)
        {
            OnAddUser?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnAddUser;


        private void UserEditForm_Shown(object sender, EventArgs e)
        {

        }
    }
}
