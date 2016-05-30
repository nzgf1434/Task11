using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UsersNote.Entites;

namespace _EPAM_UsersNote.Interfaces.BLL
{
    public interface IUserNoteBLL
    {
        IEnumerable<string> GetAllUserAwards(User user);
        
        IEnumerable<User> GetAllUsers();

        User GetUser(Guid id);

        User CreateUser(User user);

        bool DeleteUser(Guid id);

        bool ToAwardUser(User user, Award award);

        void WriteUsers();

        void DeleteAwardInUser(User user, Award award);

        void AddPictureForUser(User user, string path, byte[] foto);

        void SetUserName(User user, string name);

        void SetUserBirthday(User user, DateTime birthday);

        byte[] GetUserPicture(User user);

        
    }
}
