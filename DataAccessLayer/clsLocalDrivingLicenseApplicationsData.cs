using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsLocalDrivingLicenseApplicationsData : clsApplicationsData
    {

        public static int AddNewLocalLicense(int ApplicaionID, int ApplicationPersonID, DateTime ApplicationDate,
             byte ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            ApplicaionID = AddNewApplication(ApplicationPersonID, ApplicationDate,
            ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

            int LocalLicensID = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO LocalDrivingLicenseApplications
                            (ApplicationID,LicenseClassID)
                         VALUES (@ApplicationID, @LicenseClassID);
                         select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicaionID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LocalLicensID = insertedID;
                }
            }
            catch (Exception ex)
            {
                //sdf
            }
            finally
            {
                connection.Close();
            }

            return LocalLicensID;
        }

        public static bool UpdateLocalLicense(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate,
             byte ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LocalLicenseID, int LicenseClassID)
        {
            UpdateApplication(ApplicationID, ApplicationPersonID, ApplicationDate,
             ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE LocalDrivingLicenseApplications
                             SET ApplicationID = @ApplicationID
                                ,LicenseClassID = @LicenseClassID
                             WHERE LocalDrivingLicenseApplicationID = @LocalLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //afd
            }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }


        public static bool FindLocalLicenseByApplicationID(int ApplicationID, ref int ApplicationPersonID, ref DateTime ApplicationDate,
            ref byte ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees,
            ref int CreatedByUserID, ref int LocalLicenseID, ref int LicenseCLassID)
        {
            if (!FindApplicationByID(ApplicationID, ref ApplicationPersonID, ref ApplicationDate,
            ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
            ref CreatedByUserID))
                return false;

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from LocalDrivingLicenseApplications where ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    LocalLicenseID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseCLassID = (int)reader["LicenseClassID"];
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


        public static int ApplicationStatusIsNew(int ApplicationPersonID, int LicenseCLassID, int ApplicationStatus)
        {
            int ApplicationID = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"SELECT Applications.ApplicationID
                            FROM Applications INNER JOIN LocalDrivingLicenseApplications
                            ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            where ApplicationPersonID = @ApplicationPersonID
                            and ApplicationStatus = @ApplicationStatus and LicenseClassID = @LicenseCLassID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LicenseCLassID", LicenseCLassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int applicationID))
                {
                    ApplicationID = applicationID;
                }
            }
            catch (Exception ex)
            {
                //adf
            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }

        public static int ApplicationStatusIsCompleted(int ApplicationPersonID, int LicenseCLassID, int ApplicationStatus)
        {
            int ApplicationID = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"SELECT Applications.ApplicationID
                            FROM Applications INNER JOIN LocalDrivingLicenseApplications
                            ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            where ApplicationPersonID = @ApplicationPersonID
                            and ApplicationStatus = @ApplicationStatus and LicenseClassID=@LicenseCLassID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LicenseCLassID", LicenseCLassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int applicationID))
                {
                    ApplicationID = applicationID;
                }
            }
            catch (Exception ex)
            {
                //ads
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;

        }


        public static int GetApplicationIDFromLocalApplicationID(int LocalApplicationID)
        {
            int ApplicationID = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select ApplicationID from LocalDrivingLicenseApplications 
                where LocalDrivingLicenseApplicationID = @LocalApplicationID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalApplicationID", LocalApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int applicationID))
                {
                    ApplicationID = applicationID;
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

        public static DataTable GetDrivingLicenseApplicationInfo(int LocalLicenseID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select [L.D.L.AppID], [Driving Class], [Passed Tests] 
                            from LocalDrivingLicensesApplications where [L.D.L.AppID] = @LocalLicenseID ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

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

        public static bool FindLocalLicenseByLocalLicenseID(ref int ApplicationID, ref int ApplicationPersonID, ref DateTime ApplicationDate,
            ref byte ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees,
            ref int CreatedByUserID, int LocalLicenseID, ref int LicenseCLassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @LocalLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseCLassID = (int)reader["LicenseClassID"];
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

            if (!FindApplicationByID(ApplicationID, ref ApplicationPersonID, ref ApplicationDate,
           ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
           ref CreatedByUserID))
                return false;
            
            return isFound;
        }

        public static bool DeleteLocalLicenseApplication(int LocalLicenseID)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"Declare @ApplicationID  int
                            set @ApplicationID = (select ApplicationID from LocalDrivingLicenseApplications
                            where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID);
                            
                            delete from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
                            delete from Applications where ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalLicenseID);

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
    }
}