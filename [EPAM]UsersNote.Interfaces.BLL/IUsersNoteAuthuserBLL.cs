using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UsersNote.Entites;

namespace _EPAM_UsersNote.Interfaces.BLL
{
    public interface IUsersNoteAuthuserBLL
    {
        void AddAuthUser(AuthUser user);

        void SetUserAsAdmin(string name);

        void SetUserAsUser(string name);

        string GetUserRole(string name);

        void WriteAuthUsers();

        Dictionary<string, string> GetAllAuthUsers();

    }
}
