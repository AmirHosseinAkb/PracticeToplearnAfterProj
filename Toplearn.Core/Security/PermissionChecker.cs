using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Core.Security
{
    internal class PermissionChecker : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermissionService _permissionService;
        int _permissionId;
        public PermissionChecker(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService=(IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!_permissionService.IsUserHavePermission(context.HttpContext.User.Identity.Name, _permissionId))
                {
                    var redirect = new RedirectResult("/Login");
                }
            }
            else
            {
                var redirect = new RedirectResult("/Login");
            }
        }
    }
}
