using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsInternationalLicensesData
    {
        public static DataTable GetAllDriverInternationalLicensesByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select InternationalLicenseID, ApplicationID, IssuedUsingLocalLicenseID,IssueDate,
                            ExpirationDate, IsActive from InternationalLicenses
                            where DriverID = @DriverID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

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
                //asd
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,IssueDate,
                            ExpirationDate, IsActive from InternationalLicenses; ";

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
                //asd
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int InternationalLicenseIsExistByDriverID(int DriverID)
        {
            int InternationalLicenseID = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select InternationalLicenseID from InternationalLicenses
                             where DriverID = @DriverID
                             order by ApplicationID desc; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int internationalLicenseID))
                {
                    InternationalLicenseID = internationalLicenseID;
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

            return InternationalLicenseID;
        }

        public static bool InternationalLicenseIsActive(int InternationalLicenseID)
        {
            bool isAvtive = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select ExpirationDate from InternationalLicenses
                             where InternationalLicenseID = @InternationalLicenseID ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
            
                if(result != null)
                {
                    isAvtive = Convert.ToBoolean(Convert.ToDateTime(result) > DateTime.Now);
                }
            }
            catch(Exception ex)
            {
                //asdf
            }
            finally
            {
                connection.Close();
            }
            return isAvtive;
        }

        public static bool UpdateInternationalLicenseStatus(int InternationalLicenseID, bool IsActive)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE InternationalLicenses
                             SET IsActive = @IsActive
                             WHERE InternationalLicenseID = @InternationalLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool FindInternationalLicenseByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID,
            ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref byte IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from InternationalLicenses
                             where InternationalLicenseID = @InternationalLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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
            return isFound;
        }
    
    
        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, byte IsActive,int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO InternationalLicenses
           (ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive,CreatedByUserID)
            VALUES
              (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID)
            
            select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
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

            return InternationalLicenseID;

        }
    }
}
