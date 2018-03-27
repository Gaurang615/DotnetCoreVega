using System.Threading.Tasks;
using vega.Core;

namespace vega.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public VegaDbContext Context { get; set; }
        public UnitOfWork(VegaDbContext context)
        {
            this.Context = context;

        }

        public async Task CompleteAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}