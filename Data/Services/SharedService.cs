using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq.Expressions;
using InteractiveTyingGameBlazor.DbModels;

namespace InteractiveTyingGameBlazor.Data.Services
{
    public class SharedService<T>(ApplicationDbContext dbContext, AuthenticationStateProvider auth) where T : BaseEntity
	{
		protected readonly ApplicationDbContext _dbContext = dbContext;
		protected readonly AuthenticationStateProvider _auth = auth;

        public IEnumerable<TResult> Read<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query)
        {
			return query(_dbContext.Set<T>()).ToList();
		}

        [Obsolete]
        public void UpdateProperty<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression, TProperty value)
        {
            var entityEntry = _dbContext.Attach(entity);
            entityEntry.Property(propertyExpression).CurrentValue = value;
            entityEntry.Property(propertyExpression).IsModified = true;
            _dbContext.SaveChanges();
        }

        public void Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			_dbContext.SaveChanges();
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
