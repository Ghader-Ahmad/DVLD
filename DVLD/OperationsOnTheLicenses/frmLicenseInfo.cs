﻿using System;
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
    public partial class frmLicenseInfo : Form
    {
        public frmLicenseInfo(int LicesneID)
        {
            InitializeComponent();

            ctrlDriverLicenseInfo1.LoadctrlDriverLicenseInfo(LicesneID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
