using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UsersNote.Entites
{
    public class Award
    {
        public Award(string title)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public  string awardFotoPath { get; set; }
    }
}
