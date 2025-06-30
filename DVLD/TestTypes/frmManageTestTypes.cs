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

namespace DVLD.TestTypes
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        DataTable AllTestTypes = new DataTable();
        private void GetAllTestTypes()
        {
            AllTestTypes = clsTestType.GetAllTestTypes();
        }

        private void ShowAllTestTypes()
        {
            GetAllTestTypes();

            dgvManageTestTypes.DataSource = AllTestTypes;

            lblCountRecords.Text = dgvManageTestTypes.RowCount.ToString();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            ShowAllTestTypes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((int)(dgvManageTestTypes.CurrentRow.Cells[0].Value)); 

            frm.ShowDialog();

            ShowAllTestTypes();
        }
    }
}
