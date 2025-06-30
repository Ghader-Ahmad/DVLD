using DataBusinessLayer;
using DVLD.People;
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
    public partial class ctrlDrivingLocalApplicationAndAppBasicInfo : UserControl
    {
        clsLocalDrivingLicenseApplications localDrivingLicenseApplications;
        private int _ApplicationID;

        enum enStatus { New = 1, Canceled = 2, Completed = 3 };
        enStatus _Status;


        public ctrlDrivingLocalApplicationAndAppBasicInfo()
        {
            InitializeComponent();
        }

        private bool IsPersonHasLicense()
        {
            return clsLicense.IsPersonHasLicense(_ApplicationID);
        }

        private void LoadData()
        {
            lblDVLAppID.Text = localDrivingLicenseApplications.LocalDrivingLicenseApplicationsID.ToString();
            lblAppliedForLicense.Text = clsLicenseClass.FindLicenseClassByID(localDrivingLicenseApplications.LicenseClassID).ClassName;
            lblPassedTests.Text =
                clsTestType.GetCountPassedTests(localDrivingLicenseApplications.LocalDrivingLicenseApplicationsID).ToString() + "/3";

            lblID.Text = localDrivingLicenseApplications.ApplicationID.ToString();
            lblStatus.Text = ((enStatus)localDrivingLicenseApplications.ApplicationStatus).ToString();
            lblFees.Text = localDrivingLicenseApplications.PaidFees.ToString();
            lblType.Text = clsApplicationTypes.FindApplication(localDrivingLicenseApplications.ApplicationTypeID).ApplicationTitle;
            lblApplicant.Text = clsPeople.GetFullName(localDrivingLicenseApplications.ApplicationPersonID);
            lblDate.Text = localDrivingLicenseApplications.ApplicationDate.ToString("dd/MMM/yyyy");
            lblStatusDate.Text = localDrivingLicenseApplications.LastStatusDate.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsUser.FindUserByUserID(localDrivingLicenseApplications.CreatedByUserID).UserName;

            lblShowLicenseInfo.Enabled = IsPersonHasLicense();
        }

        public void LoadctrlDrivingLocalApplicationAndAppBasicInfo(int LocalLicenseID)
        {
            _ApplicationID = clsLocalDrivingLicenseApplications.GetApplicationIDFromLocalApplicationID(LocalLicenseID);

            localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalLicenseByApplicationID(_ApplicationID);

            LoadData();
        }

        private void lblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonDetails frm = new frmShowPersonDetails(localDrivingLicenseApplications.ApplicationPersonID);
            frm.ShowDialog();
        
        }

     
    }
}
