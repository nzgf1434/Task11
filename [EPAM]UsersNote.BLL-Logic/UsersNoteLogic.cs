using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UsersNote.Entites;
using _EPAM_UsersNote.Interfaces.BLL;
using _EPAM_UsersNote.Interfaces.DAL;
using _EPAM_UsersNote.Logger;
using _EPAM_UsersNote.DALFiles;

namespace _EPAM_UsersNote.BLL_Logic
{
    public class UsersNoteLogic : IUserNoteBLL
    {
        string logPath = ConfigurationManager.AppSettings["LogPath"] ;
        string usersPath = ConfigurationManager.AppSettings["UsersPath"];

        private IUsersNoteUserDAL userDAL;
       
        public UsersNoteLogic()
        {
            string mode = ConfigurationManager.AppSettings["DataMode"];

            try
            {
                switch (mode)
                {

                    case "Files":
                    {
                        userDAL = new _EPAM_UsersNote.DALFiles.DALuser();
                    }
                    break;

                    case "Database":
                    {
                        userDAL = new _EPAM_UsersNote.DALDatabase.DALusers();
                    }
                    break;
                }
            }

            catch (ConfigurationFileException e)
            {
                throw e;
            }
        }

        public User CreateUser(User user)
        {
            try
            {
                return userDAL.CreateUser(user);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public bool DeleteUser(Guid id)
        {
            try
            {
                return userDAL.DeleteUser(id);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public IEnumerable<string> GetAllUserAwards(User user)
        {
            return userDAL.GetAllUserAwards(user).ToArray();
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return userDAL.GetAllUsers().ToArray();
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public User GetUser(Guid id)
        {
            try
            {
                return userDAL.GetUser(id);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public bool ToAwardUser(User user, Award award)
        {
            try
            {
                userDAL.ToAwardUser(user, award);
                return true;
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void WriteUsers()
        {
            try
            {
                userDAL.WriteUsers();
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void DeleteAwardInUser(User user, Award award)
        {
            try
            {
                userDAL.DeleteAwardInUser(user, award);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void AddPictureForUser(User user, string path, byte[] foto)
        {
            try
            {
                userDAL.AddPictureForUser(user, path, foto);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void SetUserName(User user, string name)
        {
            try
            {
                userDAL.SetUserName(user, name);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void SetUserBirthday(User user, DateTime birthday)
        {
            try
            {
                userDAL.SetUserBirthday(user, birthday);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public byte[] GetUserPicture(User user)
        {
            try
            {
               return userDAL.GetUserPicture(user);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }
        
    }
}

