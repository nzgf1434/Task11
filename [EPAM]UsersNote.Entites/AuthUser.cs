using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UsersNote.Entites
{
    public class AuthUser
    {
        public string Name { get; set; }
        public string Role { get; set; }

        public AuthUser(string name)
        {
            this.Name = name;
            this.Role = "user";
        }
    }
}
