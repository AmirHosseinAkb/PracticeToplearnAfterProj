﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.Course;

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
        #endregion
    }
}
