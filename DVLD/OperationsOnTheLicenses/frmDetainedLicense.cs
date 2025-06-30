using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.OperationsOnTheLicenses
{
    public partial class frmDetainedLicense : Form
    {
        public frmDetainedLicense(int LicenseID)
        {
            InitializeComponent();

            ctrlDetainLicense1.LoadctrlDetainLicense(LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
