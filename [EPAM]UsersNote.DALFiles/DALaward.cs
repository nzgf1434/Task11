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
    public class DALaward : IUsersNoteAwardDAL
    {
        private static List<Award> awardlist = new List<Award>();
        private static string awardsPath = ConfigurationManager.AppSettings["AwardsPath"];
        private static string usersFoto = ConfigurationManager.AppSettings["UsersFoto"];
        private static string[] readUs;

        public DALaward()
        {
            awardlist.Clear();
            if (File.Exists(awardsPath))
            {
                readUs = File.ReadAllLines(awardsPath);
                for (int i = 0; i < readUs.Length; i++)
                {
                    string[] awardData = readUs[i].Split(',');
                    Award award = new Award(awardData[0]);
                    award.Id = Guid.Parse(awardData[1]);
                    if (awardData.Length > 2)
                    {
                        award.awardFotoPath = awardData[2];
                    }
                    awardlist.Add(award);
                }
            }
        }


        public Award CreateAward(Award award)
        {
            awardlist.Add(award);
            return awardlist[awardlist.Count-1];
        }

        public bool DeleteAward(Guid id)
        {
            Award x = awardlist.FirstOrDefault(award => award.Id == id);
            if (x == null)
            {
                return false;
            }

            awardlist.Remove(x);
            return true;
        }

        public IEnumerable<Award> GetAllAwards()
        {
            foreach (var item in awardlist)
            {
                yield return item;
            }
        }

        public Award GetAward(Guid id)
        {
            return awardlist.FirstOrDefault(award => award.Id == id);
        }

        public Award GetAward(string name)
        {
            return awardlist.FirstOrDefault(award => award.Title == name);
        }

        public void WriteAwards()
        {
            FileStream str1 = File.Create(awardsPath);
            str1.Close();

            using (StreamWriter write = File.AppendText(awardsPath))
            {
                foreach (var item in awardlist)
                {
                        write.WriteLine("{0},{1},{2}", item.Title, item.Id, item.awardFotoPath);
                }
            }
        }

        public void AddPictureForAward(Award award, string path, byte[] foto)
        {
            award.awardFotoPath = path;

            Award x = awardlist.FirstOrDefault(award1 => award1.Id == award.Id);

            if (foto.Length > 0)
            {
                string[] patharr = path.Split('#');
                File.WriteAllBytes(usersFoto + patharr[0], foto);
                x.awardFotoPath = path;
            }
            else
            {
                x.awardFotoPath = path;
            }
        }

        public void SetAwardTitle(Award award, string title)
        {
            award.Title = title;
        }

        public byte[] GetAwardPicture(Award award)
        {
            string[] pathArr = award.awardFotoPath.Split('#');
            if (pathArr[0] == "award.jpg")
            {
                byte[] ImageContent = File.ReadAllBytes(usersFoto + "award.jpg");
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
    }
}
