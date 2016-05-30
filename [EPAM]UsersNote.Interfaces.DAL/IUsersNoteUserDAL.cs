using _EPAM_UsersNote.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UsersNote.Interfaces.DAL
{
    public interface IUsersNoteUserDAL
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(Guid id);

        User CreateUser(User user);

        bool DeleteUser(Guid id);

        IEnumerable<string> GetAllUserAwards(User user);

        void WriteUsers();

        void DeleteAwardInUser(User user, Award award);

        void AddPictureForUser(User user, string path, byte[] foto);

        void SetUserName(User user, string name);

        void SetUserBirthday(User user, DateTime birthday);

        bool ToAwardUser(User user, Award award);

        byte[] GetUserPicture(User user);
    }
}
