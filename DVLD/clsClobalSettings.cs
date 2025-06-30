using DataBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD
{
    public class clsClobalSettings
    {
        static clsUser _CurrentUser = new clsUser();
        public static clsUser CurrentUser
        {
            get { return _CurrentUser; }

            set { _CurrentUser = value; }
        }


        public enum enApplicationTypeTitle { NewLocalDrivingLicenseService = 1, RenewDrivingLicenseService = 2,
        ReplacementforaLostDrivingLicense = 3, ReplacementforaDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5,
            NewInternationalLicense = 6, RetakeTest = 7
        };

        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplacementforDamaged = 3, ReplacementforLost = 4};
    }
}
