using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UsersNote.Entites;

namespace _EPAM_UsersNote.Interfaces.DAL
{
    public interface IUsersNoteAwardDAL
    {
        IEnumerable<Award> GetAllAwards();

        Award GetAward(Guid id);

        Award GetAward(string name);

        Award CreateAward(Award award);

        bool DeleteAward(Guid id);

        void WriteAwards();

        void AddPictureForAward(Award award, string path, byte[] foto);

        void SetAwardTitle(Award award, string title);

        byte[] GetAwardPicture(Award award);
    }
}
