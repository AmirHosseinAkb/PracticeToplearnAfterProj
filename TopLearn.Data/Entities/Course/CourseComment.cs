using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.Course
{
    public class CourseComment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(300)]
        public string Comment { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdminRead { get; set; }

        #region Relations

        public Course Course { get; set; }

        public User.User User { get; set; }
        #endregion
    }
}
