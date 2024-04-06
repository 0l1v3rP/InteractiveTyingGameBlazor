using InteractiveTyingGameBlazor.DbModels;
using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace InteractiveTyingGameBlazor.Data.Services
{
    public class MessageService(ApplicationDbContext dbContext, AuthenticationStateProvider auth) : SharedService<Message>(dbContext, auth)
    {
        public IEnumerable<Message> GetChatMessages(string userId, string recipientId)
            => _dbContext.Messages.Where(message => (message.SenderId == userId &&
                    message.RecipentId == recipientId) || (message.RecipentId == userId &&
                    message.SenderId == recipientId)).OrderBy(message => message.Timestamp);
    }
}
