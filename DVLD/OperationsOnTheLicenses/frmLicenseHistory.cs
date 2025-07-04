﻿using DataBusinessLayer;
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
    public partial class frmLicenseHistory : Form
    {
        private clsLocalDrivingLicenseApplications _localDrivingLicenseApplications;

        private int _PersonID;

        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;

            ctrlPersonInformationWithFilter1.LoadctrlPersonInformationWithFilter(_PersonID);
            ctrlDriverLicensesHistory1.LoadctrlDriverLicensesHistory(_PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
