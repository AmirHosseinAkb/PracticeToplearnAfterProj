using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;

namespace TopLearn.Pages.Admin.Users
{
    public class DeletedUsersListModel : PageModel
    {
        private IUserService _userService;
        public DeletedUsersListModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersForShowInAdminViewModel UsersForShowInAdminViewModel { get; set; }
        public void OnGet(int pageId=1,string filterUserName="",string filterEmail="")
        {
            UsersForShowInAdminViewModel=_userService.GetDeletedUsersForShowInAdmin(pageId,filterUserName,filterEmail);
        }
        public IActionResult OnPost(int userId)
        {
            _userService.ReturnFromDeletedUsers(userId);
            return RedirectToPage("Index");
        }
    }
}
