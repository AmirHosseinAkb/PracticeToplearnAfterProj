using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.Generators;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private TopLearnContext _context;
        public CourseService(TopLearnContext context)
        {
            _context = context;
        }

        public void AddCourse(Course course, IFormFile courseImage, IFormFile courseDemo)
        {
            course.CourseImageName = "NoPhoto.png";
            course.CreateDate = DateTime.Now;
            if (courseImage != null)
            {
                course.CourseImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(courseImage.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Images",
                    course.CourseImageName
                    );
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    courseImage.CopyTo(stream);
                }
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Thumbnails",
                    course.CourseImageName);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(imagePath, thumbPath, 300);
            }
            if (courseDemo != null)
            {
                course.CourseDemoFileName = NameGenerator.GenerateUniqName() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Demoes",
                    course.CourseDemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }
            UpdateCourse(course);
        }

        public void EditCourse(Course course, IFormFile courseImage, IFormFile courseDemoFile)
        {
            course.UpdateDate = DateTime.Now;
            if (courseImage != null)
            {
                string imagePath = "";
                string thumbPath = "";
                if (course.CourseImageName != "NoPhoto.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Images",
                        course.CourseImageName);
                    thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Thumbnails",
                        course.CourseImageName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    if (File.Exists(thumbPath))
                    {
                        File.Delete(thumbPath);
                    }
                }
                course.CourseImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(courseImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Images",
                        course.CourseImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    courseImage.CopyTo(stream);
                }
                thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Thumbnails",
                        course.CourseImageName);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(imagePath, thumbPath, 300);
            }

            if (courseDemoFile != null)
            {
                string demoPath = "";
                if (course.CourseDemoFileName != null)
                {
                    demoPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Demoes",
                        course.CourseDemoFileName);
                    if (File.Exists(demoPath))
                        File.Delete(demoPath);
                }
                course.CourseDemoFileName = NameGenerator.GenerateUniqName() + Path.GetExtension(courseDemoFile.FileName);
                demoPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Demoes",
                        course.CourseDemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemoFile.CopyTo(stream);
                }
            }
            UpdateCourse(course);
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public List<SelectListItem> GetCourseGroups()
        {
            return _context.CourseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetCourseLevels()
        {
            return _context.CourseLevels
                .Select(l => new SelectListItem()
                {
                    Text = l.LevelTitle,
                    Value = l.LevelId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetCourseStatuses()
        {
            return _context.CourseStatuses
                .Select(s => new SelectListItem()
                {
                    Text = s.StatusTitle,
                    Value = s.StatusId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetCourseTeachers()
        {
            return _context.UserRoles.Where(ur => ur.RoleId == 2)
                .Select(ur => ur.User)
                .Select(u => new SelectListItem()
                {
                    Text = u.UserName,
                    Value = u.UserId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupsOfCourseGroups(int groupId)
        {
            return _context.CourseGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }
    }
}
