using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsLicense
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass {  get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public byte IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public static bool IsPersonHasLicense(int ApplicationID)
        {
            return clsLicenseData.IsPersonHasLicense(ApplicationID);
        }

        public clsLicense()
        {
            LicenseID = 0;
            ApplicationID = 0;
            DriverID = 0;
            LicenseClass = 0;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = 0;
            IssueReason = 0;
            CreatedByUserID = 0;
        }

        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate,
            DateTime expirationDate, string notes, float paidFees, byte isActive, byte issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
        }

        public bool IssueLicense()
        {
            this.LicenseID = clsLicenseData.IssueLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
            this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        public static clsLicense FindLicenseByApplicationID(int ApplicationID)
        {
            int LicenseID = 0, DriverID = 0, LicenseClass = 0, CreatedUserByID = 0;
            byte IsActive = 0, IssueReason = 0;
            DateTime IssueDate=DateTime.Now, ExpirationDate= DateTime.Now;
            string Notes = "";
            float PaidFees = 0;

            if (clsLicenseData.FindLicenseByApplicationID(ref LicenseID, ApplicationID, ref DriverID, ref LicenseClass,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedUserByID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedUserByID);
            }

            else
                return null;
        }

        public static clsLicense FindLicenseByLicenseID(int LicenseID)
        {
            int ApplicationID = 0, DriverID = 0, LicenseClass = 0, CreatedUserByID = 0;
            byte IsActive = 0, IssueReason = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;

            if (clsLicenseData.FindLicenseByLicenseID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedUserByID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedUserByID);
            }

            else
                return null;
        }

        public static DataTable GetAllDriverLicensesByDriverID(int DriverID)
        {
            return clsLicenseData.GetAllDriverLicensesByDriverID(DriverID);
        }

        public static bool LicenseIsActiveAndNotExpired(int LicenseID)
        {
            return clsLicenseData.LicenseIsActiveAndNotExpired(LicenseID);
        }

        public static bool LicenseIsActive(int LicenseID)
        {
            return clsLicenseData.LicenseIsActive(LicenseID);
        }

        public static int DoesThieDriverHaveAnotherActiceLicenseOfTheSameClass(int DriverID, int LicenseClass)
        {
            return clsLicenseData.DoesThieDriverHaveAnotherActiceLicenseOfTheSameClass(DriverID, LicenseClass);
        }

        public static bool ModifyLicenseStatus(int LicenseID, bool isActive)
        {
            return clsLicenseData.ModifyLicenseStatus(LicenseID, isActive); 
        }

        public static bool LicenseIsFromClassesThree(int LicenseID)
        {
            return clsLicenseData.LicenseIsFromClassesThree(LicenseID);
        }


    }
}
