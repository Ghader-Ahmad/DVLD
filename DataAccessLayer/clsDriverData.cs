using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsDriverData
    {
        public static DataTable GetListDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from ListDrivers";

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

        public static int PersonIsADriver(int PersonID)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select DriverID from Drivers
                            where PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int driverID))
                {
                    DriverID = driverID;
                }
            }
            catch (Exception ex)
            {
                //sad
            }
            finally
            {
                connection.Close();
            }
            return DriverID;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO Drivers
                             (PersonID,CreatedByUserID,CreatedDate)
                            VALUES
                                (@PersonID, @CreatedByUserID, @CreatedDate);
                            select SCOPE_IDENTITY(); ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }
            }
            catch (Exception ex)
            {
                //asd
            }
            finally
            { connection.Close(); }

            return DriverID;
        }

        public static int GetDriverID(int PersonID)
        {
            int DriverID = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select DriverID from Drivers
                            where PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();    

                if(result != null && int.TryParse(result.ToString(),out int driverID))
                {
                    DriverID = driverID;
                }
            }
            catch(Exception ex)
            {
                //adf
            }
            finally
            {
                connection.Close();
            }
            return DriverID;
        }

    }
}
