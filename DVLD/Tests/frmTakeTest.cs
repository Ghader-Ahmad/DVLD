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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = 0;
        private int _TestTypeID = 0;

        clsTestAppointment testAppointment;
        clsTests test;

        public frmTakeTest(int TestAppointmentID, int TestTypeID)
        {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;

            ctrlTakeTest1.LoadctrlTakeTest(_TestAppointmentID);
        }

        private byte GetResult()
        {
            if (rbPass.Checked)
                return 1;

            if (rbFail.Checked)
                return 0;

            else
                return 0;
        }

        private void AddTest()
        {
            test = new clsTests();

            test.TestAppointmentID = _TestAppointmentID;
            test.TestResult = GetResult();
            test.Notes = (txtNotes.Text == "") ? null: txtNotes.Text;
            test.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
        }

        private bool TestIsLocked()
        {
            if(clsTestAppointment.IsTestAppointmentActiveByTestAppointmentID(_TestAppointmentID))
            {
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                btnSave.Enabled = true; 
                return true;
            }
        }

        private bool CompletedApplicationForRetakeTestIfApplicable()
        {
            testAppointment = clsTestAppointment.FindTestAppontmentByID(_TestAppointmentID);

            int LocalLicenseID = testAppointment.LocalDrivingLicenseApplicationID;
            int ApplicationID = clsLocalDrivingLicenseApplications.GetApplicationIDFromLocalApplicationID(LocalLicenseID);

            int ApplicationPersonID = clsApplications.FindApplicationByID(ApplicationID).ApplicationPersonID;


            return clsApplications.CompletedApplicationForRetakeTestIfApplicable(ApplicationPersonID);
        }

        private void Save()
        {
            AddTest();

            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail result after you save.",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if(test.AddAndSaveTest())
                {
                    CompletedApplicationForRetakeTestIfApplicable();

                    clsTestAppointment.LockedTestAppointment(_TestAppointmentID);
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            if (!TestIsLocked())
                return;
        }
    }
}
