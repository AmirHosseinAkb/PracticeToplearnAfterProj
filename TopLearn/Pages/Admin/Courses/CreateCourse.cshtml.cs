using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class CreateCourseModel : PageModel
    {
        private ICourseService _courseService;
        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public void GetInformations()
        {
            var groups = _courseService.GetCourseGroups();
            ViewData["CourseGroups"] = new SelectList(groups, "Value", "Text");

            var subGroups = _courseService.GetSubGroupsOfCourseGroups(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text");

            var teachers = _courseService.GetCourseTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");

            var statuses = _courseService.GetCourseStatuses();
            ViewData["Statuses"] = new SelectList(statuses, "Value", "Text");

            var levels = _courseService.GetCourseLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");
        }
        [BindProperty]
        public Course Course { get; set; }
        public void OnGet()
        {
            GetInformations();
        }

        public IActionResult OnPost(IFormFile courseImage, IFormFile? courseDemoFile)
        {
            if(!ModelState.IsValid || !courseImage.IsImage())
            {
                GetInformations();
                return Page();
            }
            //AddCourse
            _courseService.AddCourse(Course, courseImage, courseDemoFile);
            return RedirectToPage("Index");
        }
    }
}
