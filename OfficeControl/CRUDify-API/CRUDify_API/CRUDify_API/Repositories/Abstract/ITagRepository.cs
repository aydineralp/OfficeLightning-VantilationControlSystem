using CRUDify_API.Entities;
using CRUDify_API.Entities.DTOs;

namespace CRUDify_API.Repositories.Abstract
{
    public interface ITagRepository
    {
        IEnumerable<TagDTO> GetAllTagDTOs();
        IEnumerable<Tag> GetAllTags();
        Task<IEnumerable<Tag>> GetTagsByRoomName(string roomName);
        Task<IEnumerable<Tag>> GetTagsByLocationName(string locationName);
    }
}
