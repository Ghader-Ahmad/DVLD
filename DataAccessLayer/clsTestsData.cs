using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestsData
    {
        public static bool HasThisTestBeenPassed(int LocalLicenseID, string TestType)
        {
            bool isPassed = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select TestResult from PassedTests 
            where LocalDrivingLicenseApplicationID = @LocalLicenseID and TestTypeTitle = @TestType and TestResult=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@TestType", TestType);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null)
                {
                    isPassed = Convert.ToBoolean(result);
                }
            }
            catch(Exception ex)
            {
                //dfa
            }
            finally
            {
                connection.Close();
            }
            return isPassed;
        }

        public static DataTable GetTestAppointmentsByID(int LocalLicenseID, int TestTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select TestAppointmentID, AppointmentDate, PaidFees, IsLocked from TestAppointments
            where LocalDrivingLicenseApplicationID = @LocalLicenseID and TestTypeID = @TestTypeID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);
            }
            catch(Exception ex)
            {
                //ad
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int AddAndSaveTest(int TestAppointmentID, int TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO Tests
           (TestAppointmentID,TestResult,Notes,CreatedByUserID)
            VALUES
            (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);

            select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if (Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }
            catch(Exception ex)
            {
                //ad
            }
            finally
            {
                connection.Close();
            }
            return TestID;
        }
    }
}
 