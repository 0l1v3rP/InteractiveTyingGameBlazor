using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace InteractiveTyingGameBlazor.Extensions
{
    public static class AuthExtensions
    {
        public static async Task<string?> GetUserIdAsync(this AuthenticationStateProvider provider)        
            => (await provider.GetAuthenticationStateAsync())
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  
    }
}
