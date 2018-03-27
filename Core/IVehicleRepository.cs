using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id,bool includeRelated);
        void AddVehilce(Vehicle vehicle);

        void RemoveVehicle(Vehicle vehicle);
    }
}