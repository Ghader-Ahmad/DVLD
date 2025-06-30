using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{
    public class clsApplicationTypesData
    {
        public static DataTable GetApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select ApplicationTypeID as ID, ApplicationTypeTitle as Title, ApplicationFees as Fees
                            from ApplicationTypes ";

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
                //adsfa
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool UpdateApplicationType(int ApplicationID, string NewTitle, float NewFees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"update ApplicationTypes
                            set ApplicationTypeTitle = @NewTitle, ApplicationFees = @NewFees
                            where ApplicationTypeID = @ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NewTitle", NewTitle);
            command.Parameters.AddWithValue("@NewFees", NewFees);
            command.Parameters.AddWithValue("@ID", ApplicationID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //asda
            }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static bool FindApplication(int ID, ref string Title, ref float Fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from ApplicationTypes where ApplicationTypeID = @ID; ";

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    isFound = true;

                    Title = (string)reader["ApplicationTypeTitle"];
                    Fees = Convert.ToSingle(reader["ApplicationFees"]);
                }

            }
            catch(Exception ex)
            {
                //afd
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static float GetFeesByApplicationTypeID(int ApplicationTypeID)
        {
            float ApplicationTypeFees = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select ApplicationFees from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null)
                {
                    ApplicationTypeFees = Convert.ToSingle(result);
                }
            }
            catch(Exception ex)
            {
                //afd
            }
            finally
            {
                connection.Close();
            }

            return ApplicationTypeFees;
        }


        public static float GetFeesByApplicationTypeTitle(int ApplicationTypeID)
        {
            float ApplicationTypeFees = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select ApplicationFees from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();

                float result = Convert.ToSingle(command.ExecuteScalar());

                if (result != null)
                {
                    ApplicationTypeFees = result;
                }
            }
            catch (Exception ex)
            {
                //afd
            }
            finally
            {
                connection.Close();
            }

            return ApplicationTypeFees;
        }
    }
}
