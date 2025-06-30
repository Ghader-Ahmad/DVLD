using DataBusinessLayer;
using DVLD.ApplicationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmManageApplicationType : Form
    {
        public frmManageApplicationType()
        {
            InitializeComponent();
        }

        DataTable ApplicationDataTable = new DataTable();

        private void GetApplicationTypes()
        {
            ApplicationDataTable = clsApplicationTypes.GetApplicationTypes();        
        }

        private void ShowApplicationTypes()
        {
            GetApplicationTypes();

            dgvManageApplicationTypes.DataSource = ApplicationDataTable;

            lblCountRecords.Text = dgvManageApplicationTypes.RowCount.ToString();
        }

        private void frmManageApplicationType_Load(object sender, EventArgs e)
        {
            ShowApplicationTypes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)(dgvManageApplicationTypes.CurrentRow.Cells[0].Value));
            frm.ShowDialog();

            ShowApplicationTypes();
        }
    }
}
