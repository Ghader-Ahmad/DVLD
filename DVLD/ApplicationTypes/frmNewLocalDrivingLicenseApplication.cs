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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.ApplicationTypes
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;
        clsLocalDrivingLicenseApplications _LocalLicense;
        enum enApplicationStatus { New = 1, Canceled = 2, Completed = 3 };
        enApplicationStatus _ApplicationStatus;
        private int _ApplicationID = 0;

        public frmNewLocalDrivingLicenseApplication(int ApplicationID)
        {
            InitializeComponent();


            _ApplicationID = ApplicationID;
            _Mode = (_ApplicationID == -1) ? enMode.AddNew : enMode.Update;
        }

        private void FillLicenseClassesInComboBox()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dt.Rows)
            {
                cbLicenseClass.Items.Add(row[0].ToString());
            }
        }

        private void LoadData()
        {
            FillLicenseClassesInComboBox();
            cbLicenseClass.SelectedIndex = 2;

            lblApplicationFees.Text = clsApplicationTypes.FindApplication(1).ApplicationFees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = clsClobalSettings.CurrentUser.UserName;

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalLicense = new clsLocalDrivingLicenseApplications();
                return;
            }

            _LocalLicense = clsLocalDrivingLicenseApplications.FindLocalLicenseByApplicationID(_ApplicationID);
            ctrlPersonInformationWithFilter1.LoadctrlPersonInformationWithFilter(_LocalLicense.ApplicationPersonID);
            if (_LocalLicense == null)
                return;

            lblMode.Text = "Update Local Driving License Application";
            this.Text = "Update Local Driving License Application";


            lblDLApplicationID.Text = _LocalLicense.LocalDrivingLicenseApplicationsID.ToString();
            lblApplicationDate.Text = _LocalLicense.ApplicationDate.ToString("dd/MM/yyyy");
        }

        private void GetDataFromUser()
        {
            _LocalLicense.ApplicationPersonID = ctrlPersonInformationWithFilter1.PersonID;
            _LocalLicense.ApplicationDate = DateTime.Now;
            _LocalLicense.LastStatusDate = DateTime.Now;
            _LocalLicense.ApplicationTypeID = 1;
            _LocalLicense.ApplicationStatus = Convert.ToByte(enApplicationStatus.New);
            _LocalLicense.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _LocalLicense.CreatedByUserID = clsClobalSettings.CurrentUser.UserID;
            _LocalLicense.LicenseClassID = clsLicenseClass.FindLicenseClassByName(cbLicenseClass.Text).LicenseClassID;
        }

        private bool ApplicationStatusIsNew()
        {
            int LastApplicationID = clsLocalDrivingLicenseApplications.ApplicationStatusIsNew(_LocalLicense.ApplicationPersonID,
                _LocalLicense.LicenseClassID, Convert.ToByte(enApplicationStatus.New));


            if (LastApplicationID != 0)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active " +
                    $"application for the selected class with id = {LastApplicationID}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return true;
            }
            return false;
        }

        private bool ApplicationStatusIsCompleted()
        {
            int LastApplicationID = clsLocalDrivingLicenseApplications.ApplicationStatusIsCompleted(_LocalLicense.ApplicationPersonID,
                _LocalLicense.LicenseClassID, Convert.ToByte(enApplicationStatus.Completed));


            if (LastApplicationID != 0)
            {
                MessageBox.Show("This person has actually local license from this license class, You can" +
                    " choose another license class if you want that.", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return true;
            }
            return false;
        }

        private void Save()
        {
            GetDataFromUser();

            if (ApplicationStatusIsNew())
                return;

            if (ApplicationStatusIsCompleted())
                return;

            if (_LocalLicense.Save())
            {
                lblDLApplicationID.Text = _LocalLicense.LocalDrivingLicenseApplicationsID.ToString();
                lblMode.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Next()
        {
            if (ctrlPersonInformationWithFilter1.PersonID == -1)
                return;

            tabControl1.SelectedIndex = 1;
        }

  
        private void frmNewLocalDrivingLicenseApplication_Load_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            Save();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            Next();
        }
    }
}
