using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace InteractiveTyingGameBlazor.Extensions
{
    public static class AuthExtensions
    {
        public static async Task<string?> GetUserIdAsync(this AuthenticationStateProvider provider)
        {
            var authState = await provider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
