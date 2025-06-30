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

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {
        private int _PersonID = -1;
        private clsUser _UserInfo;

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;


        public frmAddUpdateUser(int PersonID)
        {
            _PersonID = PersonID;

            InitializeComponent();

            _Mode = (_PersonID == -1)? enMode.AddNew : enMode.Update;
        }
        
        

        private void LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                this.Text = "Add New User";
                _UserInfo = new clsUser();
                ctrlPersonInformationWithFilter1.LoadctrlPersonInformationWithFilter(_PersonID);

                return;
            }

            lblMode.Text = "Update User";
            this.Text = "Update User";

            _UserInfo = clsUser.FindUserByPersonID(_PersonID);

            if (_UserInfo == null)
                return;


            ctrlPersonInformationWithFilter1.LoadctrlPersonInformationWithFilter(_PersonID);

            lblUserID.Text = _UserInfo.UserID.ToString();
            txtUserName.Text = _UserInfo.UserName;
            txtPassword.Text = _UserInfo.Password;
            txtConfirmPassword.Text = txtPassword.Text;
            cbIsActive.Checked = (_UserInfo.IsActive == 1) ? true : false;
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Next()
        {
            if (ctrlPersonInformationWithFilter1.PersonID == -1)
                return;


            if (clsUser.IsUserExist(ctrlPersonInformationWithFilter1.PersonID))
            {
                MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            tabControl1.SelectedIndex = 1;

        }

        private void  Save()
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text) ||
                (txtConfirmPassword.Text != txtPassword.Text))
                return;


            _UserInfo.Password = txtPassword.Text;
            _UserInfo.UserName = txtUserName.Text;  
            _UserInfo.IsActive = Convert.ToByte((cbIsActive.Checked) ? 1 : 0);
            _UserInfo.PersonID = ctrlPersonInformationWithFilter1.PersonID;

            if(_UserInfo.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblMode.Text = "Update User";
                lblUserID.Text = _UserInfo.UserID.ToString();

                this.Text = "Update User";
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUserName.Text))
            {
                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "UserName cannot be blank");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text )
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
