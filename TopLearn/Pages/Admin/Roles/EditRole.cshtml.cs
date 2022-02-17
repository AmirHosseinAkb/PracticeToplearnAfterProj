using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.User;

namespace TopLearn.Pages.Admin.Roles
{
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService=permissionService;
        }
        public void GetInformations(int roleId)
        {
            ViewData["RolePermissions"] = _permissionService.GetRolePermissionIds(roleId);
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
        }
        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int roleId)
        {
            GetInformations(roleId);
            Role=_permissionService.GetRoleById(roleId);
        }

        public IActionResult OnPost(List<int> selectedPermissions)
        {
            var role = _permissionService.GetRoleByIdNoTracking(Role.RoleId);
            if (!ModelState.IsValid)
            {
                GetInformations(Role.RoleId);
                return Page();
            }
            if (role.RoleTitle != Role.RoleTitle)
            {
                if (_permissionService.IsExistRoleByTitle(Role.RoleTitle))
                {
                    ViewData["ExistRole"] = true;
                    return Page();
                }
            }
            //Update Role
            _permissionService.UpdateRole(Role);
            //EditRolePermissions
            _permissionService.EditRolePermissions(selectedPermissions, Role.RoleId);

            return RedirectToPage("Index");
        }
    }
}
