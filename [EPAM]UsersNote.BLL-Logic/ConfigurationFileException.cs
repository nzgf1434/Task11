using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UsersNote.BLL_Logic
{
    class ConfigurationFileException : Exception
    {
        public ConfigurationFileException()
        {
            
        }

        public ConfigurationFileException(string message) : base(message)
        {
            
        }

        public ConfigurationFileException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
