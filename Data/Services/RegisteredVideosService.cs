using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using InteractiveTyingGameBlazor.Extensions;
using AngleSharp.Dom;
using InteractiveTyingGameBlazor.DbModels;
using Microsoft.EntityFrameworkCore;

namespace InteractiveTyingGameBlazor.Data.Services
{
    public class RegisteredVideosService(ApplicationDbContext dbContext, AuthenticationStateProvider auth) : SharedService<RegisteredVideo>(dbContext, auth)
    {
		public async Task<IEnumerable<RegisteredVideo>> GetUserVideos()
		{
			var userId = await _auth.GetUserIdAsync();
            return _dbContext.RegisteredVideos.Where(i => i.UserId == userId);
		}

        public void UpdatePressedChars(string videoId, int correctChars, int pressedChars)
        {
            _dbContext.RegisteredVideos
            .Where(v => v.Id == videoId)
            .ExecuteUpdate(v =>
                v.SetProperty(v => v.CorrectChars, v => v.CorrectChars + correctChars)
                .SetProperty(v => v.PressedChars,v =>  v.PressedChars + pressedChars)
            );
        }
        public int GetAverageCPM(string videoId)
        {
            var results = _dbContext.TypingResults.Where(result => result.VideoId == videoId);
            return results.Any() ? (int)results.Average(result => result.CPM) : 0;
        }
        public async Task<IEnumerable<RegisteredVideo>> GetAvailableVideos()
        {
            var userId = await _auth.GetUserIdAsync();
            return _dbContext.RegisteredVideos.Where(i => i.UserId == userId || i.IsGlobal);
        }

        public void IncreaseCounter(string videoId)
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
                Delete(entity);

            return entityExists;
        }
    }
}
