namespace CRUDify_API.Entities
{
    public class Room
    {
        public Guid RoomId { get; set; }  //pk
        public Guid LocationId { get; set; } //fk
        public string RoomName { get; set; } = string.Empty;

        //public Location Location { get; set; }

        //public ICollection<UserRoom> UserRooms { get; set; } = new List<UserRoom>();
    }
}
