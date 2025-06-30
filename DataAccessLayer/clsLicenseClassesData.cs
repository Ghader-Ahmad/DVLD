﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsLicenseClassesData
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select ClassName from LicenseClasses; ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);
            }
            catch (Exception ex)
            {
                //ad
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool FindLicenseClassByName(ref int LicenseClassID, string ClassName, ref string ClassDescription,
           ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from LicenseClasses where ClassName = @ClassName;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                }
            }
            catch (Exception ex)
            {
                //asd
            }

            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool FindLicenseClassByID(int LicenseClassID,ref string ClassName, ref string ClassDescription,
          ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from LicenseClasses where LicenseClassID = @LicenseClassID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                }
            }
            catch (Exception ex)
            {
                //asd
            }

            finally
            {
                connection.Close();
            }

            return isFound;
        }


    }
}

