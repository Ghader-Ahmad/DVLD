using DataBusinessLayer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace DVLD.People
{
    public partial class ctrlPersonInformation : UserControl
    {
        public ctrlPersonInformation()
        {
            InitializeComponent();
        }

        string imagesFolder = @"C:\DVLD-People-Images";
        private clsPeople _PersonInfo = new clsPeople();
        private int _PersonID = -1;

        public void LoadctrlPersonInformation(int PersonID)
        {
            _PersonID = PersonID;

            ViewThisPersonData();
        }

        public void ResetctrlPersonInformation()
        {
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblPhone.Text = "[????]";
            lblCountry.Text = "[????]";

            lblEditPersonInfo.Enabled = false;

            pbPersonImage.Image = Resources.Male_512;
        }

        private bool BringAPersonDataFromTheDatabase()
        {
            _PersonInfo = clsPeople.FindByID(_PersonID);

            if (_PersonInfo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void WhatIsGender(byte Gender)
        {
            if(Gender == 0)
            {
                lblGender.Text = "Male";
            
            }
            else
            {
                lblGender.Text = "Female";
              
            }
        }

        private void IsMaleOrFemale(string Gender)
        {
            if (Gender == "Male")
            {
                pbPersonImage.Image = Resources.Male_512;
                pbPersonImage.Tag = "Default";
            }

            if (Gender == "Female")
            {
                pbPersonImage.Image = Resources.Female_512;
                pbPersonImage.Tag = "Default";
            }

        }

        private void ViewAPersonImage()
        {
            if (_PersonInfo.ImagePath != "")
            {
                using (FileStream fs = new FileStream(Path.Combine(imagesFolder, _PersonInfo.ImagePath),
                    FileMode.Open, FileAccess.Read))
                {
                    pbPersonImage.Image = Image.FromStream(fs);
                }
            }

            else
                IsMaleOrFemale(lblGender.Text);
        }

        private void ViewThisPersonData()
        {
            if (!BringAPersonDataFromTheDatabase())
                return;

            lblEditPersonInfo.Enabled = true;

            lblPersonID.Text = _PersonInfo.PersonID.ToString();
            lblName.Text = $"{_PersonInfo.FirstName} {_PersonInfo.SecondName} {_PersonInfo.ThridName} {_PersonInfo.LastName}";
            lblNationalNo.Text = _PersonInfo.NationalNo;
            WhatIsGender(Convert.ToByte(_PersonInfo.Gender));
            lblEmail.Text = _PersonInfo.Email;
            lblAddress.Text = _PersonInfo.Address;
            lblDateOfBirth.Text = _PersonInfo.DateOfBirth.ToString();
            lblPhone.Text = _PersonInfo.Phone;
            lblCountry.Text = clsCountry.FindCountryByID(_PersonInfo.NationalityCountryID).CountryName;
            ViewAPersonImage();
        }




        private void ctrlPersonDetails_Load(object sender, EventArgs e)
        {
            ViewThisPersonData();
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonInfo.PersonID);
            frm.ShowDialog();

            ViewThisPersonData();
        }
    }
}
