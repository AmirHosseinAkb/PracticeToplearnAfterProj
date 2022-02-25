using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class CreateEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int courseId)
        {
            CourseEpisode = new CourseEpisode()
            {
                CourseId = courseId
            };
        }
        public IActionResult OnPost(IFormFile? episodeFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (episodeFile == null)
            {
                ViewData["NullFile"] = true;
                return Page();
            }
            if (_courseService.IsExistsEpisodeFile(episodeFile.FileName))
            {
                ViewData["ExistFile"] = true;
                return Page();
            }
            _courseService.AddEpisode(CourseEpisode, episodeFile);
            return Redirect("/Admin/Courses/EpisodeIndex/"+CourseEpisode.CourseId);
        }
    }
}
