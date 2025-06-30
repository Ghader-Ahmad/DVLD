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
    public partial class frmUpdateTestType : Form
    {
        private int _TestID = 0;
        private clsTestType _testType;

        public frmUpdateTestType(int TestID)
        {
            InitializeComponent();

            _TestID = TestID;
        }

        private void LoadData()
        {
            _testType = clsTestType.FindTestType(_TestID);

            if (_testType != null)
            {
                lblTestID.Text = _testType.TestID.ToString();
                txtTestTitle.Text = _testType.TestTitle;
                txtTestDecription.Text = _testType.TestDescription;
                txtTestFees.Text = _testType.TestFees.ToString();
            }
        }

        private void SaveAfterUpate()
        {
            if (txtTestTitle.Text == "" || txtTestDecription.Text == "" || txtTestFees.Text == "")
                return;

            _testType.TestTitle = txtTestTitle.Text;
            _testType.TestDescription = txtTestDecription.Text;
            _testType.TestFees = Convert.ToSingle(txtTestFees.Text);

            if(_testType.UpdateTest())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAfterUpate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTestFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTestTitle_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtTestTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTitle, "Title cannot be blank");
                txtTestTitle.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTestTitle, "");
            }
        }

        private void txtTestDecription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestDecription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestDecription, "Title cannot be blank");
                txtTestDecription.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTestDecription, "");
            }
        }
    }
}
