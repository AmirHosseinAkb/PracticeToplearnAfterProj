using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;
using TopLearn.Core.DTOs.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class DeleteCourseModel : PageModel
    {
        private ICourseService _courseService;
        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService; 
        }
        [BindProperty]
        public CourseInformationsViewModel CourseInformations { get; set; }
        public void OnGet(int courseId)
        {
            CourseInformations=_courseService.GetCourseInformationsForShow(courseId);
        }

        public IActionResult OnPost()
        {
            _courseService.DeleteCourse(CourseInformations.CourseId);
            return RedirectToPage("Index");
        }
    }
}
