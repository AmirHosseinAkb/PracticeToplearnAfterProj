using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Components
{
    public class ShowGroupsMenuComponent:ViewComponent
    {
        private ICourseService _courseService;
        public ShowGroupsMenuComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var groups = _courseService.GetAllGroups();
             return await Task.FromResult(View("ShowGroupsMenuComponent",groups));
        }
    }
}
