using System.Threading.Tasks;
using vega.Core;
using vega.Core.Models;

namespace vega.Persistence
{
    public class ModelRepository : IModelRepository
    {
        public VegaDbContext context { get; }
        public ModelRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<Model> GetModelAsync(int id)
        {
            return await context.Models.FindAsync(id);
        }
    }
}