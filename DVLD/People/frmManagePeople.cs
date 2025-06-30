using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBusinessLayer;

namespace DVLD.People
{
    public partial class frmManagePeople: Form
    {
        public frmManagePeople()
        {
            InitializeComponent();

            ShowAllPeoples();
        }

        DataTable AllPeople = new DataTable();

        private DataTable GetAllPeople()
        {
            AllPeople = clsPeople.GetAllPeoples();

            lblCountRecords.Text = AllPeople.Rows.Count.ToString();

            return AllPeople;
        }

        private void ShowAllPeoples()
        {
            dgfShowAllPeoples.DataSource = GetAllPeople();
        }

        private void ShowAllPeopleFromDataView(DataView dv, string FilterBy)
        {
            dgfShowAllPeoples.ClearSelection();
            dv.RowFilter = $"{FilterBy} = '{txtFilter.Text}'";
            dgfShowAllPeoples.DataSource = dv;
        }

        private void Filter ()
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                ShowAllPeoples();
                return;
            }

            DataView PeopleDataView = AllPeople.DefaultView;
            
            switch (cbFilter.Text)
            {
                case "Person ID":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "PersonID");
                        break;
                    }
                case "National No":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "NationalNo");
                        break;
                    }
                case "First Name":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "FirstName");
                        break;
                    }
                case "Second Name":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "SecondName");
                        break;
                    }
                case "Third Name":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "ThirdName");
                        break;
                    }
                case "Last Name":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "LastName");
                        break;
                    }
                case "Nationality":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "Nationality");
                        break;
                    }
                case "Gendor":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "Gendor");
                        break;
                    }
                case "Phone":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "Phone");
                        break;
                    }
                case "Email":
                    {
                        ShowAllPeopleFromDataView(PeopleDataView, "Email");
                        break;
                    }
            }
        }


        private void AddNewPerson()
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(-1);
            frm.ShowDialog();
        
            ShowAllPeoples();
        }






        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddNewPerson();
        }
        private void AddNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPerson();
        }


        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)dgfShowAllPeoples.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            ShowAllPeoples();
        }


        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete person [ " + dgfShowAllPeoples.CurrentRow.Cells[0].Value + " ]",
                "Confirm Delete: ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPeople.DeletePerson((int)(dgfShowAllPeoples.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Person deleted successfully", "Successfed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowAllPeoples();
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error",MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }


        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonDetails frm = new frmShowPersonDetails((int)dgfShowAllPeoples.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            ShowAllPeoples();
        }


        private void cbFilter_TextChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = true;

            if (cbFilter.Text == "None")
            {
                cbFilter.Text = "";
                txtFilter.Text = "";
                txtFilter.Visible = false;
                ShowAllPeoples();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }



        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text == "Phone" ||  cbFilter.Text == "Person ID")
            {
                if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }



        private void sendEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not implemented yet!", "Not Ready!", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneCailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not implemented yet!", "Not Ready!",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

       
    }
}
