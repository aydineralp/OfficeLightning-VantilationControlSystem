using CRUDify_API.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDify_API.Controller.cs
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        ILocationRepository _locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet("getalllocations")]

        public async Task<IActionResult> GetAllLocations()
        {
            var result = await _locationRepository.GetAllLocations();
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("asd");
            }
        }
    }
}
