using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.DbModels;
using InteractiveTyingGameBlazor.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace InteractiveTyingGameBlazor.Api
{
    public static class Edm
    {
        public static IEdmModel Get()
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<RegisteredVideo>("Videos");
            builder.EntitySet<TypingResult>("Results");
            builder.EntitySet<ApplicationUser>("Users");
            return builder.GetEdmModel();
        }
    }
}
