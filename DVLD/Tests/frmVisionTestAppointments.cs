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

namespace DVLD.Tests
{
    public partial class frmVisionTestAppointments : Form
    {
        private string _TestTypeTitle = "";
        private int _LocalLicenseID = 0;

        enum enMode { AddNewTest = 0, UpdateTest = 1, RetakeTest = 2 };

        private void WhatIsTestType()
        {
            switch(_TestTypeTitle)
            {
                case "Vision Test":
                    {
                        lblTitle.Text = "Vision Test Appointment";
                        pbTestType.Image = Resources.Vision_512;
                        this.Text = "Vision Test Appointment";
                        break;
                    }

                case "Written (Theory) Test":
                    {
                        lblTitle.Text = "Written Test Appointment";
                        pbTestType.Image = Resources.Written_Test_512;
                        this.Text = "Written Test Appointment";
                        break;
                    }

                case "Practical (Street) Test":
                    {
                        lblTitle.Text = "Street Test Appointment";
                        pbTestType.Image = Resources.driving_test_512;
                        this.Text = "Street Test Appointment";

                        break;
                    }
            }
        }

        public frmVisionTestAppointments(int LocalLicenseID, string testType)
        {
            _LocalLicenseID = LocalLicenseID;
            _TestTypeTitle = testType;
           

            InitializeComponent();

            ctrlDrivingLocalApplicationAndAppBasicInfo1.LoadctrlDrivingLocalApplicationAndAppBasicInfo(LocalLicenseID);
        }

        private byte GetTestTypeID(string TestType)
        {
            switch (TestType)
            {
                case "Vision Test":
                    return 1;

                case "Written (Theory) Test":
                    return 2;

                case "Practical (Street) Test":
                    return 3;
            }
            return 1;
        }

        private void GetTestAppointments(int LocalLicenseID, int TestType)
        {
            dgvTestAppointments.DataSource = clsTests.GetTestAppointmentsByID(LocalLicenseID, TestType);

            if (!(dgvTestAppointments.Rows.Count > 0))
                return;

            dgvTestAppointments.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
            dgvTestAppointments.Columns["AppointmentDate"].HeaderText = "Appointment Date";
            dgvTestAppointments.Columns["PaidFees"].HeaderText = "Paid Fees";
            dgvTestAppointments.Columns["IsLocked"].HeaderText = "Is Locked";

            lblCountRecords.Text = dgvTestAppointments.RowCount.ToString();
        }

        private bool IsTestAppointmentActive()
        {
            if(clsTestAppointment.IsTestAppointmentActiveByLocalLicenseID(_LocalLicenseID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private bool DidHeFailHisVisionTest()
        {
            if (!clsTests.HasThisTestBeenPassed(_LocalLicenseID, _TestTypeTitle))
            {

                frmScheduleTest frm = new frmScheduleTest(_LocalLicenseID, enMode.RetakeTest.ToString(), _TestTypeTitle);
                frm.ShowDialog();

                GetTestAppointments(_LocalLicenseID, GetTestTypeID(_TestTypeTitle));

                return true;
            }
            else
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            GetTestAppointments(_LocalLicenseID, GetTestTypeID(_TestTypeTitle));
            WhatIsTestType(); 
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, 
                enMode.UpdateTest.ToString(), _TestTypeTitle);
            frm.ShowDialog();

            GetTestAppointments(_LocalLicenseID, GetTestTypeID(_TestTypeTitle));
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, GetTestTypeID(_TestTypeTitle));
            frm.ShowDialog();

            GetTestAppointments(_LocalLicenseID, GetTestTypeID(_TestTypeTitle));
        }

        private void btnAddNewTestAppointment_Click(object sender, EventArgs e)
        {
            if (IsTestAppointmentActive())
                return;

            if (clsTestAppointment.GetTheNumberOfTests(_LocalLicenseID, GetTestTypeID(_TestTypeTitle)) > 0)
            {
                if (DidHeFailHisVisionTest())
                    return;

                return;
            }

            frmScheduleTest frm = new frmScheduleTest(_LocalLicenseID, enMode.AddNewTest.ToString(), _TestTypeTitle);
            frm.ShowDialog();

            GetTestAppointments(_LocalLicenseID, GetTestTypeID(_TestTypeTitle));
        }
    }
}
