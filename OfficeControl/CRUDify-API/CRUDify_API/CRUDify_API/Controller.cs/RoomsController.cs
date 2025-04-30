using CRUDify_API.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDify_API.Controller.cs
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet("getroomsbylocationname")]

        public async Task<IActionResult> GetRoomsByLocationName(string locationName)
        {
            var result = await _roomRepository.GetRoomsByLocationName(locationName);
            return Ok(new
            {
                Data = result,
                Message = "Odalar lokasyon ismine göre getirildi"
            });
        }
    }
}
