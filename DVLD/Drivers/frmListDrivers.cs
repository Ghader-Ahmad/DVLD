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

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private DataTable ListDriversDataTable;
        private void GetListDrivers()
        {
            ListDriversDataTable = clsDriver.GetListDrivers();
        }

        private void ShowListDrivers()
        {
            GetListDrivers();

            dgvShowListDrivers.DataSource = ListDriversDataTable;

            dgvShowListDrivers.Columns["DriverID"].HeaderText = "Driver ID";
            dgvShowListDrivers.Columns["PersonID"].HeaderText = "Person ID";
            dgvShowListDrivers.Columns["NationalNo"].HeaderText = "National No";
            dgvShowListDrivers.Columns["FullName"].HeaderText = "Full Name";
            dgvShowListDrivers.Columns["CreatedDate"].HeaderText = "Date";
            dgvShowListDrivers.Columns["ActiveLicneses"].HeaderText = "Active Licneses";

            lblCountRecords.Text = dgvShowListDrivers.RowCount.ToString();  
        }

        private void ShowListDriverAfterFilter(DataView dv, string FilterBy)
        {
            dgvShowListDrivers.ClearSelection();

            if (FilterBy == "FullName")
            {
                dv.RowFilter = $"{FilterBy} like '{txtFilter.Text}' + '' + '%' or {FilterBy} like '%' + '' + '{txtFilter.Text}'" +
                    $"or {FilterBy} like '%' + '{txtFilter.Text}' + '%'";
                dgvShowListDrivers.DataSource = dv;
            }
            else
            {
                dv.RowFilter = $"{FilterBy} = '{txtFilter.Text}'";
                dgvShowListDrivers.DataSource = dv;
            }

            dgvShowListDrivers.Columns["DriverID"].HeaderText = "Driver ID";
            dgvShowListDrivers.Columns["PersonID"].HeaderText = "Person ID";
            dgvShowListDrivers.Columns["NationalNo"].HeaderText = "National No";
            dgvShowListDrivers.Columns["FullName"].HeaderText = "Full Name";
            dgvShowListDrivers.Columns["CreatedDate"].HeaderText = "Date";
            dgvShowListDrivers.Columns["ActiveLicneses"].HeaderText = "Active Licneses";

            lblCountRecords.Text = dgvShowListDrivers.RowCount.ToString();
        }

        private void Filter()
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                ShowListDrivers();
                return;
            }

            DataView ListDriversDataView = ListDriversDataTable.DefaultView;

            switch (cbFilter.Text)
            {
                case "Person ID":
                    {
                        ShowListDriverAfterFilter(ListDriversDataView, "PersonID");
                        break;
                    }
                case "Driver ID":
                    {
                        ShowListDriverAfterFilter(ListDriversDataView, "DriverID");
                        break;
                    }
                case "Full Name":
                    {
                        ShowListDriverAfterFilter(ListDriversDataView, "FullName");
                        break;
                    }
                case "National No":
                    {
                        ShowListDriverAfterFilter(ListDriversDataView, "NationalNo");
                        break;
                    }
            }

        }

        private void IfFilterEqualNone()
        {
            cbFilter.Text = "";
            txtFilter.Text = "";
            txtFilter.Visible = false;
            ShowListDrivers();
        }



        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            ShowListDrivers();
        }

        private void cbFilter_TextChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = true;

            if(cbFilter.Text == "None")
            {
                IfFilterEqualNone();
                return;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text == "Person ID" || cbFilter.Text == "Driver ID")
            {
                if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
