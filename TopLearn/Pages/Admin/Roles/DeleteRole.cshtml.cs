using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.User;

namespace TopLearn.Pages.Admin.Roles
{
    public class DeleteRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int roleId)
        {
            Role=_permissionService.GetRoleById(roleId);
        }
        public IActionResult OnPost()
        {
            _permissionService.DeleteRole(Role.RoleId);
            return RedirectToPage("Index");
        }
    }
}
