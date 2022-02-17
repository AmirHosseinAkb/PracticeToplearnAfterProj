using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.User;

namespace TopLearn.Pages.Admin.Roles
{
    public class CreateRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService=permissionService;
        }
        [BindProperty]
        public Role Role { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
        }

        public IActionResult OnPost(List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Permissions"] = _permissionService.GetAllPermissions();
                return Page();
            }
            if (_permissionService.IsExistRoleByTitle(Role.RoleTitle))
            {
                ViewData["Permissions"] = _permissionService.GetAllPermissions();
                ViewData["ExistRole"] = true;
                return Page();
            }
            //Add Role
            int roleId=_permissionService.AddRole(Role);

            //Add RolePermissions
            _permissionService.AddRolePermissions(selectedPermissions, roleId);
            return RedirectToPage("Index");

        }
    }
}
