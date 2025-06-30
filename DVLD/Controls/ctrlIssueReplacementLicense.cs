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
    public partial class ctrlIssueReplacementLicense : UserControl
    {
        public ctrlIssueReplacementLicense()
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
       
        private byte GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
            {
                return (int)clsClobalSettings.enIssueReason.ReplacementforDamaged;
            }
            else
                return (int)clsClobalSettings.enIssueReason.ReplacementforLost;
        }

        private float GetApplicationFees()
        {
            if (rbDamagedLicense.Checked)
                return clsApplicationTypes.GetFeesByApplicationTypeTitle((int)clsClobalSettings.enApplicationTypeTitle.ReplacementforaDamagedDrivingLicense);

            else
                return clsApplicationTypes.GetFeesByApplicationTypeTitle((int)clsClobalSettings.enApplicationTypeTitle.ReplacementforaLostDrivingLicense);
        }

        private void SubmitApplicationForIssueReplacementLicense()
        {
            _applicationInfo.ApplicationPersonID = _PersonInfo.PersonID;
            _applicationInfo.ApplicationDate = DateTime.Now;
            _applicationInfo.ApplicationTypeID = GetIssueReason();
            _applicationInfo.ApplicationStatus = 1;
            _applicationInfo.LastStatusDate = DateTime.Now;
            _applicationInfo.PaidFees = clsApplicationTypes.GetFeesByApplicationTypeID(_applicationInfo.ApplicationTypeID);
            _applicationInfo.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
        }

        private void LoadDataInApplicationInfo()
        {
            SubmitApplicationForIssueReplacementLicense();
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

        }

        private bool VerifyTheLicenseIsActive()
        {
            if(!clsLicense.LicenseIsActiveAndNotExpired(_license.LicenseID))
            {
                ModifyLicenseStatus();
                LoadData();

                MessageBox.Show("Selected License is not Active, Choose an active licesne.", "Not Allowed", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void LoadDataForNewLicenseInfo()
        {
            _NewLicenseInfo.DriverID = _license.DriverID;
            _NewLicenseInfo.LicenseClass = _license.LicenseClass;
            _NewLicenseInfo.IssueDate = DateTime.Now;
            _NewLicenseInfo.ExpirationDate = _license.ExpirationDate;
            _NewLicenseInfo.Notes = _license.Notes;
            _NewLicenseInfo.PaidFees = _license.PaidFees;
            _NewLicenseInfo.IsActive = 1;
            _NewLicenseInfo.IssueReason = GetIssueReason();
            _NewLicenseInfo.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
        }

        private void ResearchAndVerification()
        {
            _license = clsLicense.FindLicenseByLicenseID(Convert.ToInt32(txtLicenseID.Text));
           
            if (_license != null)
            {
                if (!VerifyTheLicenseIsActive())
                    return;

                LoadData();
                btnIssueReplacement.Enabled = true;
            }
        }

        private void BeforeClickIssueReplacement()
        {
            if(_applicationInfo.Save())
            {
                _NewLicenseInfo.ApplicationID = _applicationInfo.ApplicationID;
                clsApplications.UpdateApplicationStatusToCompletedStatus(_applicationInfo.ApplicationID);

                clsLicense.ModifyLicenseStatus(_license.LicenseID, false);
            }
        }

        private void AfterIssueNewLicense()
        {
            lblLRApplicationID.Text = _applicationInfo.ApplicationID.ToString();
            lblReplacedLicenseID.Text = _NewLicenseInfo.LicenseID.ToString();
            lblOldLienseID.Text = _license.LicenseID.ToString();
            lblShowNewLicensesInfo.Enabled = true;

            gbFilter.Enabled = false;
            gbReplacenemtFor.Enabled = false;
            btnIssueReplacement.Enabled = false;
        }

        private void IssueReplacement()
        {
            LoadDataForNewLicenseInfo();

            if (MessageBox.Show("Are you sure you want to issue a replacement for the license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BeforeClickIssueReplacement();
                if (_NewLicenseInfo.IssueLicense())
                {
                    AfterIssueNewLicense();

                    MessageBox.Show($"Licensed Replaced Successfully with ID = {_NewLicenseInfo.LicenseID}",
                        "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
                
        }



        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblMode.Text = "Replacement For Damaged License";
            lblApplicationFees.Text = clsApplicationTypes.GetFeesByApplicationTypeID((int)clsClobalSettings.enApplicationTypeTitle.ReplacementforaDamagedDrivingLicense).ToString();
            this.ParentForm.Text = "Replacement For Damaged License";
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblMode.Text = "Replacement For Lost License";
            lblApplicationFees.Text = clsApplicationTypes.GetFeesByApplicationTypeID((int)clsClobalSettings.enApplicationTypeTitle.ReplacementforaLostDrivingLicense).ToString();
            this.ParentForm.Text = "Replacement For Lost License";
        }

        private void ctrlIssueReplacementLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = GetApplicationFees().ToString();
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

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            IssueReplacement();
        }

        private void lblShowNewLicensesInfo_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_NewLicenseInfo.LicenseID);

            frm.ShowDialog();
        }
    }
}
