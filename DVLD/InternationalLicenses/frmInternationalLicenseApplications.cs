using DataBusinessLayer;
using DVLD.OperationsOnTheLicenses;
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

namespace DVLD.InternationalLicenses
{
    public partial class frmInternationalLicenseApplications : Form
    {
        public frmInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        DataTable InternationalLicensesDataTable = new DataTable();

        private void GetAllInternationalLicenses()
        {
            InternationalLicensesDataTable = clsInternationalLicenses.GetAllInternationalLicenses();
        }

        private void ShowInternationalLicenses()
        {
            GetAllInternationalLicenses();

            dgvInternationalLicenseApplication.DataSource = InternationalLicensesDataTable;

            dgvInternationalLicenseApplication.Columns["InternationalLicenseID"].HeaderText = "Int.License ID";
            dgvInternationalLicenseApplication.Columns["ApplicationID"].HeaderText = "Application ID";
            dgvInternationalLicenseApplication.Columns["DriverID"].HeaderText = "Driver ID";
            dgvInternationalLicenseApplication.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";
            dgvInternationalLicenseApplication.Columns["IssueDate"].HeaderText = "Issue Date";
            dgvInternationalLicenseApplication.Columns["ExpirationDate"].HeaderText = "Expiration Date";
            dgvInternationalLicenseApplication.Columns["IsActive"].HeaderText = "Is Active";

            lblCountRecords.Text = dgvInternationalLicenseApplication.RowCount.ToString();
        }

        private void ShowFromDataView(DataView dv)
        {
            dgvInternationalLicenseApplication.DataSource = dv;

            dgvInternationalLicenseApplication.Columns["InternationalLicenseID"].HeaderText = "Int.License ID";
            dgvInternationalLicenseApplication.Columns["ApplicationID"].HeaderText = "Application ID";
            dgvInternationalLicenseApplication.Columns["DriverID"].HeaderText = "Driver ID";
            dgvInternationalLicenseApplication.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";
            dgvInternationalLicenseApplication.Columns["IssueDate"].HeaderText = "Issue Date";
            dgvInternationalLicenseApplication.Columns["ExpirationDate"].HeaderText = "Expiration Date";
            dgvInternationalLicenseApplication.Columns["IsActive"].HeaderText = "Is Active";

            lblCountRecords.Text = dgvInternationalLicenseApplication.RowCount.ToString();
        }

        private void ShowInternationalLicensesFromDataView(DataView dv, string FilterBy)
        {
            dgvInternationalLicenseApplication.ClearSelection();

            dv.RowFilter = $"{FilterBy} = '{txtFilter.Text}'";
            ShowFromDataView(dv);
        }

        private void Filter()
        {

            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                ShowInternationalLicenses();
                return;
            }

            DataView UsersDataView = InternationalLicensesDataTable.DefaultView;

            switch (cbFilter.Text)
            {
                case "International License ID":
                    {
                        ShowInternationalLicensesFromDataView(UsersDataView, "InternationalLicenseID");
                        break;
                    }
                case "Application ID":
                    {
                        ShowInternationalLicensesFromDataView(UsersDataView, "ApplicationID");
                        break;
                    }
                case "Driver ID":
                    {
                        ShowInternationalLicensesFromDataView(UsersDataView, "DriverID");
                        break;
                    }
                case "Local License ID":
                    {
                        ShowInternationalLicensesFromDataView(UsersDataView, "IssuedUsingLocalLicenseID");
                        break;
                    }
            }
        }

        private void IfFilterEqualNone()
        {
            cbFilter.Text = "";
            txtFilter.Text = "";
            txtFilter.Visible = false;
            cbIsActive.Visible = false;
            ShowInternationalLicenses();
        }

        private void GetActiveInternationalLicenses()
        {
            DataView dv = InternationalLicensesDataTable.DefaultView;

            dv.RowFilter = $"IsActive = {1}";
            dgvInternationalLicenseApplication.DataSource = dv;
        }

        private void GetNotActiveInternationlLicenses()
        {
            DataView dv = InternationalLicensesDataTable.DefaultView;

            dv.RowFilter = $"IsActive = {0}";
            dgvInternationalLicenseApplication.DataSource = dv;
        }

        private void FilterByIsActiveOrNotInternationalLicenses()
        {
            switch (cbIsActive.Text)
            {
                case "All":
                    {
                        ShowInternationalLicenses();
                        break;
                    }

                case "Yes":
                    {
                        GetActiveInternationalLicenses();
                        break;
                    }

                case "No":
                    {
                        GetNotActiveInternationlLicenses();
                        break;
                    }
            }
        }

        private void IfFilterEqualIsActive()
        {
            txtFilter.Visible = false;
            cbIsActive.Visible = true;
        }



        private void frmInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            ShowInternationalLicenses();
        }

        private void btnNewInternationalLicenseApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicensesApplication frm = new frmNewInternationalLicensesApplication();
            frm.ShowDialog();

            ShowInternationalLicenses();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonDetails frm =
                new frmShowPersonDetails((clsApplications.FindApplicationByID((int)dgvInternationalLicenseApplication.CurrentRow.Cells[1].Value).ApplicationPersonID));
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalDriverInfo frm = new frmInternationalDriverInfo((int)dgvInternationalLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();   
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm =
            new frmLicenseHistory(clsApplications.FindApplicationByID((int)dgvInternationalLicenseApplication.CurrentRow.Cells[1].Value).ApplicationPersonID);

            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilter_TextChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = true;

            if (cbFilter.Text == "None")
            {
                IfFilterEqualNone();
            }
            if (cbFilter.Text == "Is Active")
            {
                IfFilterEqualIsActive();
            }
            else
            {
                cbIsActive.Visible = false;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void cbIsActive_TextChanged(object sender, EventArgs e)
        {
            FilterByIsActiveOrNotInternationalLicenses();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
