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
    public class DALaward : IUsersNoteAwardDAL
    {
        
        
        public IEnumerable<Award> GetAllAwards()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            Award award;
            string title = null;
            Guid guid = new Guid();
            string image = null;
            List<Award> awards = new List<Award>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetAwardItems = connection.CreateCommand();

                cmdGetAwardItems.CommandText = $"SELECT Guid, Title, AwardFotoPath FROM dbo.Awards";
                try
                {
                    connection.Open();
                    var reader = cmdGetAwardItems.ExecuteReader();
                    while (reader.Read())
                    {
                        string guidStr = reader["Guid"] as string;
                        Guid.TryParse(guidStr, out guid);
                        title = reader["Title"] as string;
                        image = reader["AwardFotoPath"] as string;
                        if (title != "8ddafe85-cf71-4265-b061-934834d605d7")
                        {
                            award = new Award(title);
                            award.Id = guid;
                            award.awardFotoPath = image;
                            awards.Add(award);
                        }
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }
            return awards;
        }

        public Award GetAward(Guid id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            Award award;
            string title = null;
            Guid guid = new Guid();
            string image = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetAwardItems = connection.CreateCommand();

                cmdGetAwardItems.CommandText = $"SELECT Guid, Title, AwardFotoPath FROM dbo.Awards WHERE Guid = @guid";
                cmdGetAwardItems.Parameters.AddWithValue("@guid", id);
                try
                {
                    connection.Open();
                    var reader = cmdGetAwardItems.ExecuteReader();
                    while (reader.Read())
                    {
                        string guidStr = (string) reader["Guid"];
                        Guid.TryParse((string) reader["Guid"], out guid);
                        title = reader["Title"] as string;
                        image = reader["AwardFotoPath"] as string;
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            award = new Award(title);
            award.Id = guid;
            award.awardFotoPath = image;
            return award;
        }

        public Award GetAward(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            Award award;
            string title = null;
            Guid guid = new Guid();
            string image = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetAwardItems = connection.CreateCommand();
                
                cmdGetAwardItems.CommandText = $"SELECT Guid, Title, AwardFotoPath FROM dbo.Awards WHERE Title = @title";
                cmdGetAwardItems.Parameters.AddWithValue("@title", name);
                try
                {
                    connection.Open();
                    var reader = cmdGetAwardItems.ExecuteReader();
                    while (reader.Read())
                    {
                        string guidStr = (string) reader["Guid"];
                        Guid.TryParse((string) reader["Guid"], out guid);
                        title = (string) reader["Title"];
                        image = reader["AwardFotoPath"] as string;
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            award = new Award(title);
            award.Id = guid;
            award.awardFotoPath = image;
            return award;
        }

        public Award CreateAward(Award award)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdAwardInsert = connection.CreateCommand();
                cmdAwardInsert.CommandText = $"INSERT INTO dbo.Awards(Guid, Title) VALUES(@guid, @title)";
                cmdAwardInsert.Parameters.AddWithValue("@guid", award.Id);
                cmdAwardInsert.Parameters.AddWithValue("@title", award.Title);
                try
                {
                    connection.Open();
                    cmdAwardInsert.ExecuteNonQuery();
                    return award;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool DeleteAward(Guid id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdDelUserAward = connection.CreateCommand();
                cmdDelUserAward.CommandText = $"DELETE FROM dbo.UsersInNote WHERE Award_Id = @guid";
                cmdDelUserAward.Parameters.AddWithValue("@guid", id);

                var cmdDelAward = connection.CreateCommand();
                cmdDelAward.CommandText = $"DELETE FROM dbo.Awards WHERE Guid = @guid";
                cmdDelAward.Parameters.AddWithValue("@guid", id);

                try
                {
                    connection.Open();
                    int y = cmdDelUserAward.ExecuteNonQuery();
                    int i = cmdDelAward.ExecuteNonQuery();
                    if (i + y > 0)
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

        public void WriteAwards()
        {
            
        }

        public void AddPictureForAward(Award award, string path, byte[] content)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            byte[] contentDefault = null;
            if (content.Length > 0)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var cmdSetAwardTitle = connection.CreateCommand();
                    cmdSetAwardTitle.CommandText =
                        $"UPDATE dbo.Awards SET AwardFotoPath = @path, Content = @content WHERE Guid = @Guid";
                    cmdSetAwardTitle.Parameters.AddWithValue("@path", path);
                    cmdSetAwardTitle.Parameters.AddWithValue("@content", content);
                    cmdSetAwardTitle.Parameters.AddWithValue("@Guid", award.Id);
                    try
                    {
                        connection.Open();
                        cmdSetAwardTitle.ExecuteNonQuery();
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
                    var cmdGetDefaultImage = connection.CreateCommand();
                    cmdGetDefaultImage.CommandText = $"SELECT Content FROM dbo.Awards WHERE Guid = @guid";
                    cmdGetDefaultImage.Parameters.AddWithValue("@guid", "8ddafe85-cf71-4265-b061-934834d605d7");
                    try
                    {
                        connection.Open();
                        var reader = cmdGetDefaultImage.ExecuteReader();
                        while (reader.Read())
                        {
                            contentDefault = (byte[])reader["Content"];
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
                    var cmdSetAwardTitle = connection.CreateCommand();
                    cmdSetAwardTitle.CommandText =
                        $"UPDATE dbo.Awards SET AwardFotoPath = @path, Content = @content WHERE Guid = @Guid";
                    cmdSetAwardTitle.Parameters.AddWithValue("@content", contentDefault);
                    cmdSetAwardTitle.Parameters.AddWithValue("@Guid", award.Id);
                    cmdSetAwardTitle.Parameters.AddWithValue("@path", path);
                    try
                    {
                        connection.Open();
                        cmdSetAwardTitle.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }

            }
        }

        public void SetAwardTitle(Award award, string title)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdSetAwardTitle = connection.CreateCommand();
                cmdSetAwardTitle.CommandText = $"UPDATE dbo.Awards SET Title = @title WHERE Guid = @Guid";
                cmdSetAwardTitle.Parameters.AddWithValue("@title", title);
                cmdSetAwardTitle.Parameters.AddWithValue("@Guid", award.Id);
                try
                {
                    connection.Open();
                    cmdSetAwardTitle.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public byte[] GetAwardPicture(Award award)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["defaul"].ConnectionString;
            byte[] content = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var cmdGetAwardFile = connection.CreateCommand();
                cmdGetAwardFile.CommandText = $"SELECT Content FROM dbo.Awards WHERE Guid = @guid";
                cmdGetAwardFile.Parameters.AddWithValue("@guid", award.Id);
                try
                {
                    connection.Open();
                    var reader = cmdGetAwardFile.ExecuteReader();
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
