using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.DbModels;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Results;

namespace InteractiveTyingGameBlazor.Api
{
    public class VideosController(ApplicationDbContext context) : GenericController<RegisteredVideo>(context)
    {
        
    }
}
