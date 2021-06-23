using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApplication.Models
{
    public class WelcomeEmailModel
    {
        public string InstructorName { get; set; }

        public WelcomeEmailModel(string instructorName)
        {
            InstructorName = instructorName;
        }
    }
}
