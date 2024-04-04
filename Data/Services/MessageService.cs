using InteractiveTyingGameBlazor.DbModels;
using Microsoft.AspNetCore.Components.Authorization;

namespace InteractiveTyingGameBlazor.Data.Services
{
    public class MessageService(ApplicationDbContext dbContext, AuthenticationStateProvider auth) : SharedService<RegisteredVideo>(dbContext, auth)
    {
    }
}
