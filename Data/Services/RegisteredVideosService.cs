using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using InteractiveTyingGameBlazor.Extensions;
using InteractiveTyingGameBlazor.Models;
using AngleSharp.Dom;

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

        public void IncreaseCounter(Guid videoId)
        {
            var video = _dbContext.Set<RegisteredVideo>().Find(videoId);
            if (video != null)
            {
                video.Counter += 1;
                _dbContext.Set<RegisteredVideo>().Update(video);
                _dbContext.SaveChanges();
            }
        }

        public bool DeleteBasedOnOverview(VideoOverview overView)
        {

            var entity = _dbContext.RegisteredVideos.FirstOrDefault(i => i.Id == overView.VideoId);
            bool entityExists = entity is not null;
            if (entityExists)
            {
                Delete(entity);
            }
            return entityExists;
        }
    }
}
