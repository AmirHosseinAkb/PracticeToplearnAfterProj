using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;
using TopLearn.Core.Convertors;

namespace TopLearn.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public EditUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService= userService;
            _permissionService= permissionService;
        }
        public void GetInformations(int userId)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            ViewData["UserRoles"] = _permissionService.GetUserRoleIds(userId);
        }
        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }
        public void OnGet(int userId)
        {
            GetInformations(userId);
            EditUserViewModel=_userService.GetUserForEditInAdmin(userId);
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            var user=_userService.GetUserById(EditUserViewModel.UserId);
            if (!ModelState.IsValid)
            {
                GetInformations(EditUserViewModel.UserId);
                return Page();
            }
            if (EditUserViewModel.UserName != user.UserName)
            {
                if (_userService.IsExistUserByUserName(EditUserViewModel.UserName))
                {
                    GetInformations(EditUserViewModel.UserId);
                    ViewData["ExistUserName"] = true;
                    return Page();
                }
            }
            if (user.Email != EmailConvertor.FixEmail(EditUserViewModel.Email))
            {
                if (_userService.IsExistUserByEmail(EditUserViewModel.Email))
                {
                    GetInformations(EditUserViewModel.UserId);
                    ViewData["ExistEmail"] = true;
                    return Page();
                }
            }
            //Edit USer
            _userService.EditUserFromAdmin(EditUserViewModel);
            //Edit User Roles
            _permissionService.EditUserRoles(selectedRoles, EditUserViewModel.UserId);

            return RedirectToPage("Index");
        }
    }
}
