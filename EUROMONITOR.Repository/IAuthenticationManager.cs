using EUROMONITOR.Model;
using EUROMONITOR.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EUROMONITOR.Repository
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserAuthenticationDto userAuth);
        Task<string> CreateToken();
        User GetAuthenticatedUserDetails();
    }
}
