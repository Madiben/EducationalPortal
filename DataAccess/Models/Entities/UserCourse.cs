using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public class UserCourse
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public double CompletionPercentage { get; set; }

        public UserCourse()
        {

        }

        public override string ToString()
        {
            return $"Course Name: {Course.Name}, Completion Percentage: {CompletionPercentage}.\n";
        }

    }
}
