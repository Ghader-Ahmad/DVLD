using DataBusinessLayer;
using DVLD.InternationalLicenses;
using DVLD.OperationsOnTheLicenses;
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
    public partial class ctrlDriverLicensesHistory : UserControl
    {
        public ctrlDriverLicensesHistory()
        {
            InitializeComponent();
        }

        private int _PersonID;
        private int _DriverID;

        DataTable LicenseDataTable;
        DataTable InternationalLicensesDataTable;

        public void LoadctrlDriverLicensesHistory(int PersonID)
        {
            _PersonID = PersonID;   
            _DriverID = clsDriver.GetDriverID(_PersonID);

            ShowAllLocalLicenses();
            ShowAllInternationalLicenses();
        }

        private void GetAllDriverLicensesByDriverID()
        {
            LicenseDataTable = clsLicense.GetAllDriverLicensesByDriverID(_DriverID);
        }

        private void ShowAllLocalLicenses()
        {
            GetAllDriverLicensesByDriverID();

            if (LicenseDataTable.Rows.Count == 0)
                return;

            dgvLocalLicensesHistory.DataSource = LicenseDataTable;

            dgvLocalLicensesHistory.Columns["LicenseID"].HeaderText = "Lic.ID";
            dgvLocalLicensesHistory.Columns["ApplicationID"].HeaderText = "App.ID";
            dgvLocalLicensesHistory.Columns["ClassName"].HeaderText = "Class Name";
            dgvLocalLicensesHistory.Columns["IssueDate"].HeaderText = "Issue Date";
            dgvLocalLicensesHistory.Columns["ExpirationDate"].HeaderText = "Expiration Date";
            dgvLocalLicensesHistory.Columns["IsActive"].HeaderText = "Is Active";

            lblCountRecordsLocalHistory.Text = dgvLocalLicensesHistory.RowCount.ToString();
        }

        private void GetAllDriverInternationalLicensesByDriverID()
        {
            InternationalLicensesDataTable = clsInternationalLicenses.GetAllDriverInternationalLicensesByDriverID(_DriverID);
        }

        private void ShowAllInternationalLicenses()
        {
            GetAllDriverInternationalLicensesByDriverID();

            if (InternationalLicensesDataTable.Rows.Count == 0)
                return;

            dgvInternationalLicensesHistory.DataSource = InternationalLicensesDataTable;

            dgvInternationalLicensesHistory.Columns["InternationalLicenseID"].HeaderText = "Int.License ID";
            dgvInternationalLicensesHistory.Columns["ApplicationID"].HeaderText = "Application ID";
            dgvInternationalLicensesHistory.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";
            dgvInternationalLicensesHistory.Columns["IssueDate"].HeaderText = "Issue Date";
            dgvInternationalLicensesHistory.Columns["ExpirationDate"].HeaderText = "Expiration Date";
            dgvInternationalLicensesHistory.Columns["IsActive"].HeaderText = "Is Active";

            lblCountRecordsInternationalHistory.Text = dgvInternationalLicensesHistory.RowCount.ToString();
        }

        private void showLocalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
        }

        private void showInternationalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalDriverInfo frm = new frmInternationalDriverInfo((int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
