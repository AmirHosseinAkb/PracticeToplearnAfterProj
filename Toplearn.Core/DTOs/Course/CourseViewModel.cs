using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.Course
{
    public class ShowCoursesViewModel
    {
        public List<TopLearn.Data.Entities.Course.Course> Courses { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
