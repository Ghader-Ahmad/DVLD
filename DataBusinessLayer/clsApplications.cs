using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsApplications
    {
        enum enMode { AddNew = 0, Update =  1 };
        enMode _Mode;

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsData.AddNewApplication(this.ApplicationPersonID, this.ApplicationDate,
                this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(this.ApplicationID, this.ApplicationPersonID, this.ApplicationDate,
                this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public int ApplicationID { get; set; }
        public int ApplicationPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public byte ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        
        public clsApplications()
        {
            _Mode = enMode.AddNew;

            ApplicationID = 0;
            ApplicationPersonID = 0;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 0;
            ApplicationStatus = 0;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
        }

        public  clsApplications(int applicationID, int applicationPersonID, DateTime applicationDate,
            byte applicationTypeID, byte applicationStatus, DateTime lastStatusDate, float paidFees, int createdByUserID)
        {
            _Mode = enMode.Update;
            
            ApplicationID = applicationID;
            ApplicationPersonID = applicationPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
        }

        public static DataTable GetLocalDrivingLicenseApplications()
        {
            return clsApplicationsData.GetLocalDrivingLicenseApplications();
        }

        public static clsApplications FindApplicationByID(int ApplicationID)
        {
            int ApplicationPersonID = 0, CreatedByUserID = 0;
            byte ApplicationTypeID = 0, ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            float PaidFees = 0;

            if(clsApplicationsData.FindApplicationByID(ApplicationID, ref ApplicationPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplications(ApplicationID, ApplicationPersonID, ApplicationDate,
                    ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }

            else
                return null;
        }

        public static clsApplications FindApplicationBy(int ApplicationPersonID, byte ApplicationTypeID)
        {
            int ApplicationID = 0, CreatedByUserID = 0;
            byte ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            float PaidFees = 0;

            if (clsApplicationsData.FindApplication(ref ApplicationID, ApplicationPersonID, ref ApplicationDate,
                ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplications(ApplicationID, ApplicationPersonID, ApplicationDate,
                    ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }

            else
                return null;
        }

        public bool Save ()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        _Mode = enMode.Update;
                        return _AddNewApplication();
                    }

                case enMode.Update:
                    {
                        return _UpdateApplication();
                    }
            }
            return false;
        }


        public static bool EditApplicationStatus(int ApplicationID, byte ApplicationStatus)
        {
            return clsApplicationsData.EditApplicationStatus(ApplicationID, ApplicationStatus);
        }

        public static DataTable GetApplicationBasicInfo(int ApplicationID)
        {
            return clsApplicationsData.GetApplicationBasicInfo(ApplicationID);
        }


        public static bool CompletedApplicationForRetakeTestIfApplicable(int ApplicationPersonID)
        {
            return clsApplicationsData.CompletedApplicationForRetakeTestIfApplicable(ApplicationPersonID);
        }

        public static bool UpdateApplicationStatusToCompletedStatus(int ApplicationID)
        {
            return clsApplicationsData.UpdateApplicationStatusToCompletedStatus(ApplicationID);
        }
    }
}
