using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AdminService : IAdminService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> CreateRole(string name)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = name
            };
            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await Task.FromResult(_roleManager.Roles.ToList());
        }

        public async Task<IdentityRole> GetRoleByID(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<bool> UpdateRole(string id, string newName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return false;
            }

            role.Name = newName;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return false;
            }

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }
    }
}
