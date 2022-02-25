using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class EpisodeIndexModel : PageModel
    {
        private ICourseService _courseService;
        public EpisodeIndexModel(ICourseService courseService)
        {
            _courseService = courseService; 
        }
        public List<CourseEpisode> CourseEpisodes { get; set; }
        public void OnGet(int courseId)
        {
            ViewData["CourseId"]=courseId;
            CourseEpisodes=_courseService.GetEpisodesOfCourse(courseId);
        }
    }
}
