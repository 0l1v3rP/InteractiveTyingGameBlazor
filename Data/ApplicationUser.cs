using InteractiveTyingGameBlazor.DbModels;
using Microsoft.AspNetCore.Identity;

namespace InteractiveTyingGameBlazor.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser, IEntityWithId
    {
    }

}
