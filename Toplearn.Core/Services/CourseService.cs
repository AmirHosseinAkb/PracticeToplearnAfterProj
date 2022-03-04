using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs.Course;
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

        public void AddCourseComment(CourseComment courseComment)
        {
            _context.CourseComments.Add(courseComment);
            _context.SaveChanges();
        }

        public void AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.EpisodeFileName = episodeFile.FileName;
            string episodePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Course",
                "Episodes",
                episode.EpisodeFileName);
            using (var stream = new FileStream(episodePath, FileMode.Create))
            {
                episodeFile.CopyTo(stream);
            }

            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();
        }

        public void DeleteCourse(int courseId)
        {
            var course = GetCourseById(courseId);
            course.IsDeleted = true;
            _context.SaveChanges();
        }

        public void DeleteEpisode(CourseEpisode episode)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Course",
                "Episodes",
                 episode.EpisodeFileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _context.CourseEpisodes.Remove(episode);
            _context.SaveChanges();
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

        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            string filePath = "";
            if (episodeFile != null)
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Episodes",
                    episode.EpisodeFileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                episode.EpisodeFileName = episodeFile.FileName;
                filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Episodes",
                    episode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }
            UpdateEpisode(episode);
        }

        public List<CourseGroup> GetAllGroups()
        {
            return _context.CourseGroups.Include(g=>g.CourseGroups).ToList();
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public Tuple<List<CourseComment>, int> GetCourseComments(int courseId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            var query = _context.CourseComments.Include(c=>c.User).Where(c => c.CourseId == courseId);
            int pageCount = query.Count() / take;
            if (query.Count() % take > 0)
            {
                pageCount++;
            }
            return Tuple.Create(query.Skip(skip).Take(take).ToList(), pageCount);
        }

        public Course GetCourseForShow(int courseId)
        {
            return _context.Courses
                .Include(c => c.CourseEpisodes)
                .Include(c => c.CourseLevel)
                .Include(c => c.CourseStatus)
                .Include(c => c.User)
                .Include(c=>c.UserCourses)
                .SingleOrDefault(c => c.CourseId == courseId);
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

        public CourseInformationsViewModel GetCourseInformationsForShow(int courseId)
        {
            return _context.Courses
                .Include(c => c.User)
                .Include(c => c.CourseStatus)
                .Include(c => c.CourseEpisodes)
                .Where(c => c.CourseId == courseId)
                .Select(c => new CourseInformationsViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    TeacherName = c.User.UserName,
                    EpisodeCount = c.CourseEpisodes.Count(),
                    StatusTitle = c.CourseStatus.StatusTitle
                }).Single();
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

        public Tuple<List<ShowCourseListItemViewModel>, int> GetCourses(int pageId = 1, string filterCourseTitle = "", string getType = "", string orderByType = "", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
            {
                take = 8;
            }
            IQueryable<Course> result = _context.Courses;
            if (!string.IsNullOrEmpty(filterCourseTitle))
            {
                result = result.Where(c => c.CourseTitle.Contains(filterCourseTitle) || c.CourseTags.Contains(filterCourseTitle));
            }
            switch (getType)
            {
                case "all":
                    break;
                case "free":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }
                case "buy":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }
            }
            switch (orderByType)
            {
                case "createDate":
                    {
                        result = result.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "updateDate":
                    {
                        result = result.OrderByDescending(c => c.UpdateDate);
                        break;
                    }
            }
            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice >= startPrice);
            }
            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice <= endPrice);
            }

            if (selectedGroups != null)
            {
                foreach (int groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubId==groupId);
                }
            }
            int skip = (pageId - 1) * take;
            var query= result.Include(c => c.CourseEpisodes).Skip(skip).Take(take)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    CourseImageName = c.CourseImageName,
                    CoursePrice = c.CoursePrice
                }).ToList();
            int pageCount = result.Count() / take;
            if (result.Count() % take != 0)
            {
                pageCount++;
            }
            return Tuple.Create(query,pageCount);
        }

        public ShowCoursesViewModel GetCoursesForShow(int pageId = 1, string filterCourseTitle = "")
        {
            IQueryable<Course> result = _context.Courses.Include(c => c.User).Include(c => c.CourseStatus).Include(c => c.CourseEpisodes);
            if (!string.IsNullOrEmpty(filterCourseTitle))
            {
                result = result.Where(r => r.CourseTitle.Contains(filterCourseTitle));
            }
            int take = 5;
            int skip = (pageId - 1) * take;

            var courseVm = new ShowCoursesViewModel()
            {
                Courses = result.Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };
            if (result.Count() % take != 0)
            {
                courseVm.PageCount++;
            }
            return courseVm;
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

        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }

        public List<CourseEpisode> GetEpisodesOfCourse(int courseId)
        {
            return _context.CourseEpisodes.Where(e => e.CourseId == courseId).ToList();
        }

        public List<ShowCourseListItemViewModel> GetPopularCourses()
        {
            return _context.Courses.Include(c => c.UserCourses)
                .Where(c=>c.OrderDetails.Any())
                .OrderByDescending(c => c.UserCourses.Count())
                .Take(8).Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    CourseImageName = c.CourseImageName,
                    CoursePrice = c.CoursePrice
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

        public bool IsExistsEpisodeFile(string episodeFileName)
        {
            return _context.CourseEpisodes.Any(e => e.EpisodeFileName == episodeFileName);
        }

        public bool IsUserHaveCourse(string userName, int courseId)
        {
            var userId = _context.Users.SingleOrDefault(u => u.UserName == userName).UserId;

            return _context.UserCourses.Any(uc => uc.UserId == userId && uc.CourseId == courseId);
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void UpdateEpisode(CourseEpisode episode)
        {
            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }
    }
}
