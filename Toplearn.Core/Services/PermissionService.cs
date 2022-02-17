using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.User;
using TopLearn.Data.Context;
using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Entities.Permission;

namespace TopLearn.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private TopLearnContext _context;
        public PermissionService(TopLearnContext context)
        {
            _context = context;
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public void AddRolePermissions(List<int> permissionIds, int roleId)
        {
            foreach (int permissionId in permissionIds)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                });
            }
            _context.SaveChanges();
        }

        public void AddUserRoles(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }
            _context.SaveChanges();
        }

        public void DeleteRole(int roleId)
        {
            var role=GetRoleById(roleId);
            role.IsDeleted = true;
            _context.SaveChanges();
        }

        public void EditRolePermissions(List<int> permissionIds, int roleId)
        {
            _context.RolePermissions.Where(rp => rp.RoleId == roleId).ToList().ForEach(rp => _context.RolePermissions.Remove(rp));
            AddRolePermissions(permissionIds, roleId);
        }

        public void EditUserRoles(List<int> roleIds, int userId)
        {
            _context.UserRoles.Where(ur => ur.UserId == userId).ToList().ForEach(ur => _context.UserRoles.Remove(ur));
            foreach (int roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }
            _context.SaveChanges();
        }

        public List<Permission> GetAllPermissions()
        {
            return _context.Permissions.ToList();
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public Role GetRoleByIdNoTracking(int roleId)
        {
            return _context.Roles.AsNoTracking().SingleOrDefault(r=>r.RoleId==roleId);
        }

        public List<int> GetRolePermissionIds(int roleId)
        {
            return _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId).ToList();
        }

        public List<Role> GetRolesWithPermissionsForShow()
        {
            return _context.Roles.Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .ToList();
        }

        public List<int> GetUserRoleIds(int userId)
        {
            return _context.UserRoles.Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId).ToList();
        }

        public bool IsExistRoleByTitle(string roleTile)
        {
            return _context.Roles.Any(r => r.RoleTitle == roleTile);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }
    }
}
