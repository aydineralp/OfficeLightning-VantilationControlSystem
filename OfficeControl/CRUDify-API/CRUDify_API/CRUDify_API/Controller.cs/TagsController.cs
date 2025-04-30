using CRUDify_API.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDify_API.Controller.cs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet("getalltagdetails")]

        public IActionResult GetTagDetails()
        {
            var result = _tagRepository.GetAllTagDTOs();
            return Ok(result);
        }

        [HttpGet("gettagsbyroomname")]

        public IActionResult GetTagsByRoomName(string roomName)
        {
            var result = _tagRepository.GetTagsByRoomName(roomName);
            if(result != null)
            {
                return Ok(new
                {
                    Data = result,
                    Success = true,
                    Message = "Tagler başarıyla oda ismine göre filtrelendi"
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("gettagsbylocationname")]
        public IActionResult GetTagsByLocationName(string locationName)
        {
            var result = _tagRepository.GetTagsByLocationName(locationName);
            if (result != null)
            {
                return Ok(new
                {
                    Data = result,
                    Success = true,
                    Message = "Tagler başarıyla lokasyon ismine göre filtrelendi"
                });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
