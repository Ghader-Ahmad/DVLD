using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsDetainedLicenses
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

        public clsDetainedLicenses()
        {
            DetainID = 0;
            LicenseID = 0;
            DetainDate = default;
            FineFees = 0;
            CreatedByUserID = 0;
            IsReleased = false;
            ReleaseDate = default;
            ReleasedByUserID = 0;
            ReleaseApplicationID = 0;
        }
        
        private clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, float FineFees,
        int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;   
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
        }

        public static bool LicenseIsDetained(int LicenseID)
        {
            return clsDetainedLicensesData.LicenseIsDetained(LicenseID);
        }

        public bool DetainedLicense()
        {
            this.DetainID = clsDetainedLicensesData.DetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID,
                 this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);

            return (this.DetainID != -1);
        }

        public bool ReleasedLicense()
        {
            return clsDetainedLicensesData.ReleasedLicense(this.LicenseID, this.IsReleased, this.ReleaseDate,
                    this.ReleasedByUserID, this.ReleaseApplicationID);
        }

        public static clsDetainedLicenses FindDetainedLicense(int LicenseID)
        {
            int DetainID = 0, CreatedByUserID = 0, ReleasedByUserID = 0, ReleaseApplicationID = 0;
            DateTime DetainDate = default, ReleaseDate = default;
            bool IsReleased = false; float FineFees = 0;

            if (clsDetainedLicensesData.FindDetainedLicense(ref DetainID, LicenseID, ref DetainDate, ref FineFees,
        ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees,
                 CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }

        public static DataTable GetListDetainedLicenses()
        {
            return clsDetainedLicensesData.GetListDetainedLicenses();
        }
    }
}
