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
    public partial class ctrlTakeTest : UserControl
    {
        public ctrlTakeTest()
        {
            InitializeComponent();
        }

        clsTestAppointment testAppointment;
        clsLocalDrivingLicenseApplications localDrivingLicenseApplications;

        private int _ApplicationID = 0;

        public void LoadctrlTakeTest(int TestAppointmentID)
        {
            testAppointment = clsTestAppointment.FindTestAppontmentByID(TestAppointmentID);
            _ApplicationID = clsLocalDrivingLicenseApplications.GetApplicationIDFromLocalApplicationID(testAppointment.LocalDrivingLicenseApplicationID);
            localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalLicenseByApplicationID(_ApplicationID);

            LoadData();
        }

        private void WhatTypeOfTestIsIt(int TestType)
        {
            switch (TestType)
            {
                case 1:
                    {
                        lblTitle.Text = "Scheduled Test";
                        gbTest.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                    case 2:
                    {
                        lblTitle.Text = "Scheduled Test";
                        gbTest.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }

                    case 3:
                    {
                        lblTitle.Text = "Scheduled Test";
                        gbTest.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void LoadData()
        {
            WhatTypeOfTestIsIt(testAppointment.TestTypeID);

            lblDLAppID.Text = testAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = clsLicenseClass.FindLicenseClassByID(localDrivingLicenseApplications.LicenseClassID).ClassName;
            lblName.Text = clsPeople.GetFullName(localDrivingLicenseApplications.ApplicationPersonID);
            lblTrail.Text = clsTestAppointment.GetTheNumberOfTests(testAppointment.LocalDrivingLicenseApplicationID,
                Convert.ToByte(testAppointment.TestTypeID)).ToString();
            lblDate.Text = Convert.ToDateTime(testAppointment.AppointmentDate).ToString("dd/MMM/yyyy");
            lblFees.Text = clsTestType.FindTestType(testAppointment.TestTypeID).TestFees.ToString();
            
        }
    }
}
