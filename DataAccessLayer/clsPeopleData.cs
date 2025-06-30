 using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;

namespace DataAccessLayer
{
    public class clsPeopleData
    {
        public static DataTable GetAllPeoples()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName,
                             People.ThirdName, People.LastName,
                             case People.Gendor
                             when 0 then 'Male'
                             when 1 then 'Female'
                             end as Gendor
                             , People.DateOfBirth, Countries.CountryName as Nationality, 
                             People.Phone, People.Email
                             FROM Countries INNER JOIN People 
                             ON Countries.CountryID = People.NationalityCountryID";

            SqlCommand command = new SqlCommand(query, connection);

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
                //sdfs
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
          byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO People
           (NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,
		   Gendor,Address,Phone,Email,NationalityCountryID,ImagePath)
     VALUES
           (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,
		   @Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath);
            
            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
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
            return PersonID;
        }


        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
          byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE People
                             SET NationalNo = @NationalNo
                                ,FirstName = @FirstName
                                ,SecondName = @SecondName
                                ,ThirdName = @ThirdName
                                ,LastName = @LastName
                                ,DateOfBirth = @DateOfBirth
                                ,Gendor = @Gendor
                                ,Address = @Address
                                ,Phone = @Phone
                                ,Email = @Email
                                ,NationalityCountryID = @NationalityCountryID
                                ,ImagePath = @ImagePath
                              WHERE PersonID = @PersonID;";

            SqlCommand command  = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //sdfs
            }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }

        public static bool FindPeopleByID(int PersonID,ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
         ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from People where PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    isFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToByte(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";
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

        public static bool FindPeopleByNationalNo(ref int PersonID, string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
         ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from People where NationalNo = @NationalNo;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToByte(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";
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


        public static bool DeletePersonByID(int PersonID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"Delete from People where PersonID = @PersonID;";
            
            
            SqlCommand command = new SqlCommand(query, connection);
          

            command.Parameters.AddWithValue("@PersonID", PersonID);
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

        public static bool PersonIsExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select Found = 1 from People where NationalNo = @NationalNo;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    isFound = true;
                else
                    isFound = false;
            }
            catch(Exception ex)
            {
                //sde
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }

        public static string GetFullName(int PersonID)
        {
            string FullName = "";

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select FullName = FirstName + ' ' + SecondName + ' ' + ThirdName + ' ' + 
                             LastName from People where PersonID = @PersonID;";

            SqlCommand command  =new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    FullName = result.ToString();
            }
            catch (Exception ex)
            {
                //ads
            }
            finally
            {
                connection.Close ();
            }
            return FullName;
        }

    }
}
