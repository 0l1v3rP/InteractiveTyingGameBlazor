using InteractiveTyingGameBlazor.DbModels;
using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.Authorization;

namespace InteractiveTyingGameBlazor.Api
{
    public class VideosController(ApplicationDbContext context) : GenericController<RegisteredVideo>(context) { }
    public class UserController(ApplicationDbContext context) : GenericController<ApplicationUser>(context) { }
    public class ResultsController(ApplicationDbContext context) : GenericController<TypingResult>(context) {
        private TypingResult? GetEntityByKey(string key)
            => _dbSet.FirstOrDefault(e => e.Id.Equals(key, StringComparison.OrdinalIgnoreCase));

        [HttpGet("api/Results/GetChars/{key}")]
        public IActionResult GetChars([FromRoute] string key)
            => GetEntityByKey(key) is TypingResult entity ? Ok(entity.CharsCollection) : NotFound();

        [HttpGet("api/Results/GetEntity/{key}")]
        public IActionResult GetEntity([FromRoute] string key)
            => GetEntityByKey(key) is TypingResult entity ? Ok(entity) : NotFound();
    }
}
