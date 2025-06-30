using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsApplicationTypes
    {
        public int ApplicationID { get; set; }
        public string ApplicationTitle { get; set; }
        public float ApplicationFees { get; set; }

        private clsApplicationTypes(int ApplicationID, string ApplicationTitle, float ApplicationFees)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationTitle = ApplicationTitle;
            this.ApplicationFees = ApplicationFees;
        }
        
        public static DataTable GetApplicationTypes()
        {
            return clsApplicationTypesData.GetApplicationTypes();
        }

        public bool UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationID, this.ApplicationTitle, this.ApplicationFees);
        }

        public static clsApplicationTypes FindApplication(int ID)
        {
            string Title = "";
            float Fees = 0;

            if (clsApplicationTypesData.FindApplication(ID, ref Title, ref Fees))
            {
                return new clsApplicationTypes(ID, Title, Fees);
            }

            else
                return null;
        }

        public static float GetFeesByApplicationTypeID(int ApplicationTypeID)
        {
            return clsApplicationTypesData.GetFeesByApplicationTypeID(ApplicationTypeID);
        }

        public static float GetFeesByApplicationTypeTitle(int ApplicationTypeID)
        {
            return clsApplicationTypesData.GetFeesByApplicationTypeTitle(ApplicationTypeID);
        }
    }
}
