using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Core;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehiclerepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IModelRepository modelrepository;
        public VehiclesController(IMapper mapper, IVehicleRepository vehiclerepository, IUnitOfWork unitOfWork, IModelRepository modelrepository)
        {
            this.modelrepository = modelrepository;
            this.unitOfWork = unitOfWork;
            this.vehiclerepository = vehiclerepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await modelrepository.GetModelAsync(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid model id.");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);

            vehicle.LastUpdatedWhen = DateTime.Now;
            vehicle.LastUpdatedByUser = "GAURANG";

            vehiclerepository.AddVehilce(vehicle);

            await unitOfWork.CompleteAsync();

            vehicle = await vehiclerepository.GetVehicle(vehicle.Id, includeRelated: true);

            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await modelrepository.GetModelAsync(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid model id.");
                return BadRequest(ModelState);
            }

            var vehicle = await vehiclerepository.GetVehicle(id, includeRelated: true);
 
            if (vehicle == null)
                return NotFound();


            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);

            vehicle.LastUpdatedWhen = DateTime.Now;
            vehicle.LastUpdatedByUser = "GAURANG";

            await unitOfWork.CompleteAsync();

            vehicle = await vehiclerepository.GetVehicle(vehicle.Id,includeRelated:true);
            
            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehilce(int id)
        {
            var vehicle = await vehiclerepository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            vehiclerepository.RemoveVehicle(vehicle);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehilce(int id)
        {
            var vehicle = await vehiclerepository.GetVehicle(id, includeRelated: true);
            if (vehicle == null)
                return NotFound();

            var vehilceresource = Mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehilceresource);
        }

    }
}