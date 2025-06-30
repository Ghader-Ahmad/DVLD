using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsTestType
    {
        public int TestID { get; set; }
        public string TestTitle { get; set; }
        public string TestDescription { get; set; }
        public float TestFees { get; set; }

        private clsTestType(int TestID, string TestTitle, string TestDescription, float TestFees)
        {
            this.TestID = TestID;
            this.TestTitle = TestTitle;
            this.TestDescription = TestDescription;
            this.TestFees = TestFees;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }

        public static clsTestType FindTestType(int TestID)
        {
            string TestTitle = "", TestDiscription = "";
            float TestFees = 0;

            if (clsTestTypesData.FindTestType(TestID, ref TestTitle, ref TestDiscription, ref TestFees))
            {
                return new clsTestType(TestID, TestTitle, TestDiscription, TestFees);
            }

            else
                return null;
        }

        public bool UpdateTest()
        {
            return clsTestTypesData.UpdateTestType(this.TestID, this.TestTitle, this.TestDescription, this.TestFees);
        }

        public static byte GetCountPassedTests(int LocalLicenseID)
        {
            return clsTestTypesData.GetCountPassedTests(LocalLicenseID);
        }
    }
}
