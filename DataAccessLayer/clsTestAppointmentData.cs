using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsTestAppointmentData
    {
        public static int AddNewTestAppointment(int TestTypeID, int LocalLicenseID, DateTime AppointmentDate, 
            float PaidFees, int CreatedByUserID, byte IsLocked)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO TestAppointments
           (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked)
     VALUES
           (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked)

		   select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalLicenseID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
                }
            }
            catch(Exception ex)
            {
                //asd
            }
            finally
            {
                connection.Close();
            }
            return TestAppointmentID;
        }

        public static bool IsTestAppointmentActiveByLocalLicenseID(int LocalLicenseID)
        {
            bool isActice = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select Found = 1 from TestAppointments
                    where LocalDrivingLicenseApplicationID = @LocalLicenseID and IsLocked = 0;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    isActice = true;
                }
            }
            catch (Exception ex)
            {
                //ASD
            }
            finally
            {
                connection.Close();
            }
            return isActice;
        }

        public static bool IsTestAppointmentActiveByTestAppointmentID(int TestAppointmentID)
        {
            bool isActice = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select IsLocked from TestAppointments
                              where TestAppointmentID = @TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    isActice = Convert.ToBoolean(result);
                }
            }
            catch (Exception ex)
            {
                //ASD
            }
            finally
            {
                connection.Close();
            }
            return isActice;
        }

        public static int GetTheNumberOfTests(int LocalLicenseID, byte TestTypeID)
        {
            byte NumberOfTests = 0;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select Count(*) from TestAppointments
                where LocalDrivingLicenseApplicationID = @LocalLicenseID and TestTypeID = @TestTypeID and IsLocked = 1 ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte Count))
                {
                    NumberOfTests = Count;
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
            return NumberOfTests;
        }

        public static bool UpdateAppointment(int TestAppointmentID, int TestTypeID, int LocalLicenseID, DateTime AppointmentDate,
            float PaidFees, int CreatedByUserID, byte IsLocked)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE TestAppointments
                             SET TestTypeID = @TestTypeID
                                ,LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                                ,AppointmentDate = @AppointmentDate
                                ,PaidFees = @PaidFees
                                ,CreatedByUserID = @CreatedByUserID
                                ,IsLocked = @IsLocked
                             WHERE TestAppointmentID = @TestAppointmentID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalLicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //ads
            }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }

        public static bool FindTestAppontment(ref int TestAppointmentID, int TestTypeID, int LocalLicenseID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedUserByID, ref byte IsLocked)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from TestAppointments
                    where LocalDrivingLicenseApplicationID = @LocalLicenseID and TestTypeID = @TestTypeID ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedUserByID = (int)reader["CreatedByUserID"];
                    IsLocked = (byte)reader["IsLocked"];
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

        public static bool FindTestAppontmentByID(int TestAppointmentID, ref int TestTypeID, ref int LocalLicenseID,
          ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedUserByID, ref byte IsLocked)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from TestAppointments
                             where TestAppointmentID = @TestAppointmentID ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    LocalLicenseID = (int)reader["LocalDrivingLicenseApplicationID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedUserByID = (int)reader["CreatedByUserID"];
                    IsLocked = (byte)reader["IsLocked"];
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

        public static bool LockedTestAppointment(int TestAppointmentID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE TestAppointments
                             SET IsLocked = 1
                             WHERE TestAppointmentID = @TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //ad
            }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }

        public static bool TestAppointmentIsLocked(int TestAppointmentID)
        {
            bool isLocked = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);
            string query = @"select IsLocked from TestAppointments
                             where TestAppointmentID = @TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isLocked = Convert.ToBoolean(result);
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
            return isLocked;
        }

    }
}
