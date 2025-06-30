using DataBusinessLayer;
using DVLD.ApplicationTypes;
using DVLD.OperationsOnTheLicenses;
using DVLD.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace DVLD.Applications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        enum enApplicationStatus { New = 1, Canceled = 2, Completed = 3};


        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        DataTable LocalDrivingLicenseApplications = new DataTable();

        private void EnabledOrNotMainMenue(bool Bool)
        {
            editApplicationToolStripMenuItem.Enabled = Bool;
            deleteApplicationToolStripMenuItem.Enabled = Bool;
            cancelApplicationToolStripMenuItem.Enabled = Bool;
            SechduleTestsToolStripMenuItem.Enabled = Bool;
        }

        private void UpdateMainMenueByStatus()
        {
            switch(dgvLocalDrivingLicenseApplication.CurrentRow.Cells[6].Value)
            {
                case "Completed":
                    {
                        EnabledOrNotMainMenue(false);
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                        showLicenseToolStripMenuItem.Enabled = true;

                        break;
                    }

                case "new":
                    {
                        if(Convert.ToByte(dgvLocalDrivingLicenseApplication.CurrentRow.Cells[5].Value) != 3)
                        {
                            EnabledOrNotMainMenue(true);
                            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                            showLicenseToolStripMenuItem.Enabled = false;
                        }

                        break;
                    }

                case "Canceled":
                    {
                        editApplicationToolStripMenuItem.Enabled = false;
                        cancelApplicationToolStripMenuItem.Enabled = false;
                        deleteApplicationToolStripMenuItem.Enabled = true;
                        SechduleTestsToolStripMenuItem.Enabled = false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled= false;
                        showLicenseToolStripMenuItem.Enabled = false;
                        break;
                    }
            }


          
        }

        private void GetLocalDrivingLicenseApplications()
        {
            LocalDrivingLicenseApplications = clsApplications.GetLocalDrivingLicenseApplications();
        }

        private void ShowLocalDrivingLicenseApplications()
        {
            GetLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplication.DataSource = LocalDrivingLicenseApplications;

            lblCountRecords.Text = dgvLocalDrivingLicenseApplication.RowCount.ToString();
        }

        private int GetApplicationID()
        {
            return clsLocalDrivingLicenseApplications.GetApplicationIDFromLocalApplicationID(
                   (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
        }

        private void ShowLocalDrivingLicenseApplicationsFromDataView(DataView dv, string FilterBy)
        {
            dgvLocalDrivingLicenseApplication.ClearSelection();
          
            if(FilterBy == "FullName" || FilterBy == "Status")
            {
                dv.RowFilter = $"{FilterBy} like '{txtFilter.Text}' + '%' + '' or {FilterBy} like '%' + '{txtFilter.Text}' + ''" +
                    $"or {FilterBy} like '%' + '{txtFilter.Text}' + '%' ";
            }
            else
            {
                dv.RowFilter = $"{FilterBy} = '{txtFilter.Text}'";
            }

            dgvLocalDrivingLicenseApplication.DataSource = dv;

            lblCountRecords.Text = dgvLocalDrivingLicenseApplication.RowCount.ToString();
        }

        private void Filter()
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                ShowLocalDrivingLicenseApplications();
                return;
            }

            DataView LocalLicenseDataView = LocalDrivingLicenseApplications.DefaultView;

            switch (cbFilter.Text)
            {
                case "L.D.L.AppID":
                    {
                        ShowLocalDrivingLicenseApplicationsFromDataView(LocalLicenseDataView, "L.D.L.AppID");
                        break;
                    }
                case "National No":
                    {
                        ShowLocalDrivingLicenseApplicationsFromDataView(LocalLicenseDataView, "NationalNo");
                        break;
                    }
                case "Full Name":
                    {
                        ShowLocalDrivingLicenseApplicationsFromDataView(LocalLicenseDataView, "FullName");
                        break;
                    }
                case "Status":
                    {
                        ShowLocalDrivingLicenseApplicationsFromDataView(LocalLicenseDataView, "Status");
                        break;
                    }
            }
        }

        private void WhenClickSechduleTests()
        {
            switch (dgvLocalDrivingLicenseApplication.CurrentRow.Cells[5].Value)
            {
                case 0:
                    {
                        SechduleTestsToolStripMenuItem.Enabled = true;
                        scheduleVisionTestToolStripMenuItem.Enabled = true;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        scheduleStreetTestToolStripMenuItem.Enabled = false;
                        break;
                    }
                case 1:
                    {
                        SechduleTestsToolStripMenuItem.Enabled = true;
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = true;
                        scheduleStreetTestToolStripMenuItem.Enabled = false;
                        break;
                    }
                case 2:
                    {
                        SechduleTestsToolStripMenuItem.Enabled = true;
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        scheduleStreetTestToolStripMenuItem.Enabled = true;
                        break;
                    }
                case 3:
                    {
                        SechduleTestsToolStripMenuItem.Enabled = false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                        break;
                    }
            }

            UpdateMainMenueByStatus();
        }




        private void LoadMainMenue()
        {
            WhenClickSechduleTests();
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            ShowLocalDrivingLicenseApplications();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void cbFilter_TextChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = true;

            if (cbFilter.Text == "None")
            {
                cbFilter.Text = "";
                txtFilter.Text = "";
                txtFilter.Visible = false;
                ShowLocalDrivingLicenseApplications();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "L.D.L.AppID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
        }

        private void btnNewLocalDrivingLicenseApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication(-1);

            frm.ShowDialog();

            ShowLocalDrivingLicenseApplications();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm:",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (clsApplications.EditApplicationStatus(GetApplicationID(), Convert.ToByte(enApplicationStatus.Canceled)))
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    ShowLocalDrivingLicenseApplications();
                }
            }
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new
                frmNewLocalDrivingLicenseApplication(GetApplicationID());

            frm.ShowDialog();
            ShowLocalDrivingLicenseApplications();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            LoadMainMenue();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVisionTestAppointments frm = new frmVisionTestAppointments((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value,
                "Vision Test");
            
            frm.ShowDialog();
            ShowLocalDrivingLicenseApplications();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVisionTestAppointments frm = new frmVisionTestAppointments((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value,
               "Written (Theory) Test");

            frm.ShowDialog();
            ShowLocalDrivingLicenseApplications();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVisionTestAppointments frm = new frmVisionTestAppointments((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value,
            "Practical (Street) Test");

            frm.ShowDialog();
            ShowLocalDrivingLicenseApplications();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseForTheFirstTime frm = new frmIssueDriverLicenseForTheFirstTime((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            ShowLocalDrivingLicenseApplications();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = clsLocalDrivingLicenseApplications.FindLocalLicenseByLocalLicenseID((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value).ApplicationID;
            frmLicenseInfo frm = new frmLicenseInfo(clsLicense.FindLicenseByApplicationID(ApplicationID).LicenseID);

            frm.ShowDialog();

        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsLocalDrivingLicenseApplications.DeleteLocalLicenseApplication((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value))
                {
                    ShowLocalDrivingLicenseApplications();
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Application cannot be delete", "Not Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = 
                new frmLicenseHistory(clsLocalDrivingLicenseApplications.FindLocalLicenseByLocalLicenseID((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value).ApplicationPersonID);

            frm.ShowDialog();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowApplicationDetails frm = new frmShowApplicationDetails((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
        }
    }
}
