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

namespace DVLD.Users
{
    public partial class frmChangedPassword : Form
    {
        private int _PersonID = -1;
        private clsUser _UserInfo;

        public frmChangedPassword(int PersonID)
        {
            InitializeComponent();
       
            _PersonID = PersonID;
       
            _UserInfo = clsUser.FindUserByPersonID(_PersonID);
        }

        private string GetPassword()
        {
            return _UserInfo.Password;
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text) || string.IsNullOrEmpty(txtCurrentPassword.Text) ||
                txtConfirmPassword.Text != txtNewPassword.Text)
                return;

            if(clsUser.ChangePassword(_UserInfo.UserID, txtNewPassword.Text))
            {
                MessageBox.Show("Password Changed Successfully.", "Saved", MessageBoxButtons.OK,
                    MessageBoxIcon.Information); ;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChangedPassword_Load(object sender, EventArgs e)
        {
            ctrlUserInformation1.LoadctrlUserInformation(_PersonID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtCurrentPassword.Text != GetPassword())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong!");
                txtCurrentPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank");
                txtNewPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtConfirmPassword.Text) || (txtConfirmPassword.Text != txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm your password");
                txtConfirmPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
