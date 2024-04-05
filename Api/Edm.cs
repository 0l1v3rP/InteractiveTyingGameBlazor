using InteractiveTyingGameBlazor.DbModels;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InteractiveTyingGameBlazor.Api
{
    public static class Edm
    {
        public static IEdmModel Get()
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<RegisteredVideo>("Videos");
            builder.EntitySet<TypingResult>("Results");
            return builder.GetEdmModel();
        }
    }
}
