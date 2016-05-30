using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UsersNote.Entites;
using _EPAM_UsersNote.Interfaces.BLL;
using _EPAM_UsersNote.Interfaces.DAL;
using System.Configuration;
using _EPAM_UsersNote.Logger;

namespace _EPAM_UsersNote.BLL_Logic
{
    public class UsersNoteAuthuserLogic : IUsersNoteAuthuserBLL
    {
        string logPath = ConfigurationManager.AppSettings["LogPath"];
        string pathForAuthusers = ConfigurationManager.AppSettings["AuthPath"];

        private IUsersNoteAuthusersDAL authuserDAL;

        public UsersNoteAuthuserLogic()
        {
            string mode = ConfigurationManager.AppSettings["DataMode"];

            try
            {
                switch (mode)
                {

                    case "Files":
                    {
                        authuserDAL = new _EPAM_UsersNote.DALFiles.DALauthUser();
                    }
                        break;

                    case "Database":
                        {
                            authuserDAL = new _EPAM_UsersNote.DALDatabase.DALauthUser();
                        }
                        break;
                }
            }

            catch (ConfigurationFileException e)
            {
                throw e;
            }
        }



        public void AddAuthUser(AuthUser user)
        {
            try
            {
                authuserDAL.AddAuthUser(user);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public string GetUserRole(string name)
        {
            try
            {
                return authuserDAL.GetUserRole(name);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void SetUserAsAdmin(string name)
        {
            try
            {
                authuserDAL.SetUserAsAdmin(name);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void SetUserAsUser(string name)
        {
            try
            {
                authuserDAL.SetUserAsUser(name);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void WriteAuthUsers()
        {
            try
            {
                authuserDAL.WriteAuthUsers();
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public Dictionary<string, string> GetAllAuthUsers()
        {
            try
            {
                return authuserDAL.GetAllAuthUsers();
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }
    }
}
