using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsTestAppointment
    {
        enum enMode { AddNew = 0, Update =  1 };
        enMode _Mode;

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public byte IsLocked { get; set; }

        public clsTestAppointment()
        {
            _Mode = enMode.AddNew;

            this.TestAppointmentID = 0;
            this.TestTypeID = 0;
            this.LocalDrivingLicenseApplicationID = 0;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = 0;
            this.IsLocked = 0;
        }

        private clsTestAppointment(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID,
            DateTime appointmentDate, float paidFees, int createdByUserID, byte isLocked)
        {
            _Mode = enMode.Update;

            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            return clsTestAppointmentData.UpdateAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, 
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID,this.IsLocked);
        }


        public static bool IsTestAppointmentActiveByLocalLicenseID(int LocalLicenseID)
        {
            return clsTestAppointmentData.IsTestAppointmentActiveByLocalLicenseID(LocalLicenseID);
        }

        public static bool IsTestAppointmentActiveByTestAppointmentID(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsTestAppointmentActiveByTestAppointmentID(TestAppointmentID);
        }

        public static int GetTheNumberOfTests(int LocalLicenseID, byte TestTypeID)
        {
            return clsTestAppointmentData.GetTheNumberOfTests(LocalLicenseID, TestTypeID);
        }

        public static clsTestAppointment FindTestAppontment(int LocalLicenseID, int TestTypeID)
        {
            int TestAppointmentID = 0, CreatedUserByID = 0 ;
            byte IsLocked = 0;
            float PaidFees = 0;
            DateTime AppointmentDate = DateTime.Now;

            if (clsTestAppointmentData.FindTestAppontment(ref TestAppointmentID, TestTypeID, LocalLicenseID, ref AppointmentDate,
               ref PaidFees, ref CreatedUserByID, ref IsLocked))
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalLicenseID, AppointmentDate,
                    PaidFees, CreatedUserByID, IsLocked);
            }
            else
                return null;
        }

        public static clsTestAppointment FindTestAppontmentByID(int TestAppointmentID)
        {
            int TestTypeID = 0, CreatedUserByID = 0, LocalLicenseID = 0;
            byte IsLocked = 0;
            float PaidFees = 0;
            DateTime AppointmentDate = DateTime.Now;

            if (clsTestAppointmentData.FindTestAppontmentByID(TestAppointmentID,ref TestTypeID,ref LocalLicenseID, ref AppointmentDate,
               ref PaidFees, ref CreatedUserByID, ref IsLocked))
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalLicenseID, AppointmentDate,
                    PaidFees, CreatedUserByID, IsLocked);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    {
                        _Mode = enMode.Update;
                        return _AddNewTestAppointment();
                    }

                case enMode.Update:
                    {
                        return _UpdateAppointment();
                    }
            }
            return false;
        }

        public static bool LockedTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.LockedTestAppointment(TestAppointmentID);
        }

        public static bool TestAppointmentIsLocked(int TestAppointmentID)
        {
            return clsTestAppointmentData.TestAppointmentIsLocked(TestAppointmentID);   
        }

    }
}
