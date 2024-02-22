using InteractiveTyingGameBlazor.Extensions;
using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Client;
using System.Collections;
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
