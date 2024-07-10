using Infrastructure.Models.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITokenService
    {
        Task<AuthResponse> GenerateJwtToken(IdentityUser user);

    }
}
