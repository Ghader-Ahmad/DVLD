using DataBusinessLayer;
using DVLD.OperationsOnTheLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Controls
{
    public partial class ctrlDetainLicense : UserControl
    {
        public ctrlDetainLicense()
        {
            InitializeComponent();
        }

        public void LoadctrlDetainLicense(int LicenseID)
        {
            if(LicenseID != -1)
            {
                txtLicenseID.Text = LicenseID.ToString();
                gbFilter.Enabled = false;
                ResearchAndVerification();
            }
        }

        private clsLicense _license;
        private clsPeople _PersonInfo;

        private clsDetainedLicenses _DetainedLicense = new clsDetainedLicenses();


        private bool VerifyTheLicenseToIssueAnInternationalLicense()
        {
            if (!clsDetainedLicenses.LicenseIsDetained(_license.LicenseID))
            {
                MessageBox.Show("Selected License is already detained, Choose another one.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoadData();
                return true;
            }
            return false;
        }

        private void LoadDriverLicenseInfo()
        {
            ctrlDriverLicenseInfo1.LoadctrlDriverLicenseInfo(_license.LicenseID);
        }

        private void ShowLicenseHistory()
        {
            lblShowLicensesHistory.Enabled = true;
        }

        private void LoadData()
        {
            _PersonInfo = clsPeople.FindByID(clsApplications.FindApplicationByID(_license.ApplicationID).ApplicationPersonID);

            LoadDriverLicenseInfo();
            ShowLicenseHistory();
            lblLicenseID.Text = _license.LicenseID.ToString();
        }

        private void ResearchAndVerification()
        {
            _license = clsLicense.FindLicenseByLicenseID(Convert.ToInt32(txtLicenseID.Text));

            if (_license != null)
            {
                if (VerifyTheLicenseToIssueAnInternationalLicense())
                    return;

                LoadData();
                btnDetain.Enabled = true;
                txtFineFees.Focus();
            }
            else
            {
                MessageBox.Show("License is not found", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool DetainedLicense()
        {
            _DetainedLicense.LicenseID = _license.LicenseID;
            _DetainedLicense.DetainDate = DateTime.Now;
            _DetainedLicense.FineFees = Convert.ToSingle(txtFineFees.Text);
            _DetainedLicense.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
            _DetainedLicense.IsReleased = false;

            return _DetainedLicense.DetainedLicense();
        }

        private void AfterDetainedLicnese()
        {
            lblShowLicensesInfo.Enabled = true;
            btnDetain.Enabled = false;
            gbFilter.Enabled = false;

            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
        }

        private void Detained()
        {
            if(string.IsNullOrEmpty(txtFineFees.Text)) { return; }

            if(MessageBox.Show("Are you sure you want to detain this license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (DetainedLicense())
                {
                    AfterDetainedLicnese();

                    MessageBox.Show($"License Detained Successfully with ID = {_license.LicenseID}", "License Issued",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        private void ctrlDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsClobalSettings.CurrentUser.UserName;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ResearchAndVerification();
        }

        private void lblShowLicensesHistory_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(_PersonInfo.PersonID);

            frm.ShowDialog();
        }

        private void lblShowLicensesInfo_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_license.LicenseID);

            frm.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text))
            {
                e.Cancel = true;
                txtFineFees.Focus();
                errorProvider1.SetError(txtFineFees, "Please enter fine fees");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, "");
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            Detained();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
