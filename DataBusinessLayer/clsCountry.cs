using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        private clsCountry (int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static clsCountry FindCountryByName(string CountryName)
        {
            int CountryID = -1;

            if(clsCountryData.FindCountryByName(ref CountryID, CountryName))
                return new clsCountry(CountryID, CountryName);

            else 
                return null;
        }

        public static clsCountry FindCountryByID(int CountryID)
        {
            string CountryName = "";

            if (clsCountryData.FindCountryByID(CountryID, ref CountryName))
                return new clsCountry(CountryID, CountryName);

            else
                return null;
        }
    }
}
