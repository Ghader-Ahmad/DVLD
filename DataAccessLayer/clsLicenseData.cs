using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer
{
    public class clsLicenseData
    {
        public static int IssueLicense(int ApplicationID, int DriverID,int LicenseClass,DateTime IssueDate,
            DateTime ExpirationDate, string Notes, float PaidFees, byte IsActive,byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO Licenses
           (ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,
		   PaidFees,IsActive,IssueReason,CreatedByUserID)
     VALUES
           (@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,
		   @PaidFees,@IsActive,@IssueReason,@CreatedByUserID);

          select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
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
            return LicenseID;
        }

        public static bool IsPersonHasLicense(int ApplicationID)
        {
            bool IsHas = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select Found = 1 from Licenses where ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsHas = true;
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
            return IsHas;
        }

        public static bool FindLicenseByApplicationID(ref int LicenseID, int ApplicationID, ref int DriverID, ref int LicenseClass,
         ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref byte IsActive,
         ref byte IssueReason, ref int CreatedUserByID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from Licenses
                             where ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];

                    else
                        Notes = "No Notes";

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedUserByID = (int)reader["CreatedUserByID"];
                }
            }
            catch(Exception ex)
            {
                //adsa
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool FindLicenseByLicenseID(int LicenseID,ref int ApplicationID, ref int DriverID, ref int LicenseClass,
       ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref byte IsActive,
       ref byte IssueReason, ref int CreatedUserByID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from Licenses
                             where LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];

                    else
                        Notes = "No Notes";

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedUserByID = (int)reader["CreatedUserByID"];
                }
            }
            catch (Exception ex)
            {
                //adsa
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllDriverLicensesByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select LicenseID, ApplicationID, ClassName, IssueDate, ExpirationDate, IsActive from LicensesInShortDetails
                             where DriverID = @DriverID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch(Exception ex)
            {
                //ada
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool LicenseIsActiveAndNotExpired(int LicenseID)
        {
            bool isActive = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select ExpirationDate, IsActive from Licenses
                             where LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();    

                if (reader.Read())
                {
                    DateTime ExpirationDate = (DateTime)reader["ExpirationDate"];
                    bool Active = Convert.ToBoolean(reader["IsActive"]);

                    isActive = Convert.ToBoolean((DateTime.Now > ExpirationDate) ? 0 : 1);

                    if (isActive == true)
                        isActive = Active;
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
            return isActive;
        }

        public static bool LicenseIsActive(int LicenseID)
        {
            bool isActive = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select IsActive from Licenses
                             where LicenseID = @LicenseID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null)
                {
                    isActive = Convert.ToBoolean(result);
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

            return isActive;
        }

        public static int DoesThieDriverHaveAnotherActiceLicenseOfTheSameClass(int DriverID, int LicenseClass)
        {
            int LicenseID = 0;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select LicenseID from Licenses
                             where DriverID = @DriverID and LicenseClass = @LicenseClass  and IsActive = 1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(), out int licenseID))
                {
                    LicenseID = licenseID;
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

            return LicenseID;
        }

        public static bool ModifyLicenseStatus(int LicenseID, bool IsActive)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE Licenses
                             SET IsActive = @IsActive
                             WHERE LicenseID = @LicenseID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@IsActive", IsActive);

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
            { connection.Close(); }

            return (rowAffected > 0);
        }

        public static bool LicenseIsFromClassesThree(int LicenseID)
        {
            bool isFromClassesThree = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select LicenseClass from Licenses
                             where LicenseID = @LicenseID  and LicenseClass = 3";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isFromClassesThree = Convert.ToBoolean(result);
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
            return isFromClassesThree;
            
        }


    }
}
