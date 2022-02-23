using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public ShowCoursesViewModel ShowCoursesViewModel { get; set; }
        public void OnGet(int pageId=1,string filterCourseTitle="")
        {
            ShowCoursesViewModel = _courseService.GetCoursesForShow(pageId,filterCourseTitle);
        }
    }
}
