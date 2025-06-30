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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD.clsClobalSettings;

namespace DVLD.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        string imagesFolder = @"C:\DVLD-People-Images";

        private int _LicenseID;

        private clsPeople _PersonInfo;
        private clsLicense _LicenseInfo;

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadctrlDriverLicenseInfo(int LicesneID)
        { 
            _LicenseID = LicesneID;

            _LicenseInfo = clsLicense.FindLicenseByLicenseID(_LicenseID);

            if (_LicenseInfo != null)
            {
                _PersonInfo = clsPeople.FindByID(clsApplications.FindApplicationByID(_LicenseInfo.ApplicationID).ApplicationPersonID);

                LoadDateToctrlDriverLicenseInfo();
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

        private string LicenseIsDetained()
        {
            return (clsDetainedLicenses.LicenseIsDetained(_LicenseInfo.LicenseID) == true) ? "No" : "Yes";  
        }

        private string GetIssueReason(byte IssueReason)
        {
            switch (IssueReason)
            {
                case 1:
                    return "First Time";

                case 2:
                    return "Renew";

                case 3:
                    return "Replacement for Damaged";

                case 4:
                    return "Replacement for Lost";
            }
            return "";
        }


        private void LoadDateToctrlDriverLicenseInfo()
        {
            lblClass.Text = clsLicenseClass.FindLicenseClassByID(_LicenseInfo.LicenseClass).ClassName;
            lblName.Text = $"{_PersonInfo.FirstName} {_PersonInfo.SecondName} {_PersonInfo.ThridName} {_PersonInfo.LastName}";
            lblLicenseID.Text = _LicenseInfo.LicenseID.ToString();
            lblNationalNo.Text = _PersonInfo.NationalNo;
            lblGender.Text = WhatGender(_PersonInfo.Gender);
            lblIssueDate.Text = _LicenseInfo.IssueDate.ToString("dd/MMM/yyyy");
            lblIssueReason.Text = GetIssueReason(_LicenseInfo.IssueReason);
            lblNotes.Text = _LicenseInfo.Notes;
            lblIsActive.Text = (_LicenseInfo.IsActive == 1) ? "Yes" : "No";
            lblDateOfBirth.Text = _PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = _LicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy");
            lblIsDetained.Text = LicenseIsDetained();
            ViewAPersonImage();
        }
    }
}
