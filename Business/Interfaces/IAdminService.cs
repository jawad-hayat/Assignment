using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAdminService
    {
        Task<bool> CreateRole(string name);
        Task<IEnumerable<IdentityRole>> GetRoles();
        Task<IdentityRole> GetRoleByID(string id);
        Task<bool> UpdateRole(string id, string newName);
        Task<bool> DeleteRole(string id);
    }
}
