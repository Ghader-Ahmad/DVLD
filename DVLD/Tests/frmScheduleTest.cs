using DataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmScheduleTest : Form
    {
        public frmScheduleTest(int LocalLicenseID, string Mode, string TestType)
        {
            InitializeComponent();


            ctrlVisionTest1.LoadctrlVisionTest(LocalLicenseID, Mode, TestType);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
