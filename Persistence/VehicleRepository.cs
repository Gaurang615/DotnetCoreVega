using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Core.Models;

namespace vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        public VegaDbContext context { get; }
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated=true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(vf => vf.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }
        public void AddVehilce(Vehicle vehicle)
        {
            context.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}