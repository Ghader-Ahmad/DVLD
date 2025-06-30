using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataBusinessLayer
{
    public class clsInternationalLicenses
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        public clsInternationalLicenses()
        {
            InternationalLicenseID = 0;
            ApplicationID = 0;
            DriverID = 0;
            IssuedUsingLocalLicenseID = 0;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = 0;
            CreatedByUserID = 0;
        }

        private clsInternationalLicenses(int InternationalLicenseID, int ApplicationID, int DriverID,
           int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, byte IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;   
            this.IsActive = IsActive;   
            this.CreatedByUserID = CreatedByUserID;
        }

        public static DataTable GetAllDriverInternationalLicensesByDriverID(int DriverID)
        {
            return clsInternationalLicensesData.GetAllDriverInternationalLicensesByDriverID(DriverID);
        }

     
        public static int InternationalLicenseIsExistByDriverID(int DriverID)
        {
            return clsInternationalLicensesData.InternationalLicenseIsExistByDriverID(DriverID);
        }

        public static clsInternationalLicenses FindInternationalLicenseByID(int InternationalLicenseID)
        {
            int ApplicationID = 0, DriverID = 0, IssuedUsingLocalLicenseID = 0, CreatedByUserID = 0;
            byte IsActive = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;

            if(clsInternationalLicensesData.FindInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicenses(InternationalLicenseID, ApplicationID, DriverID,
                        IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }

            else
            {
                return null;
            }
        }

        public bool AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicense(this.ApplicationID, this.DriverID,
                this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }


        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses();
        }


        public static bool InternationalLicenseIsActive(int InternationalLicenseID)
        {
           return clsInternationalLicensesData.InternationalLicenseIsActive(InternationalLicenseID);    
        }

        public static bool UpdateInternationalLicenseStatus(int InternationalLicenseID, bool IsActive)
        {
            return clsInternationalLicensesData.UpdateInternationalLicenseStatus(InternationalLicenseID, IsActive);
        }
    }
}
