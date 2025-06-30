using System;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Reflection.Emit;
using DataAccessLayer;


namespace DataBusinessLayer
{
    public class clsPeople
    {
        enum enMode { AddNew =0, Update = 1 };
        enMode _Mode ;

        public int PersonID {  get; set; }  
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThridName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThridName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address,this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return (this.PersonID != -1);
        }

        private bool _UpdateMode()
        {
            return clsPeopleData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThridName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }

        public clsPeople()
        {
            _Mode = enMode.AddNew;

            PersonID = 0;
            FirstName = "";
            SecondName = "";
            ThridName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gender = 0;
            Address = "";
            Phone = "";
            Email = "";
            ImagePath = "";
        }

        private clsPeople(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,DateTime DateOfBirth,
           byte Gender, string Address, string Phone, string Email, string ImagePath, int NationalityCountryID)
        {
            _Mode = enMode.Update;

            this.NationalNo = NationalNo;
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThridName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;
            this.NationalityCountryID = NationalityCountryID;
        }


        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    {
                        _Mode = enMode.Update;
                        return _AddNewPerson();
                    }
                case enMode.Update:
                    {
                        return _UpdateMode();
                    }
            }
            return false;
        }

        public static DataTable GetAllPeoples()
        {
            return clsPeopleData.GetAllPeoples();
        }

        public static bool DeletePerson(int PeronID)
        {
            return clsPeopleData.DeletePersonByID(PeronID);
        }

        public static clsPeople FindByID(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", ImagePath = "",
                Address = "", Phone = "", Email = "", NationalNo = "";
            int NationalityCountryID = 0;
            byte Gender = 0;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleData.FindPeopleByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth,
           ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth,
            Gender, Address, Phone, Email, ImagePath, NationalityCountryID);
            }

            else
                return null;
        }

        public static clsPeople FindByNationalNo(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", ImagePath = "",
                Address = "", Phone = "", Email = "";
            int NationalityCountryID = 0, PersonID = 0;
            byte Gender = 0;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleData.FindPeopleByNationalNo(ref PersonID, NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth,
           ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth,
            Gender, Address, Phone, Email, ImagePath, NationalityCountryID);
            }

            else
                return null;
        }

        public static bool PersonIsExist(string NationalNo)
        {
            return clsPeopleData.PersonIsExist(NationalNo);
        }


        public static string GetFullName(int PersonID)
        {
            return clsPeopleData.GetFullName(PersonID);
        }
    }
}
