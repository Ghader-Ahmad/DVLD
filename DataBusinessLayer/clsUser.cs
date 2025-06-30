using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class clsUser
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
        }
        
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName {  get; set; }
        public string Password { get; set; }
        public byte IsActive { get; set; }

        public clsUser()
        {
            _Mode = enMode.AddNew;

            UserID = 0;
            PersonID = 0;
            UserName = "";
            IsActive = 0;
        }

        public clsUser(int userID, int personID, string userName, string password, byte isActive)
        {
            _Mode = enMode.Update;

            UserID = userID;
            PersonID = personID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }

        
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static DataTable GetLoginInformation(int PersonID)
        {
            return clsUserData.GetLoginInformation(PersonID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    {
                        return _AddNewUser();
                        break;
                    }
                    case enMode.Update:
                    {
                        return _UpdateUser();
                        break;
                    }
            }
            return false;
        }

        public static bool IsUserExist(int PersonID)
        {
            return clsUserData.IsUserExist(PersonID);
        }

        public static clsUser FindUserByPersonID(int PersonID)
        {
            int UserID = 0;
            string UserName = "", Password = "";
            byte IsActive = 0;

            if (clsUserData.FindUserByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }
    
        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return clsUserData.ChangePassword(UserID, NewPassword); 
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static clsUser FindUser(string UserName, string Password)
        {
            int PersonID = 0, UserID = 0;
            byte IsActive = 0;

            if(clsUserData.FindUser(UserName, Password, ref UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindUserByUserID(int UserID)
        {
            string UserName = "", Password = "";
            int PersonID = 0;
            byte IsActive = 0;

            if (clsUserData.FindUserByUserID(ref UserName, ref Password, UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
    }
}
