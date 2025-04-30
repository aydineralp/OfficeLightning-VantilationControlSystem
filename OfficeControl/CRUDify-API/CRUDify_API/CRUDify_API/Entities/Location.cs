namespace CRUDify_API.Entities
{
    public class Location
    {
        public Guid LocationId { get; set; }  //pk
        public string LocationName { get; set; } = string.Empty;

        //public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
