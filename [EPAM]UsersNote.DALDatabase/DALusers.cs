using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using _EPAM_UsersNote.Entites;
using _EPAM_UsersNote.Interfaces.DAL;

namespace _EPAM_UsersNote.DALDatabase
{
    public class DALusers : IUsersNoteUserDAL
    {
        private static string userDefaultImg = ConfigurationManager.AppSettings["UserImg"];

        public IEnumerable<User> GetAllUsers()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            User user;
            string name = null;
            Guid guid = new Guid();
            string image = null;
            DateTime birthday = new DateTime();
            string award;
            List<User> users = new List<User>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetUsersItems = connection.CreateCommand();

                cmdGetUsersItems.CommandText = $"SELECT DISTINCT Guid, Name, Birthday, File_path FROM dbo.UsersInNote";
                connection.Open();
                var reader = cmdGetUsersItems.ExecuteReader();
                while (reader.Read())
                {
                    string guidStr = reader["Guid"] as string;
                    Guid.TryParse(guidStr, out guid);
                    name = reader["Name"] as string;
                    var date = reader["Birthday"] as string;
                    DateTime.TryParse(date, out birthday);
                    image = reader["File_path"] as string;
                    user = new User(name, birthday);
                    user.Id = guid;
                    user.FilePath = image;
                    users.Add(user);
                }
                reader.Close();
            }

            foreach (User item in users)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var cmdGetUserAwards = connection.CreateCommand();
                    cmdGetUserAwards.CommandText =
                        $"SELECT Title FROM dbo.Awards INNER JOIN dbo.UsersInNote ON dbo.UsersInNote.Guid = @id AND dbo.Awards.Guid = dbo.UsersInNote.Award_Id";
                    cmdGetUserAwards.Parameters.AddWithValue("id", item.Id);

                    connection.Open();
                    var reader1 = cmdGetUserAwards.ExecuteReader();
                    while (reader1.Read())
                    {
                        award = reader1["Title"] as string;
                        if (award != null)
                        {
                            item.Awards.Add(award);
                        }

                    }
                    reader1.Close();
                }
            }

            return users;
        }

        public User GetUser(Guid id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            User user;
            string name = null;
            string image = null;
            DateTime birthday = new DateTime();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetUser = connection.CreateCommand();

                cmdGetUser.CommandText = $"SELECT Name, Birthday, File_path FROM dbo.UsersInNote WHERE Guid = @guid";
                cmdGetUser.Parameters.AddWithValue("guid", id);
                connection.Open();
                var reader = cmdGetUser.ExecuteReader();
                while (reader.Read())
                {
                    name = reader["Name"] as string;
                    birthday = (DateTime)reader["Birthday"];
                    image = reader["File_path"] as string;
                }
                reader.Close();
            }
            user = new User(name, birthday);
            user.Id = id;
            user.FilePath = image;
            return user;
        }

        public User CreateUser(User user)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdUserInsert = connection.CreateCommand();
                cmdUserInsert.CommandText = $"INSERT INTO dbo.UsersInNote (Guid, Name, Birthday) VALUES(@guid, @name, @birthday)";
                cmdUserInsert.Parameters.AddWithValue("@guid", user.Id);
                cmdUserInsert.Parameters.AddWithValue("@name", user.Name);
                cmdUserInsert.Parameters.AddWithValue("@birthday", user.DateofBirth.ToString("yyyy-MM-dd"));
               
                try
                {
                    connection.Open();
                    cmdUserInsert.ExecuteNonQuery();
                    return user;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

       public bool DeleteUser(Guid id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdDelUser = connection.CreateCommand();
                cmdDelUser.CommandText = $"DELETE FROM dbo.UsersInNote WHERE Guid = @guid";
                cmdDelUser.Parameters.AddWithValue("@guid", id);

                try
                {
                    connection.Open();
                    int i = cmdDelUser.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }
        }

        public IEnumerable<string> GetAllUserAwards(User user)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            List<string> awards = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetUsersAwards = connection.CreateCommand();

                cmdGetUsersAwards.CommandText = $"SELECT Title FROM dbo.Awards INNER JOIN dbo.UsersInNote ON dbo.UsersInNote.Guid = @id AND dbo.Awards.Guid = dbo.UsersInNote.Award_Id";
                cmdGetUsersAwards.Parameters.AddWithValue("@id", user.Id);
                connection.Open();
                var reader = cmdGetUsersAwards.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["Title"] as string;
                    awards.Add(name);
                }
                reader.Close();
            }

            return awards;
        }

        public void WriteUsers()
        {
            
        }

        public void DeleteAwardInUser(User user, Award award)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdSetUserName = connection.CreateCommand();
                cmdSetUserName.CommandText = $"DELETE FROM dbo.UsersInNote WHERE Guid = @userGuid AND Award_Id = @awardId";
                cmdSetUserName.Parameters.AddWithValue("@userGuid", user.Id);
                cmdSetUserName.Parameters.AddWithValue("@awardId", award.Id);
                try
                {
                    connection.Open();
                    cmdSetUserName.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void AddPictureForUser(User user, string path, byte[] foto)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            byte[] fotoUser = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdSetUserName = connection.CreateCommand();
                cmdSetUserName.CommandText = $"UPDATE dbo.UsersInNote SET File_path = @path WHERE Guid = @Guid";
                cmdSetUserName.Parameters.AddWithValue("@path", path);
                cmdSetUserName.Parameters.AddWithValue("@Guid", user.Id);
                try
                {
                    connection.Open();
                    cmdSetUserName.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            
                if (foto.Length > 0)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                            var cmdUserPictureInsert = connection.CreateCommand();
                            cmdUserPictureInsert.CommandText =
                                $"INSERT INTO dbo.UserImage (Guid, Content) VALUES(@guid, @content)";
                            cmdUserPictureInsert.Parameters.AddWithValue("@guid", user.Id);
                            cmdUserPictureInsert.Parameters.AddWithValue("@content", foto);
                            try
                            {
                                connection.Open();
                                cmdUserPictureInsert.ExecuteNonQuery();
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                    }
                }
            
                else
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        var cmdUserGetDefaultImage = connection.CreateCommand();
                        cmdUserGetDefaultImage.CommandText =
                            $"SELECT Content FROM dbo.UserImage WHERE Guid = @guid";
                        cmdUserGetDefaultImage.Parameters.AddWithValue("@guid", "c1055e6b-0581-4195-9bad-a80b53a2b911");
                        var cmdUserPictureInsert = connection.CreateCommand();
                        try
                        {
                            connection.Open();
                            var reader = cmdUserGetDefaultImage.ExecuteReader();
                            while (reader.Read())
                            {
                                fotoUser = (byte[]) reader["Content"];
                            }
                            reader.Close();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                    using (var connection = new SqlConnection(connectionString))
                    {
                        var cmdUserPictureInsert = connection.CreateCommand();
                        cmdUserPictureInsert.CommandText =
                            $"INSERT INTO dbo.UserImage (Guid, Content) VALUES(@guid, @content)";
                        cmdUserPictureInsert.Parameters.AddWithValue("@guid", user.Id);
                        cmdUserPictureInsert.Parameters.AddWithValue("@content", fotoUser);
                        try
                        {
                            connection.Open();
                            cmdUserPictureInsert.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
        }

        public void SetUserName(User user, string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdSetUserName = connection.CreateCommand();
                cmdSetUserName.CommandText = $"UPDATE dbo.UsersInNote SET Name = @name WHERE Guid = @Guid";
                cmdSetUserName.Parameters.AddWithValue("@name", name);
                cmdSetUserName.Parameters.AddWithValue("@Guid", user.Id);
                try
                {
                    connection.Open();
                    cmdSetUserName.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void SetUserBirthday(User user, DateTime birthday)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdSetUserName = connection.CreateCommand();
                cmdSetUserName.CommandText = $"UPDATE dbo.UsersInNote SET Birthday = @birthday WHERE Guid = @Guid";
                cmdSetUserName.Parameters.AddWithValue("@birthday", birthday.ToString("yyyy-MM-dd"));
                cmdSetUserName.Parameters.AddWithValue("@Guid", user.Id);
                try
                {
                    connection.Open();
                    cmdSetUserName.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool ToAwardUser(User user, Award award)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            user.Awards.Add(award.Title);
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdUserInsert = connection.CreateCommand();
                cmdUserInsert.CommandText =
                    $"INSERT INTO dbo.UsersInNote (Guid, Name, Birthday, File_path, Award_Id) VALUES(@guid, @name, @birthday, @image, @award_id)";
                cmdUserInsert.Parameters.AddWithValue("@guid", user.Id);
                cmdUserInsert.Parameters.AddWithValue("@name", user.Name);
                cmdUserInsert.Parameters.AddWithValue("@birthday", user.DateofBirth.ToString("yyyy-MM-dd"));
                cmdUserInsert.Parameters.AddWithValue("@image", user.FilePath);
                cmdUserInsert.Parameters.AddWithValue("@award_id", award.Id);
                try
                {
                    connection.Open();
                    return cmdUserInsert.ExecuteNonQuery() > 0;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public byte[] GetUserPicture(User user)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            byte[] content = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetUserPicture = connection.CreateCommand();

                cmdGetUserPicture.CommandText = $"SELECT Content FROM dbo.UserImage WHERE Guid = @guid";
                cmdGetUserPicture.Parameters.AddWithValue("guid", user.Id);
                try
                {
                    connection.Open();
                    var reader = cmdGetUserPicture.ExecuteReader();
                    while (reader.Read())
                    {
                        content = (byte[]) reader["Content"];
                    }
                    reader.Close();
                    return content;
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }
        }
    }
}
