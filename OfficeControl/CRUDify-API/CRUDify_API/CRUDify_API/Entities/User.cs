namespace CRUDify_API.Entities
{
    public class User
    {
        public Guid UserId { get; set; }  //pk
        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string SurName { get; set; } = string.Empty;

        //public ICollection<UserRoom> UserRooms { get; set; } = new List<UserRoom>();
    }
}
