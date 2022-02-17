using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;

namespace TopLearn.Pages.Admin.Users
{
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;
        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public UserInformationsForShowViewModel UserInformations { get; set; }
        public void OnGet(int userId)
        {
            UserInformations=_userService.GetUserInformationsForShow(userId);
        }
        public IActionResult OnPost()
        {
            _userService.DeleteUser(UserInformations.UserName);
            return RedirectToPage("Index");
        }
    }
}
