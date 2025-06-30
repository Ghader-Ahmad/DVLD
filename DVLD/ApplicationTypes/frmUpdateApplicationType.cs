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

namespace DVLD.ApplicationTypes
{
    public partial class frmUpdateApplicationType : Form
    {
        int _ApplicationID = -1;
        clsApplicationTypes _ApplicationTypes;
        public frmUpdateApplicationType(int ApplicationID)
        {
            InitializeComponent();

            _ApplicationID = ApplicationID;
        }

        private void LoadData()
        {
            _ApplicationTypes = clsApplicationTypes.FindApplication(_ApplicationID);

            lblApplicationID.Text = _ApplicationTypes.ApplicationID.ToString();
            txtTitle.Text = _ApplicationTypes.ApplicationTitle;
            txtFees.Text = _ApplicationTypes.ApplicationFees.ToString();
        }

        private void Save()
        {
            if (txtTitle.Text == "" || txtFees.Text == "")
                return;

            _ApplicationTypes.ApplicationTitle = txtTitle.Text;
            _ApplicationTypes.ApplicationFees = Convert.ToSingle(txtFees.Text);

            if (_ApplicationTypes.UpdateApplicationType())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider1.SetError(txtTitle, "Title cannot be blank");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, "");
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                txtFees.Focus();
                errorProvider1.SetError(txtFees, "Fees cannot be blank");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, "");
            }
        }
    }
}
