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

namespace DVLD.Controls
{
    public partial class ctrlPersonInformationWithFilter : UserControl
    {
        public ctrlPersonInformationWithFilter()
        {
            InitializeComponent();
        }

        private void cbFilter_TextChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Person ID")
                cbFilter.Tag = "PersonID";

            if (cbFilter.Text == "National No")
                cbFilter.Tag = "NationalNo";
        }

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        private clsPeople _Person;
        private int _PersonID;

        public int PersonID
        {
            get
            {
                if (_Person == null)
                    return -1;
                else
                    return _Person.PersonID;
            }
        }




        public void LoadctrlPersonInformationWithFilter(int PersonID)
        {
            _PersonID = PersonID;
            _Mode = (_PersonID == -1) ? enMode.AddNew : enMode.Update;

            LoadData();
        }

        private void LoadData()
        {
            if (_Mode == enMode.Update)
            {
                ctrlPersonInformation1.LoadctrlPersonInformation(_PersonID);
             
                cbFilter.SelectedIndex = 0;
                txtFilter.Text = _PersonID.ToString();

                gbFilter.Enabled = false;
            }

            if (_Mode == enMode.AddNew)
                gbFilter.Enabled = true;
        }


        private void FilterByPersonID()
        {
            _Person = clsPeople.FindByID(Convert.ToInt32(txtFilter.Text));

            if (_Person != null)
            {
                ctrlPersonInformation1.LoadctrlPersonInformation(_Person.PersonID);
            }
            else
            {
                ctrlPersonInformation1.ResetctrlPersonInformation();
                MessageBox.Show($"No Person with Person ID = {txtFilter.Text}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FilterByNationalNo()
        {
            _Person = clsPeople.FindByNationalNo(txtFilter.Text);

            if (_Person != null)
            {
                ctrlPersonInformation1.LoadctrlPersonInformation(_Person.PersonID);
            }

            else
            {
                ctrlPersonInformation1.ResetctrlPersonInformation();
                MessageBox.Show($"No Person with National No = {txtFilter.Text}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void Filter()
        {
            switch (cbFilter.Tag)
            {
                case "PersonID":
                    {
                        FilterByPersonID();
                        break;
                    }

                case "NationalNo":
                    {
                        FilterByNationalNo();
                        break;
                    }
            }
        }

        private void FormAddUpdatePerson_DataBack(object sender, int PersonID)
        {
            txtFilter.Text = PersonID.ToString();
            cbFilter.SelectedIndex = 1;
            FilterByPersonID();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(-1);
            frm.ctrlAdd_EditPerson1.DataBack += FormAddUpdatePerson_DataBack;
            frm.ShowDialog();
        }

        private void ctrlPersonInformationWithFilter_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
