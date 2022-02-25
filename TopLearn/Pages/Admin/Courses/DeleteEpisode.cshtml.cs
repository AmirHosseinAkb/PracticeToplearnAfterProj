using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class DeleteEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public DeleteEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int episodeId)
        {
            CourseEpisode=_courseService.GetEpisodeById(episodeId);
        }

        public IActionResult OnPost()
        {
            _courseService.DeleteEpisode(CourseEpisode);
            return Redirect("/Admin/Courses/EpisodeIndex/" + CourseEpisode.CourseId);
        }
    }
}
