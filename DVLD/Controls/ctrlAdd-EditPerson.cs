using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Properties;
using DataBusinessLayer;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.IO;

namespace DVLD
{
    public partial class ctrlAdd_EditPerson: UserControl
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        clsPeople _Person;
        private int _PersonId = -1;

        string imagesFolder = @"C:\DVLD-People-Images";
        string originalFilePath = "";
        private string _NewImageNameToSave = "";

        public void LoadctrlAddEditPerson(int PersonID)
        {
            _PersonId = PersonID;
            _Mode = (_PersonId == -1) ? enMode.AddNew : enMode.Update;
        }

        public int PersonID
        {
            get { return _Person.PersonID; }
        }

        public ctrlAdd_EditPerson()
        {
            InitializeComponent();
        }

        private void FillCountriesInComboBox()
        {
            DataTable CountriesDataTable = clsCountry.GetAllCountries();

            foreach (DataRow row in CountriesDataTable.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

        }

        private void IsMaleOrFemale()
        {
            if (rbMale.Checked == true)
            {
                pbPersonImage.Tag = "Default";
                pbPersonImage.Image = Resources.Male_512;
            }

            if (rbFemale.Checked == true)
            {
                pbPersonImage.Tag = "Default";
                pbPersonImage.Image = Resources.Female_512;
            }
        }

        private void UpdateDate()
        {
            DateTime Today = DateTime.Now;
            DateTime eighteenYearsAgo = Today.AddYears(-18);
            dtpDateOfBirth.Value = eighteenYearsAgo;
            dtpDateOfBirth.MaxDate = eighteenYearsAgo;
        }

        private void LoadData()
        {
            UpdateDate();
            FillCountriesInComboBox();
            cbCountry.Text = "Jordan";

            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPeople();
                return;
            }

            _Person = clsPeople.FindByID(_PersonId);

            lblMode.Text = "Update Person";
            lblPersonID.Text = _Person.PersonID.ToString();

            IsMaleOrFemale();

            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThridName;
            txtLastName.Text  = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;

            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Text = _Person.DateOfBirth.ToString();
            txtPhone.Text = _Person.Phone;

            cbCountry.SelectedIndex = _Person.NationalityCountryID - 1;

            if (_Person.ImagePath != "")
            {
                using (FileStream fs = new FileStream(Path.Combine(imagesFolder, _Person.ImagePath), FileMode.Open, FileAccess.Read))
                {
                    pbPersonImage.Image = Image.FromStream(fs);
                }
            }

            lblRemove.Visible = (_Person.ImagePath != "");
        }

        private byte GetGender()
        {
            if (rbMale.Checked)
                return 0;
            else
                return 1;
        }

        private void NewImageNameToSaveInDatabase()
        {
            string guideFileName = Guid.NewGuid().ToString();

            string extension = Path.GetExtension(originalFilePath);

            _NewImageNameToSave = guideFileName + extension;
        }

        private void SaveImage()
        {
            if(_Person.ImagePath != "" && _NewImageNameToSave != "")
            {
                File.Delete(Path.Combine(imagesFolder, _Person.ImagePath));
            }

            if ((pbPersonImage.Tag.ToString() == "Default"))
                return;

            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            string newImagePath = Path.Combine(imagesFolder, _NewImageNameToSave);


            File.Copy(originalFilePath, newImagePath);
        }

        private void Save()
        {
            int CountryID = clsCountry.FindCountryByName(cbCountry.Text).CountryID;

            lblPersonID.Text = _Person.PersonID.ToString();

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThridName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.Email = txtEmail.Text;
            _Person.Address = txtAddress.Text;
            _Person.Phone = txtPhone.Text;  
            _Person.NationalNo = txtNationalNo.Text;
            _Person.Gender = GetGender();
            _Person.NationalityCountryID = CountryID;
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            SaveImage();

            if (_NewImageNameToSave == "" && _Person.ImagePath == "")
                _Person.ImagePath = null;

            if (_NewImageNameToSave != "")
                _Person.ImagePath = _NewImageNameToSave;
            

            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully","Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            lblMode.Text = "Update Person";
        }





        private void ctrlAdd_EditPerson_Load(object sender, EventArgs e)
        {
            LoadData();
        }





        private void rbMale_CheckedChanged_1(object sender, EventArgs e)
        {
            IsMaleOrFemale();
        }




        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFirstName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();
                errorProvider1.SetError(txtFirstName, "Required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFirstName, "");
            }
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtSecondName.Text))
            {
                e.Cancel = true;
                txtSecondName.Focus();
                errorProvider1.SetError(txtSecondName, "Required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtSecondName, "");
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                e.Cancel = true;
                txtLastName.Focus();
                errorProvider1.SetError(txtLastName, "Required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLastName, "");
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "Required!");
                return;
            }
            if (clsPeople.PersonIsExist(txtNationalNo.Text))
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                if (!Regex.IsMatch(txtEmail.Text, emailPattern))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtEmail, "Please enter the email in a correct format!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtEmail, "");
                }
            }

        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtAddress.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAddress, "Required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAddress, "");
            }
        }




        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }



        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files | *.jpg;*.jpeg;*.png;*.bmp;*.gif;";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalFilePath = openFileDialog1.FileName;
                
                string ImageName = Path.GetFileNameWithoutExtension(originalFilePath);
                pbPersonImage.Tag = ImageName;

                NewImageNameToSaveInDatabase();

                using (FileStream fs = new FileStream(originalFilePath, FileMode.Open, FileAccess.Read))
                {
                    pbPersonImage.Image = Image.FromStream(fs);
                }
            }

            lblRemove.Visible = true;
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(pbPersonImage.Image != null)
            {
                pbPersonImage.Image.Dispose();
                pbPersonImage.Image = null;
            }

            if(_Person.ImagePath != "")
            {
                File.Delete(Path.Combine(imagesFolder, _Person.ImagePath));
                _Person.ImagePath = "";
            }

            if (_NewImageNameToSave != "")
                _NewImageNameToSave = "";

            IsMaleOrFemale();
            lblRemove.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form parentForm = this.ParentForm;

            if (parentForm != null)
            {
                DataBack?.Invoke(this, Convert.ToInt32(lblPersonID.Text));

                parentForm.Close();
            }
        }
    }
}
