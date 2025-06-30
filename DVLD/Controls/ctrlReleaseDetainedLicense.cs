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
    public partial class ctrlReleaseDetainedLicense : UserControl
    {
        public ctrlReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public void LoadctrlReleaseDetainedLicense(int LicneseID)
        {
          if(LicneseID != -1)
            {
                txtLicenseID.Text = LicneseID.ToString();
                gbFilter.Enabled = false;

                ResearchAndVerification();
            }
        }

        private clsLicense _license;
        private clsPeople _PersonInfo;
        private clsDetainedLicenses _DetainedLicenes;

        private clsApplications _ApplicationInfo = new clsApplications();


        private bool LicenseIsDetained()
        {
            if(clsDetainedLicenses.LicenseIsDetained(_license.LicenseID))
            {
                LoadData();
                MessageBox.Show("Selected License is not detained, Choose another one.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void ShowLicenseHistory()
        {
            lblShowLicensesHistory.Enabled = true;
        }

        private void LoadDriverLicenseInfo()
        {
            ctrlDriverLicenseInfo1.LoadctrlDriverLicenseInfo(_license.LicenseID);
        }

        private void LoadDataInApplicationForReleaseDetainedLicense()
        {
            _ApplicationInfo.ApplicationPersonID = _PersonInfo.PersonID;
            _ApplicationInfo.ApplicationDate = DateTime.Now;
            _ApplicationInfo.ApplicationTypeID = (int)clsClobalSettings.enApplicationTypeTitle.ReleaseDetainedDrivingLicsense;
            _ApplicationInfo.ApplicationStatus = 1;
            _ApplicationInfo.LastStatusDate = DateTime.Now;
            _ApplicationInfo.PaidFees = clsApplicationTypes.GetFeesByApplicationTypeID(_ApplicationInfo.ApplicationTypeID);
            _ApplicationInfo.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
        }

        private void LoadData()
        {
            _PersonInfo = clsPeople.FindByID(clsApplications.FindApplicationByID(_license.ApplicationID).ApplicationPersonID);

            ShowLicenseHistory();
            LoadDriverLicenseInfo();
            LoadDataInApplicationForReleaseDetainedLicense();
            lblLicenseID.Text = _license.LicenseID.ToString();
        }

        private void LoadDetainInfo()
        {
            lblDetainID.Text = _DetainedLicenes.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicenes.DetainDate.ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = _ApplicationInfo.PaidFees.ToString();
            lblCreatedBy.Text = clsUser.FindUserByUserID(_DetainedLicenes.CreatedByUserID).UserName;
            lblFineFees.Text = _DetainedLicenes.FineFees.ToString();
            lblTotalFees.Text = (_ApplicationInfo.PaidFees + _DetainedLicenes.FineFees).ToString();
        }

        private void LoadDateAfterVerification()
        {
            LoadData();

            _DetainedLicenes = clsDetainedLicenses.FindDetainedLicense(_license.LicenseID);

            LoadDetainInfo();
        }

        private void ResearchAndVerification()
        {
            _license = clsLicense.FindLicenseByLicenseID(Convert.ToInt32(txtLicenseID.Text));

            if (_license != null)
            {
                if (!LicenseIsDetained())
                    return;

                LoadDateAfterVerification();
                btnRelease.Enabled = true;
            }
            else
            {
                MessageBox.Show("License is not found", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SaveApplication()
        {
            _ApplicationInfo.Save();

            _DetainedLicenes.IsReleased = true;
            _DetainedLicenes.ReleaseDate = DateTime.Now;
            _DetainedLicenes.ReleasedByUserID = clsClobalSettings.CurrentUser.UserID;
            _DetainedLicenes.ReleaseApplicationID = _ApplicationInfo.ApplicationID;
        }

        private void AfterRelease()
        {
            clsApplications.UpdateApplicationStatusToCompletedStatus(_ApplicationInfo.ApplicationID);
            lblApplicationID.Text = _ApplicationInfo.ApplicationID.ToString();
            btnRelease.Enabled = false;
            gbFilter.Enabled = false;
            lblShowLicensesInfo.Enabled = true;
        }

        private void Release()
        {
            SaveApplication();

            if (MessageBox.Show("Are you sure you want release this detained licesne?", "Confirm", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (_DetainedLicenes.ReleasedLicense())
                {
                    AfterRelease();

                    MessageBox.Show("Detained license released successfully.", "Detained License Released",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                clsApplications.EditApplicationStatus(_ApplicationInfo.ApplicationID, 2);
            }
            
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            Release();
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
    }
}
