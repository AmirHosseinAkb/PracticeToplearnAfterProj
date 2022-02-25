using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class EditEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public EditEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int episodeId)
        {
            CourseEpisode=_courseService.GetEpisodeById(episodeId);
        }
        public IActionResult OnPost(IFormFile? episodeFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(episodeFile != null)
            {
                if (_courseService.IsExistsEpisodeFile(episodeFile.FileName))
                {
                    ViewData["ExistFile"] = true;
                    return Page();
                }
            }
            _courseService.EditEpisode(CourseEpisode, episodeFile);

            return Redirect("/Admin/Courses/EpisodeIndex/"+CourseEpisode.CourseId);
        }
    }
}
