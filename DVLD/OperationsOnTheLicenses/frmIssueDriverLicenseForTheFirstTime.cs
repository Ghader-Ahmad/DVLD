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
using TheArtOfDevHtmlRenderer.Adapters;

namespace DVLD.OperationsOnTheLicenses
{
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        public frmIssueDriverLicenseForTheFirstTime(int LocalLicenseID)
        {
            InitializeComponent();

            _localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalLicenseByLocalLicenseID(LocalLicenseID);
            _licenseClass = clsLicenseClass.FindLicenseClassByID(_localDrivingLicenseApplications.LicenseClassID);
            ctrlDrivingLocalApplicationAndAppBasicInfo1.LoadctrlDrivingLocalApplicationAndAppBasicInfo(LocalLicenseID);
        }

        private clsLocalDrivingLicenseApplications _localDrivingLicenseApplications;
        private clsLicenseClass _licenseClass;
        private clsLicense _license;
        private int AddNewDriver()
        {
            clsDriver driver = new clsDriver();

            driver.PersonID = _localDrivingLicenseApplications.ApplicationPersonID;
            driver.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
            driver.CreatedDate = DateTime.Now;

            if (driver.AddNewDriver())
            {
                return driver.DriverID;
            }
            else
                return -1;
        }

        private int GetDriverID()
        {
            int DriverID = clsDriver.PersonIsADriver(_localDrivingLicenseApplications.ApplicationPersonID);
           
            if(DriverID != -1)
            {
                return DriverID;
            }

            else
            {
                return AddNewDriver();
            }
        }

        private bool AddNewLicense()
        {
            _license = new clsLicense();

            _license.ApplicationID = _localDrivingLicenseApplications.ApplicationID;
            _license.DriverID = GetDriverID();
            _license.LicenseClass = _localDrivingLicenseApplications.LicenseClassID;
            _license.IssueDate = DateTime.Now;
            _license.ExpirationDate = _license.IssueDate.AddYears(_licenseClass.DefaultValidityLength);
            _license.Notes = txtNotes.Text;
            _license.PaidFees = _licenseClass.ClassFees;
            _license.IsActive = 1;
            _license.IssueReason = (byte)clsClobalSettings.enIssueReason.FirstTime;
            _license.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;


            return _license.IssueLicense();
        }

        private void IssueLicense()
        {
            if(AddNewLicense() && clsApplications.UpdateApplicationStatusToCompletedStatus(_localDrivingLicenseApplications.ApplicationID))
            {
                MessageBox.Show($"License Issued Successfully with License ID = {_license.LicenseID}.", "Succeeded",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            IssueLicense();
        }
    }
}
