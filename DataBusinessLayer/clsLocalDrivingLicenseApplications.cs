using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsLocalDrivingLicenseApplications : clsApplications
    {
        enum enMode { AddNew = 0, Update =  1 };
        enMode _Mode;

        private bool _AddNewLocalLicense()
        {
            this.LocalDrivingLicenseApplicationsID = clsLocalDrivingLicenseApplicationsData.AddNewLocalLicense(this.ApplicationID, this.ApplicationPersonID, this.ApplicationDate,
             this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID,
                this.LicenseClassID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateLocalLicense()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLocalLicense(this.ApplicationID, this.ApplicationPersonID, this.ApplicationDate,
              this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID, this.LocalDrivingLicenseApplicationsID,
                this.LicenseClassID);
        }

        public int LocalDrivingLicenseApplicationsID { get; set; }
        public int LicenseClassID { get; set; }

        public int ApplicationID { get; set; }


        public clsLocalDrivingLicenseApplications()
        {
            _Mode = enMode.AddNew;

            LocalDrivingLicenseApplicationsID = 0;
            LicenseClassID = 0;

            ApplicationID = 0;
            ApplicationPersonID = 0;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 0;
            ApplicationStatus = 0;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
        }

        private clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationsID, int ApplicationID,
            int LicenseClassID, int applicationPersonID, DateTime applicationDate,
            byte applicationTypeID, byte applicationStatus, DateTime lastStatusDate, float paidFees, int createdByUserID)
            : base(ApplicationID, applicationPersonID, applicationDate, applicationTypeID, applicationStatus,
                lastStatusDate, paidFees, createdByUserID)
        {
            _Mode = enMode.Update;

            this.LocalDrivingLicenseApplicationsID= LocalDrivingLicenseApplicationsID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
        }

        private clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationsID, int LicenseClassID, int ApplicationID)
        {
            this.LocalDrivingLicenseApplicationsID = LocalDrivingLicenseApplicationsID;
            this.LicenseClassID = LicenseClassID;
            this.ApplicationID = ApplicationID;
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    {
                        _Mode = enMode.AddNew;
                        return _AddNewLocalLicense();
                    }

                case enMode.Update:
                    {
                        return _UpdateLocalLicense();
                    }
            }
            return false;
        }

        public static clsLocalDrivingLicenseApplications FindLocalLicenseByApplicationID(int ApplicationID)
        {
            int LocalLicenseID = 0, LicenseCLassID = 0, ApplicationPersonID = 0, CreatedByUserID = 0;
            byte ApplicationTypeID = 0, ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            float PaidFees = 0;

            if (clsLocalDrivingLicenseApplicationsData.FindLocalLicenseByApplicationID(ApplicationID, ref ApplicationPersonID, ref ApplicationDate,
            ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
            ref CreatedByUserID, ref LocalLicenseID, ref LicenseCLassID))
            {
                return new clsLocalDrivingLicenseApplications(LocalLicenseID, ApplicationID, LicenseCLassID, ApplicationPersonID,
                    ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }

            else
                return null;
        }

        public static clsLocalDrivingLicenseApplications FindLocalLicenseByLocalLicenseID(int LocalLicenseID)
        {
            int ApplicationID = 0, LicenseCLassID = 0, ApplicationPersonID = 0, CreatedByUserID = 0;
            byte ApplicationTypeID = 0, ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            float PaidFees = 0;

            if (clsLocalDrivingLicenseApplicationsData.FindLocalLicenseByLocalLicenseID(ref ApplicationID, ref ApplicationPersonID, ref ApplicationDate,
            ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
            ref CreatedByUserID,  LocalLicenseID, ref  LicenseCLassID))
            {
                return new clsLocalDrivingLicenseApplications(LocalLicenseID, ApplicationID, LicenseCLassID, ApplicationPersonID,
                     ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
                return null;
        }


        public static int ApplicationStatusIsCompleted(int ApplicationPersonID, int LicenseCLassID, int ApplicationStatus)
        {
            return clsLocalDrivingLicenseApplicationsData.ApplicationStatusIsCompleted(ApplicationPersonID, LicenseCLassID, ApplicationStatus);
        }

        public static int ApplicationStatusIsNew(int ApplicationPersonID, int LicenseCLassID, int ApplicationStatus)
        {
            return clsLocalDrivingLicenseApplicationsData.ApplicationStatusIsNew(ApplicationPersonID, LicenseCLassID, ApplicationStatus);
        }


        public static int GetApplicationIDFromLocalApplicationID(int LocalApplicationID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetApplicationIDFromLocalApplicationID(LocalApplicationID);
        }

        public static DataTable GetDrivingLicenseApplicationInfo(int LocalLicneseID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetDrivingLicenseApplicationInfo(LocalLicneseID);
        }


        public static bool DeleteLocalLicenseApplication(int LocalLicenseID)
        {
            return clsLocalDrivingLicenseApplicationsData.DeleteLocalLicenseApplication(LocalLicenseID);
        }

    }
}
