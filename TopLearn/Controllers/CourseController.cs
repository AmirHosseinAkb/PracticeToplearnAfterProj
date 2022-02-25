using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IOrderService _orderService;
        public CourseController(ICourseService courseService, IOrderService orderService)
        {
            _courseService = courseService;
            _orderService = orderService;
        }
        public IActionResult Index(int pageId = 1, string filterCourseTitle = "", string getType = "", string orderByType = ""
            , int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 9)
        {
            ViewBag.Groups = _courseService.GetAllGroups();
            ViewBag.selected = selectedGroups;
            return View(_courseService.GetCourses(pageId, filterCourseTitle, getType, orderByType, startPrice, endPrice, selectedGroups, take));
        }

        [Route("ShowCourse/{courseId}")]
        public IActionResult ShowCourse(int courseId)
        {
            var course = _courseService.GetCourseForShow(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);

        }

        [Authorize]
        [Route("BuyCourse/{courseId}")]
        public IActionResult BuyCourse(int courseId)
        {
            int orderId = _orderService.AddOrder(User.Identity.Name, courseId);
            return Redirect("/ShowOrder/" + orderId);
        }

        [Route("DownloadEpisode/{episodeId}")]
        public IActionResult DownloadEpisode(int episodeId)
        {
            var episode = _courseService.GetEpisodeById(episodeId);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Course",
                "Episodes",
                episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;
            if (episode.IsFree)
            {
                Byte[] downloadFile = System.IO.File.ReadAllBytes(filePath);
                return File(downloadFile, "application/force-download", fileName);
            }
            if (User.Identity.IsAuthenticated && _courseService.IsUserHaveCourse(User.Identity.Name, episode.CourseId))
            {
                Byte[] downloadFile = System.IO.File.ReadAllBytes(filePath);
                return File(downloadFile, "application/force-download", fileName);
            }
            return Forbid();
        }
    }
}
