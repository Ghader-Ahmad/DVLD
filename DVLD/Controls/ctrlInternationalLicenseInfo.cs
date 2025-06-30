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

namespace DVLD.Controls
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        string imagesFolder = @"C:\DVLD-People-Images";

        private clsInternationalLicenses _internationalLicenses;
        private clsPeople _PersonInfo;

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

        private string WhatGender(byte Gender)
        {
            if (Gender == 0)
            {
                return "Male";

            }
            else
            {
                return "Female";
            }
        }

        private void LoadDate()
        {
            lblName.Text = $"{_PersonInfo.FirstName} {_PersonInfo.SecondName} {_PersonInfo.ThridName} {_PersonInfo.LastName}";
            lblIntLicenseID.Text = _internationalLicenses.InternationalLicenseID.ToString();
            lblLicenseID.Text = _internationalLicenses.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _PersonInfo.NationalNo;
            lblGender.Text = WhatGender(_PersonInfo.Gender);
            lblIssueDate.Text = _internationalLicenses.IssueDate.ToString("dd/MMM/yyyy");
            lblApplicationID.Text = _internationalLicenses.ApplicationID.ToString();
            lblIsActive.Text = (_internationalLicenses.IsActive == 1) ? "Yes" : "No";
            lblDateOfBirth.Text = _PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblDriverID.Text = _internationalLicenses.DriverID.ToString();
            lblExpirationDate.Text = _internationalLicenses.ExpirationDate.ToString("dd/MMM/yyyy");

            ViewAPersonImage();
        }

        public void LoadctrlInternationalLicenseInfo(int InternaionalLicenseID)
        {
            _internationalLicenses = clsInternationalLicenses.FindInternationalLicenseByID(InternaionalLicenseID);
            _PersonInfo = clsPeople.FindByID(clsApplications.FindApplicationByID(_internationalLicenses.ApplicationID).ApplicationPersonID);

            LoadDate();
        }
    }
}
