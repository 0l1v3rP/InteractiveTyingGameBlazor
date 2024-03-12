using Microsoft.EntityFrameworkCore;
using InteractiveTyingGameBlazor.DbModels;
using Microsoft.AspNetCore.Components.Authorization;

namespace InteractiveTyingGameBlazor.Data.Services
{
	public class SharedService<T>(ApplicationDbContext dbContext, AuthenticationStateProvider auth) where T : BaseEntity
	{
		protected readonly ApplicationDbContext _dbContext = dbContext;
		protected readonly AuthenticationStateProvider _auth = auth;
		
        public void Add(T entity)
		{
			//_dbContext.Set<T>().Add(entity);
			//_dbContext.SaveChanges();
		}

		public void Update(T entity)
		{
            var existingEntity = _dbContext.Set<T>().Find(entity.Id);
            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();
        }

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			_dbContext.SaveChanges();
		}
	}
}
