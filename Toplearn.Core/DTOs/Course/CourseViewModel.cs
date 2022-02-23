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
    public class CourseInformationsViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string TeacherName { get; set; }
        public int EpisodeCount { get; set; }
        public string StatusTitle { get; set; }
    }
}
