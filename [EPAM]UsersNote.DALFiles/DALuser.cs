using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UsersNote.Entites;
using _EPAM_UsersNote.Interfaces.DAL;


namespace _EPAM_UsersNote.DALFiles
{
    public class DALuser : IUsersNoteUserDAL 
    {
        private static List<User> userlist = new List<User>();
        private static string usersPath = ConfigurationManager.AppSettings["UsersPath"];
        private static string usersFoto = ConfigurationManager.AppSettings["UsersFoto"];
        private static string[] readUs; 

        public DALuser()
        {
            if (File.Exists(usersPath))
            {
                userlist.Clear();
                readUs = File.ReadAllLines(usersPath);
                for (int i = 0; i < readUs.Length; i++)
                {
                    string[] userData = readUs[i].Split(',');
                    User user = new User(userData[0], DateTime.Parse(userData[1]));
                    user.Id = Guid.Parse(userData[2]);
                    user.FilePath = userData[3];
                    int length = userData.Length;
                    if (length > 4)
                        {
                            for (int j = 4; j < length; j++)
                            {
                                user.Awards.Add(userData[j]);
                            }
                        }
                    userlist.Add(user);
                 }
                 
            }
       }

        public void WriteUsers()
        {
            FileStream str1 = File.Create(usersPath);
            str1.Close();

            using (StreamWriter write = File.AppendText(usersPath))
            {
                foreach (var item in userlist)
                {
                    if (item.Awards.Count() != 0)
                    {
                        StringBuilder str = new StringBuilder(item.Name + "," + item.DateofBirth + "," + item.Id + "," + item.FilePath);
                        foreach (var award in item.Awards)
                        {
                            str.Append(",").Append(award);
                        }

                        write.WriteLine(str.ToString());
                    }
                    else
                    {
                        write.WriteLine("{0},{1},{2},{3}", item.Name, item.DateofBirth, item.Id, item.FilePath);
                    }
                }
            }
        }
        
        public bool DeleteUser(Guid id)
        {
            User x = userlist.FirstOrDefault(user => user.Id == id);
            if (x == null)
            {
                return false;
            }

            userlist.Remove(x);
            return true;
        }

        public User GetUser(Guid id)
        {
           return userlist.FirstOrDefault(user => user.Id == id);
        }

        public IEnumerable<User> GetAllUsers() 
        {
            foreach (var item in userlist)
            {
                yield return item;
            }
        }

        public IEnumerable<string> GetAllUserAwards(User user)
        {
            foreach (var item in user.Awards)
            {
                yield return item;
            }
        }

        public User CreateUser(User user)
        {
            userlist.Add(user);
            return userlist[userlist.Count - 1];
        }

        private static void ParseUser(string str)
        {
            string[] userData = str.Split(',');
        }

        public void DeleteAwardInUser(User user, Award award)
        {
            user.Awards.RemoveAll(name => name == award.Title);
        }

        public void AddPictureForUser(User user, string path, byte[] foto)
        {
            User x = userlist.FirstOrDefault(user1 => user1.Id == user.Id);
            
            if (foto.Length > 0)
            {
                string[] patharr = path.Split('#');
                File.WriteAllBytes(usersFoto + patharr[0], foto);
                x.FilePath = path;
            }
            else
            {
                x.FilePath = path;
            }
        }

        public byte[] GetUserPicture(User user)
        {

            string[] pathArr = user.FilePath.Split('#');
            if (pathArr[0] == "user.png")
            {
                byte[] ImageContent = File.ReadAllBytes(usersFoto + "user.png");
                return ImageContent;
            }
            else
            {
                string path = usersFoto + pathArr[0];
                if (File.Exists(path))
                {
                    byte[] ImageContent = File.ReadAllBytes(usersFoto + pathArr[0]);
                    return ImageContent;
                }
                else
                {
                    return new byte[0];
                }
            }
            
        }

        public void SetUserName(User user, string name)
        {
            user.Name = name;
        }

        public void SetUserBirthday(User user, DateTime birthday)
        {
            user.DateofBirth = birthday;
        }

        public bool ToAwardUser(User user, Award award)
        {
            user.Awards.Add(award.Title);
            return true;
        }
    }
}
