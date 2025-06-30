using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestTypesData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select TestTypeID as ID, TestTypeTitle as Title, TestTypeDescription as Description, 
                             TestTypeFees as Fees from TestTypes; ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {
                //asda
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool UpdateTestType(int TestID, string TestTitle, string TestDescription, float TestFees)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE TestTypes
                             SET TestTypeTitle = @TestTypeTitle
                                ,TestTypeDescription = @TestTypeDescription
                                ,TestTypeFees = @TestTypeFees
                             WHERE TestTypeID = @TestID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestTypeDescription", TestDescription);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTitle);
            command.Parameters.AddWithValue("@TestTypeFees", TestFees);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //ads
            }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }

        public static bool FindTestType(int TestID, ref string TestTitle, ref string TestDescription, ref float TestFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from TestTypes where TestTypeID = @TestID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    TestTitle = (string)reader["TestTypeTitle"];
                    TestDescription = (string)reader["TestTypeDescription"];
                    TestFees = Convert.ToSingle(reader["TestTypeFees"]);
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
    
        public static byte GetCountPassedTests(int LocalLicenseID)
        {
            byte Count = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select Count(*) from PassedTests
                           where LocalDrivingLicenseApplicationID = @LocalLicenseID and TestResult = 1; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte count))
                    Count = count;
            }
            catch (Exception ex)
            {
                //asd
            }
            finally
            {
                connection.Close();
            }
            return Count;
        }
    }
}
