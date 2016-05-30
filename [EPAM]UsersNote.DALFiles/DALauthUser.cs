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
    public class DALauthUser : IUsersNoteAuthusersDAL
    {
        private Dictionary<string, string> usersIsAuth = new Dictionary<string, string>();
        private string pathForAuthusers = ConfigurationManager.AppSettings["AuthPath"];

        public DALauthUser()
        {
            UsersIsAuth.Clear();
            if (File.Exists(pathForAuthusers))
            {
                var readUs = File.ReadAllLines(pathForAuthusers);

                for (int i = 0; i < readUs.Length; i++)
                {
                    var str = readUs[i].Split(',');
                    UsersIsAuth.Add(str[0], str[1]);
                }
            }
        }

        public Dictionary<string, string> UsersIsAuth
        {
            get { return this.usersIsAuth; }
        }

        public Dictionary<string, string> GetAllAuthUsers()
        {
            return UsersIsAuth;
        } 

        public void AddAuthUser(AuthUser user)
        {
            usersIsAuth.Add(user.Name, user.Role);
        }

        public void SetUserAsAdmin(string name)
        {
            usersIsAuth[name] = "user:admin";
        }

        public void SetUserAsUser(string name)
        {
            usersIsAuth[name] = "user";
        }

        public string GetUserRole(string name)
        {
            return usersIsAuth[name];
        }

        public void WriteAuthUsers()
        {
            FileStream str1 = File.Create(pathForAuthusers);
            str1.Close();

            using (StreamWriter write = File.AppendText(pathForAuthusers))
            {
                foreach (var item in usersIsAuth)
                {
                    write.WriteLine("{0},{1}", item.Key, item.Value);
                }
            }
        }
    }
}
