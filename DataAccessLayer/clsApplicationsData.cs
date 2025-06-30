using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsApplicationsData
    {
        public static DataTable GetLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from LocalDrivingLicensesApplications;";

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
                //asd
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int AddNewApplication(int ApplicationPersonID, DateTime ApplicationDate,
             byte ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO Applications
           (ApplicationPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID)
            VALUES
           (@ApplicationPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees,
            @CreatedByUserID);
            
            select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertesID))
                {
                    ApplicationID = insertesID;
                }
            }
            catch (Exception ex)
            {
                //ad
            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate,
             byte ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE Applications
                            SET ApplicationPersonID = @ApplicationPersonID
                               ,ApplicationDate = @ApplicationDate
                               ,ApplicationTypeID = @ApplicationTypeID
                               ,ApplicationStatus = @ApplicationStatus
                               ,LastStatusDate = @LastStatusDate
                               ,PaidFees = @PaidFees
                               ,CreatedByUserID = @CreatedByUserID
                            WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //asdf
            }
            finally
            {
                connection.Close();
            }

            return (rowAffected > 0);
        }

        public static bool FindApplicationByID(int ApplicationID, ref int ApplicationPersonID, ref DateTime ApplicationDate, 
            ref byte ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees,
            ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from Applications where ApplicationID = @ApplicationID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationPersonID = (int)reader["ApplicationPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = Convert.ToByte(reader["ApplicationTypeID"]);
                    ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
            }
            catch (Exception ex)
            {
                //asdf
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool EditApplicationStatus(int ApplicationID, byte ApplicationStatus)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"update Applications
                             set ApplicationStatus = @ApplicationStatus
                             where ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //asd
            }
            finally
            {
                connection.Close();
            }

            return (rowAffected > 0);
        }

        public static DataTable GetApplicationBasicInfo(int ApplicationID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from ApplicationBasicInfo where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);
            }
            catch(Exception ex)
            {
                //ads
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool FindApplication(ref int ApplicationID, int ApplicationPersonID, ref DateTime ApplicationDate,
            byte ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees,
            ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from Applications
                    where ApplicationPersonID =@ApplicationPersonID and ApplicationTypeID = @ApplicationTypeID
                    order by ApplicationID desc; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
            }
            catch (Exception ex)
            {
                //asdf
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool CompletedApplicationForRetakeTestIfApplicable(int ApplicationPersonID)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE Applications
                            SET
                               ApplicationStatus = 3 
                            WHERE ApplicationPersonID = @ApplicationPersonID and ApplicationTypeID = 7 and ApplicationStatus = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //ad
            }
            finally
            {
                connection.Close();
            }

            return (rowAffected > 0);
        }


        public static bool UpdateApplicationStatusToCompletedStatus(int ApplicationID)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE Applications
                            SET ApplicationStatus = 3
                            WHERE ApplicationID = @ApplicationID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //asda
            }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }
    }
}
