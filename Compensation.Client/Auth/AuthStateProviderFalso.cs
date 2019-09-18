using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Compensation.Client.Auth
{
    public class AuthStateProviderFalso : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new List<Claim> { 
                new Claim(ClaimTypes.Name,"Franklin Reveron")
            
            },"prueba");
            await Task.Delay(3000);
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));

        }
    }
}
