using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.Course
{
    public class CourseStatus
    {
        [Key]
        public int StatusId { get; set; }
        [Display(Name = "عنوان وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string StatusTitle { get; set; }

        public bool IsDeleted { get; set; }
        //Relations
        public List<Course> Courses { get; set; }
    }
}
