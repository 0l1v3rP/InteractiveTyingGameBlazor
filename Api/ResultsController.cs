using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.DbModels;
using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;

namespace InteractiveTyingGameBlazor.Api
{
    public class ResultsController(ApplicationDbContext context) : GenericController<TypingResult>(context)
    {
    }
}
