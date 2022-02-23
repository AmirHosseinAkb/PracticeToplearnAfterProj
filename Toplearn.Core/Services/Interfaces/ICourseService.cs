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
        #endregion
    }
}
