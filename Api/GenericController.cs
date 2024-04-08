using AngleSharp.Dom;
using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace InteractiveTyingGameBlazor.Api
{
    public class GenericController<T>(ApplicationDbContext context) : ODataController where T : class, IEntityWithId
    {
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        [EnableQuery]
        public IQueryable<T> Get()
            => _dbSet;
     
        [EnableQuery]
        public SingleResult<T> Get([FromODataUri] string key)
            => SingleResult.Create(_dbSet.Where(e => e.Id == key));
    }
}