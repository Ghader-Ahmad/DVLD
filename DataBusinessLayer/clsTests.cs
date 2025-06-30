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
    public class clsTests
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public byte TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTests()
        {
            TestID = 0;
            TestAppointmentID = 0;
            TestResult = 0;
            Notes = "";
            CreatedByUserID = 0;
        }

        public static bool HasThisTestBeenPassed(int LocalLicenseID, string TestType)
        {
            return clsTestsData.HasThisTestBeenPassed(LocalLicenseID, TestType);
        }

        public static DataTable GetTestAppointmentsByID(int LocalLicenseID, int TestTypeID)
        {
            return clsTestsData.GetTestAppointmentsByID(LocalLicenseID, TestTypeID);
        }

        public bool AddAndSaveTest()
        {
            this.TestID = clsTestsData.AddAndSaveTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);
        }
    }
}
