using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Groups
{
    public class EditGroupModel : PageModel
    {
        private ICourseService _courseService;
        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseGroup CourseGroup { get; set; }
        public void OnGet(int groupId)
        {
            CourseGroup=_courseService.GetGroupById(groupId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.UpdateGroup(CourseGroup);
            return RedirectToPage("Index");
        }
    }
}
