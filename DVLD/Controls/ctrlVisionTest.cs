using DataBusinessLayer;
using DVLD.Properties;
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
    public partial class ctrlVisionTest : UserControl
    {
        enum enMode { AddNewTest = 0, UpdateTest = 1, RetakeTest = 2 };
        enMode _Mode;

        enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        enTestType _TestTypeTitle;

        private int _LocalLicenseID = 0;
        private int _TestAppointmentID = 0;
        private int _ApplicationID = 0;

        clsApplications _CurrentApplication;

        public ctrlVisionTest()
        {
            InitializeComponent();
        }

        clsLocalDrivingLicenseApplications localDrivingLicenseApplications;
        clsTestAppointment testAppointment;
        clsApplications retakeTestApplications;

        private enMode GetMode(string Mode)
        {
            switch (Mode)
            {
                case "AddNewTest":
                    return enMode.AddNewTest;

                case "UpdateTest":
                    return enMode.UpdateTest;

                case "RetakeTest":
                    return enMode.RetakeTest;
            }
            return enMode.AddNewTest;
        }

        private enTestType GetTestType(string TestType)
        {
            switch (TestType)
            {
                case "Vision Test":
                    return enTestType.VisionTest;

                case "Written (Theory) Test":
                    return enTestType.WrittenTest;

                case "Practical (Street) Test":
                    return enTestType.StreetTest;
            }
            return enTestType.VisionTest;
        }

        private void WhatIsTestType()
        {
            switch (_TestTypeTitle)
            {
                case enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointment";
                        pbTestType.Image = Resources.Vision_512;
                        gbTestType.Text = "Vision Test Appointment";
                        break;
                    }

                case enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointment";
                        pbTestType.Image = Resources.Written_Test_512;
                        gbTestType.Text = "Written Test Appointment";
                        break;
                    }

                case enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointment";
                        pbTestType.Image = Resources.driving_test_512;
                        gbTestType.Text = "Street Test Appointment";

                        break;
                    }
            }
        }

        public void LoadctrlVisionTest(int LocalLicenseID, string Mode, string TestType)
        {
            _LocalLicenseID = LocalLicenseID;

            _Mode = GetMode(Mode);
            _TestTypeTitle = GetTestType(TestType);

            WhatIsTestType();

            btnSave.Enabled = true;

            if (_Mode == enMode.UpdateTest)
            {
                testAppointment = clsTestAppointment.FindTestAppontmentByID(_LocalLicenseID);
                _LocalLicenseID = testAppointment.LocalDrivingLicenseApplicationID;
                _ApplicationID = clsLocalDrivingLicenseApplications.GetApplicationIDFromLocalApplicationID(_LocalLicenseID);
                _CurrentApplication = clsApplications.FindApplicationByID(_ApplicationID);
                LoadDataInUpdateMode();
                return;
            }

            LoadData();
        }

        private bool SaveDate()
        {
            testAppointment.TestTypeID = (int)_TestTypeTitle;
            testAppointment.LocalDrivingLicenseApplicationID = _LocalLicenseID;
            testAppointment.AppointmentDate = dtpDate.Value;
            testAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            testAppointment.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
            testAppointment.IsLocked = 0;

            if (testAppointment.Save())
            {
                MessageBox.Show("Date Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        private void LoadDataInAddNewMode()
        {
            lblTitle.Text = "Create Schedual Test";
            lblSubTitle.Visible = false;
            testAppointment = new clsTestAppointment();
        }

        private void LoadDataInUpdateMode()
        {
            lblTitle.Text = "Update Schedual Test";
            if (testAppointment != null)
            {
                lblSubTitle.Visible = false;
                dtpDate.Enabled = true;

                localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalLicenseByApplicationID(_ApplicationID);

                dtpDate.Value = testAppointment.AppointmentDate;
                lblDLAppID.Text = testAppointment.LocalDrivingLicenseApplicationID.ToString();
                lblDClass.Text = clsLicenseClass.FindLicenseClassByID(localDrivingLicenseApplications.LicenseClassID).ClassName;
                lblName.Text = clsPeople.GetFullName(localDrivingLicenseApplications.ApplicationPersonID);
                lblTrail.Text = clsTestAppointment.GetTheNumberOfTests(_LocalLicenseID, ((byte)_TestTypeTitle)).ToString();
                lblFees.Text = clsTestType.FindTestType((int)_TestTypeTitle).TestFees.ToString();
                lblTotalFees.Text = lblFees.Text;
                gbRetakeTestInfo.Enabled = false;
            }

            if (clsTestAppointment.TestAppointmentIsLocked(testAppointment.TestAppointmentID))
            {
                lblTitle.Text = "Schedule Retake Test";

                lblSubTitle.Visible = true;
                lblSubTitle.Text = "Person already sat for the test, appontment loaked.";

                dtpDate.Enabled = false;
                gbRetakeTestInfo.Enabled = true;

                btnSave.Enabled = false;

                return;
            }

            if (testAppointment.AppointmentDate.Day < DateTime.Now.Day)
            {
                testAppointment.IsLocked = 1;
                dtpDate.Enabled = false;

                testAppointment.Save();
            }

            retakeTestApplications = clsApplications.FindApplicationBy(_CurrentApplication.ApplicationPersonID, (int)clsClobalSettings.enApplicationTypeTitle.RetakeTest);
            
            if(retakeTestApplications != null && retakeTestApplications.ApplicationStatus == 1)
            {
                UpdateUserInterfaceWhenTheModeIsRetakeTest();
            }
        }

        private void RetakeTestApplication()
        {
            retakeTestApplications = new clsApplications();

            retakeTestApplications.ApplicationPersonID = clsApplications.FindApplicationByID(_ApplicationID).ApplicationPersonID;
            retakeTestApplications.ApplicationDate = DateTime.Now;
            retakeTestApplications.ApplicationTypeID = (int)clsClobalSettings.enApplicationTypeTitle.RetakeTest;
            retakeTestApplications.ApplicationStatus = 1;
            retakeTestApplications.LastStatusDate = DateTime.Now;
            retakeTestApplications.PaidFees = clsApplicationTypes.GetFeesByApplicationTypeTitle(retakeTestApplications.ApplicationTypeID);
            retakeTestApplications.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;

            retakeTestApplications.Save();

        }

        private void UpdateUserInterfaceWhenTheModeIsRetakeTest()
        {
            lblTitle.Text = "Schedule Retake Test";
            lblSubTitle.Visible = false;
            gbRetakeTestInfo.Enabled = true;

            lblRAppFees.Text = clsApplicationTypes.GetFeesByApplicationTypeTitle((int)clsClobalSettings.enApplicationTypeTitle.RetakeTest).ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRAppFees.Text)).ToString();

            if (retakeTestApplications != null)
            {
                lblRTestAppID.Text = retakeTestApplications.ApplicationID.ToString();
            }
        }

        private void LoadDataInRetakeTestMode()
        {
            UpdateUserInterfaceWhenTheModeIsRetakeTest();

            testAppointment = new clsTestAppointment();
        }

        private void LoadData()
        {
            _ApplicationID = clsLocalDrivingLicenseApplications.GetApplicationIDFromLocalApplicationID(_LocalLicenseID);
            _CurrentApplication = clsApplications.FindApplicationByID(_ApplicationID);

            localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalLicenseByApplicationID(_ApplicationID);

            if (localDrivingLicenseApplications != null)
            {
                lblDLAppID.Text = localDrivingLicenseApplications.LocalDrivingLicenseApplicationsID.ToString();
                lblDClass.Text = clsLicenseClass.FindLicenseClassByID(localDrivingLicenseApplications.LicenseClassID).ClassName;
                lblName.Text = clsPeople.GetFullName(localDrivingLicenseApplications.ApplicationPersonID);
                lblTrail.Text = clsTestAppointment.GetTheNumberOfTests(_LocalLicenseID, ((byte)_TestTypeTitle)).ToString();
                lblFees.Text = clsTestType.FindTestType((int)_TestTypeTitle).TestFees.ToString();
                lblTotalFees.Text = lblFees.Text;
                gbRetakeTestInfo.Enabled = false;
            }

            if (_Mode == enMode.AddNewTest)
                LoadDataInAddNewMode();

            if (_Mode == enMode.RetakeTest)
                LoadDataInRetakeTestMode();
        }

        private void Save()
        {
            SaveDate();



            if (_Mode == enMode.RetakeTest)
            {
                RetakeTestApplication();
                lblRTestAppID.Text = retakeTestApplications.ApplicationID.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

      
    }
}
