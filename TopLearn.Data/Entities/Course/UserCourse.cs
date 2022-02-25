using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.Course
{
    public class UserCourse
    {
        [Key]
        public int UC_Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CourseId { get; set; }

        public User.User User { get; set; }
        public Course Course { get; set; }
    }
}
