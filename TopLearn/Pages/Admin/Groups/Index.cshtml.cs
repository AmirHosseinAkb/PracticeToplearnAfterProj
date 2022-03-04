using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Groups
{
    public class IndexModel : PageModel
    {
        private ICourseService _corseService;
        public IndexModel(ICourseService courseService)
        {
            _corseService = courseService;  
        }
        public List<CourseGroup> CourseGroups { get; set; }
        public void OnGet()
        {
            CourseGroups = _corseService.GetAllGroups();
        }
    }
}
