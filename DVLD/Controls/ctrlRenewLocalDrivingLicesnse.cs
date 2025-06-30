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
using System.Web.Hosting;
using System.Windows.Forms;

namespace DVLD.Controls
{
    public partial class ctrlRenewLocalDrivingLicesnse : UserControl
    {
        public ctrlRenewLocalDrivingLicesnse()
        {
            InitializeComponent();
        }

        private clsLicense _license;
        private clsPeople _PersonInfo;

        private clsApplications _applicationInfo = new clsApplications();
        private clsLicense _NewLicenseInfo = new clsLicense();

        private void ModifyLicenseStatus()
        {
            if (Convert.ToBoolean(_license.IsActive) == true)
            {
                clsLicense.ModifyLicenseStatus(Convert.ToInt32(txtLicenseID.Text), false);
            }
        }

        private bool DoesThieDriverHaveAnotherActiceLicenseOfTheSameClass()
        {
            int LicenseID = clsLicense.DoesThieDriverHaveAnotherActiceLicenseOfTheSameClass(_license.DriverID, _license.LicenseClass);
            
            if(LicenseID != 0)
            {
                MessageBox.Show($"This License has been previously renewed and its number is {LicenseID}", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                ctrlDriverLicenseInfo1.LoadctrlDriverLicenseInfo(LicenseID);
                return true;
            }
            return false;
        }

        private bool LicneseIsActive()
        {
            LoadData();

            if (clsLicense.LicenseIsActiveAndNotExpired(_license.LicenseID))
            {
                MessageBox.Show($"Selected License is not yet expiared, it will expire on: {_license.ExpirationDate.ToString("dd/MMM/yyyy")} ",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            ModifyLicenseStatus();

            if (DoesThieDriverHaveAnotherActiceLicenseOfTheSameClass())
                return true;

            return false;
        }
        private bool LicenseIsDetained()
        {
            if (!clsDetainedLicenses.LicenseIsDetained(_license.LicenseID))
            {
                MessageBox.Show("License is Detained cannot renew.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private void ResearchAndVerification() 
        {
            _license = clsLicense.FindLicenseByLicenseID(Convert.ToInt32(txtLicenseID.Text));

            if(_license != null)
            {
                if (LicneseIsActive())
                    return;

                if (LicenseIsDetained())
                    return;

                btnRenew.Enabled = true;
            }
        }

        private void SubmitApplicationForRenewDrivingLicenseService()
        {
            _applicationInfo.ApplicationPersonID = _PersonInfo.PersonID;
            _applicationInfo.ApplicationDate = DateTime.Now;
            _applicationInfo.ApplicationTypeID = (int)clsClobalSettings.enApplicationTypeTitle.RenewDrivingLicenseService;
            _applicationInfo.ApplicationStatus = 1;
            _applicationInfo.LastStatusDate = DateTime.Now;
            _applicationInfo.PaidFees = clsApplicationTypes.GetFeesByApplicationTypeID(_applicationInfo.ApplicationTypeID);
            _applicationInfo.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
        }

        private void LoadNewLicneseInfo()
        {
            _NewLicenseInfo.DriverID = _license.DriverID;
            _NewLicenseInfo.LicenseClass = _license.LicenseClass;
            _NewLicenseInfo.IssueDate = DateTime.Now;
            _NewLicenseInfo.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.FindLicenseClassByID(_license.LicenseClass).DefaultValidityLength);
            _NewLicenseInfo.PaidFees = clsLicenseClass.FindLicenseClassByID(_license.LicenseClass).ClassFees;
            _NewLicenseInfo.IsActive = 1;
            _NewLicenseInfo.IssueReason = (int)clsClobalSettings.enIssueReason.Renew;
            _NewLicenseInfo.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
        }

        private void LoadDataInApplicationNewLicenseInfo()
        {
            SubmitApplicationForRenewDrivingLicenseService();
            LoadNewLicneseInfo();
        }

        private void LoadDriverLicenseInfo()
        {
            ctrlDriverLicenseInfo1.LoadctrlDriverLicenseInfo(_license.LicenseID);
        }

        private void LoadDataOnScreen()
        {
            lblApplicationDate.Text = _applicationInfo.ApplicationDate.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = _NewLicenseInfo.IssueDate.ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = _applicationInfo.PaidFees.ToString();
            lblLicenseFees.Text = _NewLicenseInfo.PaidFees.ToString();
            lblOldLienseID.Text = _license.LicenseID.ToString();
            lblExpirationDate.Text = _NewLicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsClobalSettings.CurrentUser.UserName;
            lblTotalFees.Text = (_applicationInfo.PaidFees + _NewLicenseInfo.PaidFees).ToString();
        }

        private void ShowLicenseHistory()
        {
            lblShowLicensesHistory.Enabled = true; 
        }

        private void LoadData()
        {
            _PersonInfo = clsPeople.FindByID(clsApplications.FindApplicationByID(_license.ApplicationID).ApplicationPersonID);

            ShowLicenseHistory();
            LoadDriverLicenseInfo();
            LoadDataInApplicationNewLicenseInfo();
            LoadDataOnScreen();
        }

        private bool SaveAndCompletedApplication()
        {
            if(_applicationInfo.Save())
            {
                lblRLApplicationID.Text = _applicationInfo.ApplicationID.ToString();

                _NewLicenseInfo.ApplicationID = _applicationInfo.ApplicationID;
                _NewLicenseInfo.Notes = txtNotes.Text;

                return clsApplications.UpdateApplicationStatusToCompletedStatus(_applicationInfo.ApplicationID);
            }
            return false;
        }

        private void RenewLicense()
        {
            if (MessageBox.Show("Are you sure you want to Renew the license? ", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(!SaveAndCompletedApplication())
                {
                    MessageBox.Show("Application not submitted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(_NewLicenseInfo.IssueLicense())
                {
                    ctrlDriverLicenseInfo1.LoadctrlDriverLicenseInfo(_NewLicenseInfo.LicenseID);

                    lblShowNewLicensesInfo.Enabled = true;
                    lblRenewedLicenseID.Text = _NewLicenseInfo.LicenseID.ToString();
                    btnRenew.Enabled = false;
                    MessageBox.Show($"Licensed Renewed Successfully with ID = {_NewLicenseInfo.LicenseID}", "License Issued",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text)) return;

            ResearchAndVerification();
        }

        private void lblShowLicensesHistory_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(_PersonInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            RenewLicense();
        }

        private void lblShowNewLicensesInfo_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_NewLicenseInfo.LicenseID);

            frm.ShowDialog();
        }
    }
}
