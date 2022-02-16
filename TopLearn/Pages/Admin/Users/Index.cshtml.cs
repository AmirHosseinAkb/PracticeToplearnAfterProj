using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;

namespace TopLearn.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersForShowInAdminViewModel UsersForShowInAdminViewModel { get; set; }
        public void OnGet(int pageId=1,string filterUserName="",string filterEmail="")
        {
            UsersForShowInAdminViewModel = _userService.GetUsersForShowInAdmin(pageId,filterUserName,filterEmail);
        }
    }
}
