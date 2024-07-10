using Infrastructure.Models.Request;
using Infrastructure.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LogInRequest request);
    }
}
