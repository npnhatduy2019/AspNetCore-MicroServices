using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common
{
    public class RepositoryBaseAsync<T, K, TContext> : IRepositoryBaseAsync<T, K, TContext>
    where T : EntityBase<K>
    where TContext : DbContext
    {
        private readonly TContext context;
        private readonly IUnitOfWork<TContext> unitofwork;

        public RepositoryBaseAsync(TContext _context, IUnitOfWork<TContext> _unitofwork)
        {
            context = _context ?? throw new ArgumentNullException(nameof(context));
            unitofwork = _unitofwork ?? throw new ArgumentNullException(nameof(unitofwork));
        }

        
        

        public async Task<K> CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity.Id;
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
            return entities.Select(x => x.Id).ToList();
        }
         public Task UpdateAsync(T entity)
        {
            if(context.Entry(entity).State==EntityState.Unchanged)
                return Task.CompletedTask;
            T exist = context.Set<T>().Find(entity.Id);
            context.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public Task UpdateListAsync(IEnumerable<T> entities) => context.Set<T>().AddRangeAsync(entities);
        
        public Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

       

        public IQueryable<T> FindAll(bool trackChanges = false) => !trackChanges ? context.Set<T>().AsNoTracking() :
                                                                                    context.Set<T>();

        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool tranckChanges = false) => 
        !tranckChanges
        ? context.Set<T>().Where(expression).AsNoTracking()
        : context.Set<T>().Where(expression);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool tranckChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, tranckChanges);
            items = includeProperties.Aggregate(items, (current, p) =>
                        current.Include(p));

            return items;
        }

        public async Task<T?> GetByIdAsync(K id) => 
            await FindByCondition(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();

        public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
            await FindByCondition(x => x.Id.Equals(id),tranckChanges:false, includeProperties)
            .FirstOrDefaultAsync();

        public Task<IDbContextTransaction> BeginTransactionAsync() => context.Database.BeginTransactionAsync();
        public Task RollbackTransactionAsync() => context.Database.RollbackTransactionAsync();

        public Task<int> SaveChangeAsync() => unitofwork.CommitAsync();
       
         public async Task EndtransactionAsync()
        {
            await context.SaveChangesAsync();
            await context.Database.CommitTransactionAsync();
        }
    }
}