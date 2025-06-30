using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsDriver
    {
        public int DriverID {  get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            DriverID = 0;
            PersonID = 0;
            CreatedByUserID = 0;
            CreatedDate = DateTime.Now;
        }

        public static int PersonIsADriver(int PersonID)
        {
            return clsDriverData.PersonIsADriver(PersonID);
        }

        public bool AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID != -1);
        }

        public static int GetDriverID(int PersonID)
        {
            return clsDriverData.GetDriverID(PersonID);
        }

        public static DataTable GetListDrivers()
        {
            return clsDriverData.GetListDrivers();
        }
    }
}
