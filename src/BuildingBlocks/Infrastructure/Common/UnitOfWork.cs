using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public class UnitOfWok<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext context;

        public UnitOfWok(TContext _context)
        {
            context = _context;
        }

        public Task<int> CommitAsync() => context.SaveChangesAsync();

        public void Dispose() => context.Dispose();
    }
}