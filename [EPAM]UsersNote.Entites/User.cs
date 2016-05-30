using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _EPAM_UsersNote.Entites
{
    public class User
    {
        private List<string> awards;
        private Guid id;
        private DateTime dateofbirth;
        private int age;

        public string FilePath { get; set; }

        public User(string name, DateTime dateofbirthday)
        {
            this.Name = name;
            dateofbirth = dateofbirthday;
            id = Guid.NewGuid();
            this.Awards = new List<string>();
            this.FilePath = null;
            if ((DateTime.Now.Month <= this.DateofBirth.Month) & (DateTime.Now.Day < this.DateofBirth.Day))
            {
                this.Age = DateTime.Now.Year - this.DateofBirth.Year - 1;
            }
            else
            {
                this.Age = DateTime.Now.Year - this.DateofBirth.Year;
            }
        }

        public List<string> Awards
        {
            get
            {
                return this.awards;
            }

            set
            {
                if (this.awards == null)
                {
                    this.awards = new List<string>();
                }
            }
        }

        public Guid Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name { get; set; }

        public DateTime DateofBirth
        {
            get { return dateofbirth; }

            set
            {
                if (value <= DateTime.Now)
                {
                    dateofbirth = value;
                }
            }
        }

        public int Age
        {
            get
            {
                if ((DateTime.Now.Month <= this.DateofBirth.Month) & (DateTime.Now.Day < this.DateofBirth.Day))
                {
                    return DateTime.Now.Year - this.DateofBirth.Year - 1;
                }
                else
                {
                    return DateTime.Now.Year - this.DateofBirth.Year;
                }
                
            }

            set
            {
                if (value > 0)
                {
                    age = value;
                }
            }
        }
    }
}
