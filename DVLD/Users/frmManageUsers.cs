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
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }

        DataTable UsersDataTable = new DataTable();

        private void GetAllUsers()
        {
            UsersDataTable = clsUser.GetAllUsers();

            lblCountUsers.Text = UsersDataTable.Rows.Count.ToString();  
        }

        private void ShowAllUsers()
        {
            GetAllUsers();

            dgvShowAllUsers.DataSource = UsersDataTable;
        }

        private void ShowAllUsersFromDataView(DataView dv, string FilterBy)
        {
            dgvShowAllUsers.ClearSelection();

            if (FilterBy == "UserName" || FilterBy == "FullName")
            {
                dv.RowFilter = $"{FilterBy} like '{txtFilter.Text}' + '' + '%'";
                dgvShowAllUsers.DataSource = dv;
            }
            else
            {
                dv.RowFilter = $"{FilterBy} = '{txtFilter.Text}'";
                dgvShowAllUsers.DataSource = dv;
            }
        }

        private void Filter()
        {
         
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                ShowAllUsers();
                return;
            }

            DataView UsersDataView = UsersDataTable.DefaultView;

            switch (cbFilter.Text)
            {
                case "Person ID":
                    {
                        ShowAllUsersFromDataView(UsersDataView, "PersonID");
                        break;
                    }
                case "User ID":
                    {
                        ShowAllUsersFromDataView(UsersDataView, "UserID");
                        break;
                    }
                case "Full Name":
                    {
                        ShowAllUsersFromDataView(UsersDataView, "FullName");
                        break;
                    }
                case "UserName":
                    {
                        ShowAllUsersFromDataView(UsersDataView, "UserName");
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
            ShowAllUsers();
        }

        private void GetActiveUsers()
        {
            DataView dv = UsersDataTable.DefaultView;

            dv.RowFilter = $"IsActive = {1}";
            dgvShowAllUsers.DataSource = dv;
        }

        private void GetNotActiveUsers()
        {
            DataView dv = UsersDataTable.DefaultView;

            dv.RowFilter = $"IsActive = {0}";
            dgvShowAllUsers.DataSource = dv;
        }

        private void FilterByIsActiveOrNotUsers()
        {
            switch (cbIsActive.Text)
            {
                case "All":
                    {
                        ShowAllUsers();
                        break;
                    }

                case "Yes":
                    {
                        GetActiveUsers();
                        break;
                    }

                case "No":
                    {
                        GetNotActiveUsers();
                        break;
                    }
            }
        }

        private void IfFilterEqualIsActive()
        {
            txtFilter.Visible = false;
            cbIsActive.Visible = true;
        }
 
        private void AddNewUser()
        {
            frmAddUpdateUser frm = new frmAddUpdateUser(-1);
            frm.ShowDialog();

            ShowAllUsers();
        }



        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            ShowAllUsers();
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
            if(cbFilter.Text == "Is Active")
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
            FilterByIsActiveOrNotUsers();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            AddNewUser();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)(dgvShowAllUsers.CurrentRow.Cells[1].Value));
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)(dgvShowAllUsers.CurrentRow.Cells[1].Value));
            frm.ShowDialog();

            ShowAllUsers();
        }

        private void changedPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangedPassword frm = new frmChangedPassword((int)(dgvShowAllUsers.CurrentRow.Cells[1].Value));

            frm.ShowDialog();
            ShowAllUsers();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete user [ " + dgvShowAllUsers.CurrentRow.Cells[0].Value + " ]",
               "Confirm Delete: ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsUser.DeleteUser((int)(dgvShowAllUsers.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("User deleted successfully", "Successfed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowAllUsers();
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text == "Person ID" || cbFilter.Text == "User ID")
            {
                if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not implemented yet!", "Not Ready!",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void sendCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not implemented yet!", "Not Ready!",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
