using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;
using TopLearn.Data.Entities.User;
using TopLearn.Core.Convertors;
using TopLearn.Core.Generators;
using TopLearn.Core.Security;

namespace TopLearn.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public CreateUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_userService.IsExistUserByUserName(CreateUserViewModel.UserName))
            {
                ViewData["IsExistUserName"] = true;
                return Page();
            }
            if (_userService.IsExistUserByEmail(CreateUserViewModel.Email))
            {
                ViewData["IsExistEmail"] = true;
                return Page();
            }
            //Add USer
            int userId=_userService.AddUserFromAdmin(CreateUserViewModel);
            //Add User Roles
            _permissionService.AddUserRoles(selectedRoles, userId);
            return RedirectToPage("Index");
        }
    }
}
