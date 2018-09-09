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
    public partial class ResultFormcs : Form
    {
        public ResultFormcs()
        {
            InitializeComponent();
        }

        public string res;
        public string name;

        private void ResultFormcs_Shown(object sender, EventArgs e)
        {
            lblResult.Text = res;
            lblName.Text = name;
        }
    }
}
