using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.Course;
using TopLearn.Core.DTOs.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Get Informations
        List<CourseGroup> GetAllGroups();
        List<SelectListItem> GetCourseGroups();
        List<SelectListItem> GetSubGroupsOfCourseGroups(int groupId);
        List<SelectListItem> GetCourseTeachers();
        List<SelectListItem> GetCourseStatuses();
        List<SelectListItem> GetCourseLevels();

        #endregion

        #region Course

        void AddCourse(Course course, IFormFile courseImage, IFormFile courseDemo);
        void UpdateCourse(Course course);
        Course GetCourseById(int courseId);
        void EditCourse(Course course,IFormFile courseImage,IFormFile courseDemoFile);
        ShowCoursesViewModel GetCoursesForShow(int pageId=1,string filterCourseTitle="");
        CourseInformationsViewModel GetCourseInformationsForShow(int courseId);
        void DeleteCourse(int courseId);
        List<CourseEpisode> GetEpisodesOfCourse(int courseId);
        Tuple<List<ShowCourseListItemViewModel>,int> GetCourses(int pageId = 1, string filterCourseTitle = "", string getType = "", string orderByType = ""
            , int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);
        Course GetCourseForShow(int courseId);
        #endregion

        #region Episode

        bool IsExistsEpisodeFile(string episodeFileName);
        void AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        CourseEpisode GetEpisodeById(int episodeId);
        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);
        void UpdateEpisode(CourseEpisode episode);
        void DeleteEpisode(CourseEpisode episode);

        #endregion

        #region UserCourse

        bool IsUserHaveCourse(string userName, int courseId);

        #endregion
    }
}
