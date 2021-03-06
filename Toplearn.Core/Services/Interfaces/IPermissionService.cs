using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.Permission;
using TopLearn.Data.Entities.User;


namespace TopLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        List<Role> GetAllRoles();
        void AddUserRoles(List<int> roleIds, int userId);
        List<int> GetUserRoleIds(int userId);
        void EditUserRoles(List<int> roleIds, int userId);
        List<Role> GetRolesWithPermissionsForShow();
        List<Permission> GetAllPermissions();
        bool IsExistRoleByTitle(string roleTile);
        int AddRole(Role role);
        void AddRolePermissions(List<int> permissionIds, int roleId);
        Role GetRoleById(int roleId);
        Role GetRoleByIdNoTracking(int roleId);
        List<int> GetRolePermissionIds(int roleId);
        void UpdateRole(Role role);
        void EditRolePermissions(List<int>permissionIds,int roleId);
        void DeleteRole(int roleId);
        bool IsUserHavePermission(string userName, int permissionId);
        
    }
}
