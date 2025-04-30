namespace CRUDify_API.Entities
{
    public class UserRoom
    {
        //UserID - RoomID composit PKEY
        public Guid UserId { get; set; } 

        public Guid RoomId { get; set; } 

        //public User User { get; set; }
        //public Room Room { get; set; }
    }
}
