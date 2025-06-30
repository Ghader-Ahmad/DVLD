using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.InternationalLicenses
{
    public partial class frmInternationalDriverInfo : Form
    {
        public frmInternationalDriverInfo(int InternationalLicenseID)
        {
            InitializeComponent();

            ctrlInternationalLicenseInfo1.LoadctrlInternationalLicenseInfo(InternationalLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
