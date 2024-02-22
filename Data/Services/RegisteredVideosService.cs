using InteractiveTyingGameBlazor.DbModels;
using Microsoft.AspNetCore.Components.Authorization;
using InteractiveTyingGameBlazor.Extensions;

namespace InteractiveTyingGameBlazor.Data.Services
{
    public class RegisteredVideosService(ApplicationDbContext dbContext, AuthenticationStateProvider auth) : SharedService<RegisteredVideo>(dbContext, auth)
    {
		public async Task<IEnumerable<RegisteredVideo>> GetUserVideos()
		{
			var userId = await _auth.GetUserIdAsync();
            return _dbContext.RegisteredVideos.Where(i => i.UserId == userId);
		}

        public async Task<IEnumerable<RegisteredVideo>> GetAvailableVideos()
        {
            var userId = await _auth.GetUserIdAsync();
            return _dbContext.RegisteredVideos.Where(i => i.UserId == userId || i.IsGlobal);
        }
    }
}
