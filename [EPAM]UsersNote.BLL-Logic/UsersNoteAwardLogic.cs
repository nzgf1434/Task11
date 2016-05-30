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
    public class UsersNoteAwardLogic : IUserNoteAwardBLL
    {
        string logPath = ConfigurationManager.AppSettings["LogPath"];
        string usersPath = ConfigurationManager.AppSettings["UsersPath"];

       private IUsersNoteAwardDAL awardDAL;

        public UsersNoteAwardLogic()
        {
            string mode = ConfigurationManager.AppSettings["DataMode"];

            try
            {
                switch (mode)
                {

                    case "Files":
                        {
                            awardDAL = new _EPAM_UsersNote.DALFiles.DALaward();
                        }
                        break;

                    case "Database":
                        {
                            awardDAL = new _EPAM_UsersNote.DALDatabase.DALaward();
                        }
                        break;
                }
            }

            catch (ConfigurationFileException e)
            {
                throw e;
            }
        }

        public Award CreateAward(Award award)
        {
            try
            {
                return awardDAL.CreateAward(award);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public bool DeleteAward(Guid id)
        {
            try
            {
                return awardDAL.DeleteAward(id);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);

                throw e;
            }
        }

        public IEnumerable<Award> GetAllAwards()
        {
            try
            {
                return awardDAL.GetAllAwards().ToArray();
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public Award GetAward(Guid id)
        {
            try
            {
                return awardDAL.GetAward(id);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public Award GetAward(string name)
        {
            try
            {
                return awardDAL.GetAward(name);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void WriteAwards()
        {
            try
            {
                awardDAL.WriteAwards();
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void AddPictureForAward(Award award, string path, byte[] foto)
        {
            try
            {
                awardDAL.AddPictureForAward(award, path, foto);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public void SetAwardTitle(Award award, string title)
        {
            try
            {
                awardDAL.SetAwardTitle(award, title);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }

        public byte[] GetAwardPicture(Award award)
        {
            try
            {
                return awardDAL.GetAwardPicture(award);
            }
            catch (Exception e)
            {
                Log.ToLog(logPath, e);
                throw e;
            }
        }
    }
}
