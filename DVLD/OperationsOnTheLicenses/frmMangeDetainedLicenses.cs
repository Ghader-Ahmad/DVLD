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

namespace DVLD.OperationsOnTheLicenses
{
    public partial class frmMangeDetainedLicenses : Form
    {
        public frmMangeDetainedLicenses()
        {
            InitializeComponent();
        }

        DataTable ListDetainedLicensesDataTable;

        private void GetListDetainedLicenses()
        {
            ListDetainedLicensesDataTable = clsDetainedLicenses.GetListDetainedLicenses();
        }

        private void showListDetainedLicenses()
        {
            GetListDetainedLicenses();

            dgvListDetainedLicenses.DataSource = ListDetainedLicensesDataTable;

            dgvListDetainedLicenses.Columns["DetainID"].HeaderText = "D.ID";
            dgvListDetainedLicenses.Columns["LicenseID"].HeaderText = "L.ID";
            dgvListDetainedLicenses.Columns["DetainDate"].HeaderText = "D.Date";
            dgvListDetainedLicenses.Columns["IsReleased"].HeaderText = "Is Released";
            dgvListDetainedLicenses.Columns["FineFees"].HeaderText = "Fine Fees";
            dgvListDetainedLicenses.Columns["ReleaseDate"].HeaderText = "Release Date";
            dgvListDetainedLicenses.Columns["NationalNo"].HeaderText = "N.No";
            dgvListDetainedLicenses.Columns["FullName"].HeaderText = "Full Name";
            dgvListDetainedLicenses.Columns["ReleaseApplicationID"].HeaderText = "Release App.ID";

            lblCountUsers.Text = dgvListDetainedLicenses.RowCount.ToString();
        }

        private int GetPersonID()
        {
            int ApplicationID = clsLicense.FindLicenseByLicenseID((int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value).ApplicationID;

            return clsApplications.FindApplicationByID(ApplicationID).ApplicationPersonID;
        }


        private void ShowFromDataView(DataView dv)
        {
            dgvListDetainedLicenses.DataSource = dv;

            dgvListDetainedLicenses.Columns["DetainID"].HeaderText = "D.ID";
            dgvListDetainedLicenses.Columns["LicenseID"].HeaderText = "L.ID";
            dgvListDetainedLicenses.Columns["DetainDate"].HeaderText = "D.Date";
            dgvListDetainedLicenses.Columns["IsReleased"].HeaderText = "Is Released";
            dgvListDetainedLicenses.Columns["FineFees"].HeaderText = "Fine Fees";
            dgvListDetainedLicenses.Columns["ReleaseDate"].HeaderText = "Release Date";
            dgvListDetainedLicenses.Columns["NationalNo"].HeaderText = "N.No";
            dgvListDetainedLicenses.Columns["FullName"].HeaderText = "Full Name";
            dgvListDetainedLicenses.Columns["ReleaseApplicationID"].HeaderText = "Release App.ID";

            lblCountUsers.Text = dgvListDetainedLicenses.RowCount.ToString();
        }

        private void showListDetainedLicensesFromDataView(DataView dv, string FilterBy)
        {
            dgvListDetainedLicenses.ClearSelection();

            if (FilterBy == "FullName")
            {
                dv.RowFilter = $"{FilterBy} like '{txtFilter.Text}' + '' + '%'";
                ShowFromDataView(dv);
            }
            else
            {
                dv.RowFilter = $"{FilterBy} = '{txtFilter.Text}'";
                ShowFromDataView(dv);
            }
        }

        private void Filter()
        {

            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                showListDetainedLicenses();
                return;
            }

            DataView UsersDataView = ListDetainedLicensesDataTable.DefaultView;

            switch (cbFilter.Text)
            {
                case "Detain ID":
                    {
                        showListDetainedLicensesFromDataView(UsersDataView, "DetainID");
                        break;
                    }
                case "National No":
                    {
                        showListDetainedLicensesFromDataView(UsersDataView, "NationalNo");
                        break;
                    }
                case "Full Name":
                    {
                        showListDetainedLicensesFromDataView(UsersDataView, "FullName");
                        break;
                    }
                case "Release Application ID":
                    {
                        showListDetainedLicensesFromDataView(UsersDataView, "ReleaseApplicationID");
                        break;
                    }
            }
        }

        private void IfFilterEqualNone()
        {
            cbFilter.Text = "";
            txtFilter.Text = "";
            txtFilter.Visible = false;
            cbIsReleased.Visible = false;
            showListDetainedLicenses();
        }

        private void GetReleaseDetainedLicenses()
        {
            DataView dv = ListDetainedLicensesDataTable.DefaultView;

            dv.RowFilter = $"IsReleased = {1}";
            dgvListDetainedLicenses.DataSource = dv;
        }

        private void GetDetainedLicenses()
        {
            DataView dv = ListDetainedLicensesDataTable.DefaultView;

            dv.RowFilter = $"IsReleased = {0}";
            dgvListDetainedLicenses.DataSource = dv;
        }

        private void FilterByIsReleasedOrNot()
        {
            switch (cbIsReleased.Text)
            {
                case "All":
                    {
                        showListDetainedLicenses();
                        break;
                    }

                case "Yes":
                    {
                        GetReleaseDetainedLicenses();
                        break;
                    }

                case "No":
                    {
                        GetDetainedLicenses();
                        break;
                    }
            }
        }

        private void IfFilterEqualIsReleased()
        {
            txtFilter.Visible = false;
            cbIsReleased.Visible = true;
        }






        private void frmMangeDetainedLicenses_Load(object sender, EventArgs e)
        {
            showListDetainedLicenses();
        }

        private void btnDetainedLicense_Click(object sender, EventArgs e)
        {
            frmDetainedLicense frm = new frmDetainedLicense(-1);

            frm.ShowDialog();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleasedDetainedLicense frm = new frmReleasedDetainedLicense(-1);
            frm.ShowDialog();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmShowPersonDetails frm = new frmShowPersonDetails(GetPersonID());
            
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo((int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value);

            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(GetPersonID());
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleasedDetainedLicense frm = new frmReleasedDetainedLicense((int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!clsDetainedLicenses.LicenseIsDetained((int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value))
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;

            else
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
        }

        private void cbFilter_TextChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = true;

            if (cbFilter.Text == "None")
            {
                IfFilterEqualNone();
            }
            if (cbFilter.Text == "Is Released")
            {
                IfFilterEqualIsReleased();
            }
            else
            {
                cbIsReleased.Visible = false;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void cbIsReleased_TextChanged(object sender, EventArgs e)
        {
            FilterByIsReleasedOrNot();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text == "Detain ID" ||  cbFilter.Text == "Release Application ID")
            {
                if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
        }
    }
}
