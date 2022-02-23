using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class EditCourseModel : PageModel
    {
        private ICourseService _courseService;
        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public void GetInformations(Course course)
        {
            var groups = _courseService.GetCourseGroups();
            ViewData["CourseGroups"] = new SelectList(groups, "Value", "Text",Course.GroupId);

            var subGroups = _courseService.GetSubGroupsOfCourseGroups(course.GroupId);
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text",course.SubId);

            var teachers = _courseService.GetCourseTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text",course.TeacherId);

            var statuses = _courseService.GetCourseStatuses();
            ViewData["Statuses"] = new SelectList(statuses, "Value", "Text",Course.StatusId);

            var levels = _courseService.GetCourseLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text",Course.LevelId);
        }
        [BindProperty]
        public Course Course { get; set; }
        public void OnGet(int courseId)
        {
            Course = _courseService.GetCourseById(courseId);
            GetInformations(Course);
        }
        public IActionResult OnPost(IFormFile? courseImage,IFormFile? courseDemoFile)
        {
            if (!ModelState.IsValid)
            {
                GetInformations(Course);
                return Page();
            }
            if (!courseImage.IsImage())
            {
                ViewData["IsntImage"] = true;
                return Page();
            }
            //Edit Course
            _courseService.EditCourse(Course, courseImage, courseDemoFile);
            return RedirectToPage("Index");
        }
    }
}
