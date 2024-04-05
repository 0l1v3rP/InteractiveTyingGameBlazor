using InteractiveTyingGameBlazor.DbModels;
using InteractiveTyingGameBlazor.Extensions;
using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
namespace InteractiveTyingGameBlazor.Data.Services
{
    public class TypingResultService(ApplicationDbContext dbContext, AuthenticationStateProvider auth) : SharedService<TypingResult>(dbContext, auth)
	{
        public async Task<IEnumerable<ChartModel<DateTime, int>>> GetUserDailyAverageCPMs()
        {
            var userId = await _auth.GetUserIdAsync();
            return _dbContext.TypingResults.Where(i => i.UserId == userId)
                .GroupBy(i => i.Date.Date)
                    .OrderBy(i => i.Key)
                        .Select(g => new ChartModel<DateTime, int>(
                            g.Key,
                            (int)g.Average(r => r.CPM)));
        }

        public DateOnly? GetUserLastGameDate(string userId)
        {
            var date = _dbContext.TypingResults.Where(result => result.UserId == userId)?
                .OrderByDescending(result => result.Date)?.FirstOrDefault()?.Date;
            return date is null ? null : DateOnly.FromDateTime(date.Value);

        }

        public int TotalPlayedGames(string userId)
           => _dbContext.TypingResults.Where(result => result.UserId == userId)?.Count() ?? 0;

        public IEnumerable<ChartModel<int,int>> GetAverageCPM()
        {
            return _dbContext.TypingResults
              .GroupBy(i => i.CPM)
                .Select(g => new ChartModel<int, int>(
                    g.Key,
                    g.Count()));

        }
    }
}
