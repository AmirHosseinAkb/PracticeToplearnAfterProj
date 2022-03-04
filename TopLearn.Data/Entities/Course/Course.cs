using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.Course
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public int? SubId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int LevelId { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }

        public string? CourseDemoFileName { get; set; }

        public string? CourseImageName { get; set; }

        [Display(Name = "کلمات کلیدی دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string CourseTags { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsDeleted { get; set; }


        #region Relations

        [ForeignKey("GroupId")]
        public CourseGroup? CourseGroup { get; set; }
        [ForeignKey("SubId")]
        public CourseGroup? SubGroup { get; set; }
        [ForeignKey("TeacherId")]
        public User.User? User { get; set; }
        [ForeignKey("StatusId")]
        public CourseStatus? CourseStatus { get; set; }
        [ForeignKey("LevelId")]
        public CourseLevel? CourseLevel { get; set; }
        public List<CourseEpisode>? CourseEpisodes { get; set; }
        public List<Order.OrderDetail> OrderDetails { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<CourseComment> CourseComments { get; set; }

        #endregion
    }
}
