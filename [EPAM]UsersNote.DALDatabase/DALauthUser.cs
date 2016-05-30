using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UsersNote.Entites;
using _EPAM_UsersNote.Interfaces.DAL;

namespace _EPAM_UsersNote.DALDatabase
{
    public class DALauthUser : IUsersNoteAuthusersDAL
    {
        public void AddAuthUser(AuthUser user)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdAddAuthUser = connection.CreateCommand();
                cmdAddAuthUser.CommandText = $"INSERT INTO dbo.AuthUsers(Name, Role) VALUES(@name, @role)";
                cmdAddAuthUser.Parameters.AddWithValue("@name", user.Name);
                cmdAddAuthUser.Parameters.AddWithValue("@role", user.Role);
                try
                {
                    connection.Open();
                    cmdAddAuthUser.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public Dictionary<string, string> GetAllAuthUsers()
        {
            Dictionary<string, string> usersIsAuth = new Dictionary<string, string>();

            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            string name = null;
            string role = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetAllAuthUsers = connection.CreateCommand();
                cmdGetAllAuthUsers.CommandText = $"SELECT Name, Role FROM dbo.AuthUsers";
                try
                {
                    connection.Open();
                    var reader = cmdGetAllAuthUsers.ExecuteReader();
                    while (reader.Read())
                    {
                        name = reader["Name"] as string;
                        role = reader["Role"] as string;
                        usersIsAuth.Add(name, role);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
              }
            return usersIsAuth;
        }

        public string GetUserRole(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            string role = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetUserRole = connection.CreateCommand();
                cmdGetUserRole.CommandText = $"SELECT Role FROM dbo.AuthUsers WHERE Name = @name";
                cmdGetUserRole.Parameters.AddWithValue("@name", name);
                try
                {
                    connection.Open();
                    var reader = cmdGetUserRole.ExecuteScalar();
                    role = reader as string;
                    return role;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void SetUserAsAdmin(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdSetUserAdmin = connection.CreateCommand();
                cmdSetUserAdmin.CommandText =
                    $"UPDATE dbo.AuthUsers SET Role = @role WHERE Name = @name";
                cmdSetUserAdmin.Parameters.AddWithValue("@role", "user:admin");
                cmdSetUserAdmin.Parameters.AddWithValue("@name", name);
                try
                {
                    connection.Open();
                    cmdSetUserAdmin.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void SetUserAsUser(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdSetUserAdmin = connection.CreateCommand();
                cmdSetUserAdmin.CommandText =
                    $"UPDATE dbo.AuthUsers SET Role = @role WHERE Name = @name";
                cmdSetUserAdmin.Parameters.AddWithValue("@role", "user");
                cmdSetUserAdmin.Parameters.AddWithValue("@name", name);
                try
                {
                    connection.Open();
                    cmdSetUserAdmin.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void WriteAuthUsers()
        {
            
        }
    }
}
