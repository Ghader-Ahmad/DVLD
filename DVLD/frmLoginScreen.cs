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
using System.IO;

namespace DVLD
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        string path = @"E:\course(19)\DVLD\DVLD\Login.txt";

         clsUser CurrentUser;

        private bool IsThisUserActive()
        {
            if (CurrentUser.IsActive != 1)
            {
                MessageBox.Show("Your account has been deactivated. For assistance, Please contact your system administrator",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }

        private void Login()
        {
            if (txtPassword.Text == "" || txtUserName.Text == "")
                return;

            CurrentUser = clsUser.FindUser(txtUserName.Text, txtPassword.Text);

            if(CurrentUser != null)
            {
                if(IsThisUserActive())
                {
                    clsClobalSettings.CurrentUser = CurrentUser;
                    HomeScreen frm = new HomeScreen();
                    this.Hide();
                    frm.ShowDialog();

                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void WriteOnFile()
        {
            File.WriteAllText(path, $"{txtUserName.Text},{txtPassword.Text}");
        }

        private void ReadFromFile()
        {
            if (File.Exists(path))
            {
                string[] Lines = File.ReadAllLines(path);

                foreach (string item in Lines)
                {
                    string[] arr = (item.Split(','));
                    
                    txtUserName.Text = arr[0];
                    txtPassword.Text = arr[1];

                    cbRememberMe.Checked = true;
                }
            }
        }

        private void ClearFile()
        {
            File.WriteAllText(path, string.Empty);
        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            if(cbRememberMe.Checked)
                WriteOnFile();

            this.Close();
        }



        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
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



        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }



        private void cbRememberMe_Click(object sender, EventArgs e)
        {
            if(cbRememberMe.Checked)
            {
                if(txtPassword.Text != "" && txtUserName.Text != "")
                    WriteOnFile();

                else
                {
                    cbRememberMe.Checked = false;
                    MessageBox.Show("Please Enter UserName and Password", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                ClearFile();
            }
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            ReadFromFile();
        }
    }
}
