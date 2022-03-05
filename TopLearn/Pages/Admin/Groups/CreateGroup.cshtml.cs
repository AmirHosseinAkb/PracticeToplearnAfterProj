using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Groups
{
    public class CreateGroupModel : PageModel
    {
        private ICourseService _courseService;
        public CreateGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseGroup CourseGroup { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            //AddGroup
            _courseService.AddGroup(CourseGroup);
            return RedirectToPage("Index");
        }
    }
}
