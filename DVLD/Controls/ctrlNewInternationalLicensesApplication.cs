using DataBusinessLayer;
using DVLD.InternationalLicenses;
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
    public partial class ctrlNewInternationalLicensesApplication : UserControl
    {
        public ctrlNewInternationalLicensesApplication()
        {
            InitializeComponent();
        }

        private clsLicense _license;
        private clsPeople _PersonInfo;

        private clsApplications _applicationInfo = new clsApplications();
        private clsInternationalLicenses _internationalLicenseInfo = new clsInternationalLicenses();

        private void ModifyLicenseStatus()
        {
            if (Convert.ToBoolean(_license.IsActive) == true)
            {
                clsLicense.ModifyLicenseStatus(Convert.ToInt32(txtLicenseID.Text), false);
            }
        }

        private bool LicneseIsActiveAndFromClassThree()
        {
            if(!clsLicense.LicenseIsActiveAndNotExpired(_license.LicenseID))
            {
                ModifyLicenseStatus();

                MessageBox.Show("Cannot issue inernational license because this license is not active", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(!clsLicense.LicenseIsFromClassesThree(Convert.ToInt32(txtLicenseID.Text)))
            {
                MessageBox.Show("Cannot issue inernational license because this license is not of class three", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool InternationalLicenseIsActive(int InternationalLicenseID)
        {
            if(!clsInternationalLicenses.InternationalLicenseIsActive(InternationalLicenseID))
            {
                clsInternationalLicenses.UpdateInternationalLicenseStatus(InternationalLicenseID, false);
                return false;
            }
            return true;
        }

        private bool InternationalLicenseIsExist()
        {
            if (ThisPersonHasAnInternationalLicense())
            {
                LoadData();
                btnIsuue.Enabled = false;
                lblShowLicensesInfo.Enabled = false;

        
                return true;
            }
            return false;
        }

        private bool LicenseIsDetained()
        {
            if(!clsDetainedLicenses.LicenseIsDetained(_license.LicenseID))
            {
                MessageBox.Show("License is Detained.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private bool VerifyTheLicenseToIssueAnInternationalLicense()
        {
            if (!LicneseIsActiveAndFromClassThree())
                return false;

            if (InternationalLicenseIsExist())
                return false;

            if (LicenseIsDetained())
                return false;

            return true;
        }

        private void SubmitApplicationForIssuingInternationalLicense()
        {
            _applicationInfo.ApplicationPersonID = _PersonInfo.PersonID;
            _applicationInfo.ApplicationDate = DateTime.Now;
            _applicationInfo.ApplicationTypeID = (int)clsClobalSettings.enApplicationTypeTitle.NewInternationalLicense;
            _applicationInfo.ApplicationStatus = 1;
            _applicationInfo.LastStatusDate = DateTime.Now;
            _applicationInfo.PaidFees = clsApplicationTypes.GetFeesByApplicationTypeID(_applicationInfo.ApplicationTypeID);
            _applicationInfo.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
        }

        private void LoadInternatinalInfo()
        {
            _internationalLicenseInfo.DriverID = _license.DriverID;
            _internationalLicenseInfo.IssuedUsingLocalLicenseID = _license.LicenseID;
            _internationalLicenseInfo.IssueDate = DateTime.Now;
            _internationalLicenseInfo.ExpirationDate = DateTime.Now.AddYears(1);
            _internationalLicenseInfo.IsActive = 1;
            _internationalLicenseInfo.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
        }

        private void LoadDataInApplicationInfo()
        {
            SubmitApplicationForIssuingInternationalLicense();
            LoadInternatinalInfo();
        }

        private void LoadDriverLicenseInfo()
        {
            ctrlDriverLicenseInfo1.LoadctrlDriverLicenseInfo(_license.LicenseID);
        }

        private void ShowLicensesHistory()
        {
            lblShowLicensesHistory.Enabled = true;
        }

        private void LoadData()
        {
            _PersonInfo = clsPeople.FindByID(clsApplications.FindApplicationByID(_license.ApplicationID).ApplicationPersonID);
            
            LoadDataInApplicationInfo(); 
            LoadDriverLicenseInfo();
            ShowLicensesHistory();

            lblLocalLienseID.Text = _license.LicenseID.ToString();
            btnIsuue.Enabled = true;
        }

        private void ResearchAndVerification()
        {
            _license = clsLicense.FindLicenseByLicenseID(Convert.ToInt32(txtLicenseID.Text));

           if(_license != null)
            {
                if (!VerifyTheLicenseToIssueAnInternationalLicense())
                    return;

                LoadData();
            }
        }

        private void UpdatefrmInternaionalLicenseAfterIssueLicense()
        {
            lblILApplicationID.Text = _applicationInfo.ApplicationID.ToString();
            lblILLicenseID.Text = _internationalLicenseInfo.InternationalLicenseID.ToString();

            lblShowLicensesInfo.Enabled = true;
            btnIsuue.Enabled = false;
        }

        private bool ThisPersonHasAnInternationalLicense()
        {
            int InternationalLicenseID = clsInternationalLicenses.InternationalLicenseIsExistByDriverID(_license.DriverID);

            if (InternationalLicenseID != 0)
            {
                if(!clsInternationalLicenses.InternationalLicenseIsActive(InternationalLicenseID))
                {
                    clsInternationalLicenses.UpdateInternationalLicenseStatus(InternationalLicenseID, false);

                    if (MessageBox.Show("This Person has international license but it is not active, Do you want to issue a new one?"
                         , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { return false; }

                    else
                        return true;
                }

                
                MessageBox.Show($"This Person has an international license with ID :{InternationalLicenseID} .",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private void IssueInternationalLicense()
        { 
            if ((MessageBox.Show("Are you sure you want to issue the license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                _applicationInfo.Save();
                _internationalLicenseInfo.ApplicationID = _applicationInfo.ApplicationID;

                if(_internationalLicenseInfo.AddNewInternationalLicense() && clsApplications.UpdateApplicationStatusToCompletedStatus(_applicationInfo.ApplicationID))
                {
                    UpdatefrmInternaionalLicenseAfterIssueLicense();
                    MessageBox.Show($"International License Issued Successfully wuth ID = {_internationalLicenseInfo.InternationalLicenseID}",
                        "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtLicenseID.Text)) return;

            ResearchAndVerification();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void lblShowLicensesHistory_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(_PersonInfo.PersonID);
            frm.ShowDialog();
        }

        private void ctrlNewInternationalLicensesApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblFees.Text = clsApplicationTypes.GetFeesByApplicationTypeID((int)clsClobalSettings.enApplicationTypeTitle.NewInternationalLicense).ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsClobalSettings.CurrentUser.UserName;
        }

        private void lblShowLicensesInfo_Click(object sender, EventArgs e)
        {
            frmInternationalDriverInfo frm = new frmInternationalDriverInfo(_internationalLicenseInfo.InternationalLicenseID);
            frm.ShowDialog();
        }

        private void btnIsuue_Click(object sender, EventArgs e)
        {
            IssueInternationalLicense();
        }

    }
}
