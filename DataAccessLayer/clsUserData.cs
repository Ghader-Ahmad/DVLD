using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsUserData
    {
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"SELECT UserID, Users.PersonID, FirstName +' '+
                             SecondName + ' ' + ThirdName + ' ' + LastName as FullName,
                             UserName , IsActive
                             FROM People INNER JOIN Users 
                             ON People.PersonID = Users.PersonID; ";
            
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
                //asdasd
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetLoginInformation(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"SELECT Users.UserID, Users.UserName, Users.IsActive
                             FROM Users where PersonID = @PersonID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

            }
            catch (Exception ex)
            {
                //asfd
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int AddNewUser(int PersonID, string UserName, string Password, byte IsActive)
        {
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"INSERT INTO Users
                            (PersonID,UserName,Password,IsActive)
                            VALUES
                                (@PersonID, @UserName, @Password, @IsActive);
                            select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
            }
            catch (Exception ex)
            {
                //adsf
            }
            finally
            {
                connection.Close();
            }

            return UserID;
        }

        public static bool UpdateUser(int UserID, string UserName, string Password, byte IsActive)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"UPDATE Users
                             SET UserName = @UserName 
                                ,Password = @Password
                                ,IsActive = @IsActive
                                WHERE UserID = @UserID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUserExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select Found = 1 from Users where PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    isFound = true;

            }
            catch(Exception ex)
            {
                //asda
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool FindUserByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, 
            ref byte IsActive)
        {
            bool isFound = true;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from Users where PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = Convert.ToByte(reader["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //da
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool ChangePassword(int UserID, string NewPassword)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string qeury = @"update Users
                             set Password = @NewPassword
                             where UserID = @UserID;";

            SqlCommand command = new SqlCommand(qeury, connection);

            command.Parameters.AddWithValue("@NewPassword", NewPassword);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //asda
            }
            finally
            {
                connection.Close();
            }

            return (rowAffected > 0);
        }

        public static bool DeleteUser(int UserID)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"delete from Users where  UserID = @UserID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }

        public static bool FindUser(string UserName, string Password, ref int UserID, ref int PersonID, ref byte IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from Users where UserName = @UserName and Password = @Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = Convert.ToByte(reader["IsActive"]);
                }
            }
            catch(Exception ex)
            {
                //adfa
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }


        public static bool FindUserByUserID(ref string UserName, ref string Password, int UserID, ref int PersonID, ref byte IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDVLDSettings.connectionString);

            string query = @"select * from Users where UserID = @UserID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = Convert.ToByte(reader["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //adfa
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    }
}
