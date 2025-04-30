namespace CRUDify_API.Entities
{
    public class Tag
    {
        public Guid TagId { get; set; }  //pk
        public string TagCode { get; set; }
        public Guid DeviceTypeId { get; set; }   //fk
        public Guid RoomId { get; set; }  //fk

        //public DeviceType DeviceType { get; set; }

        //public Room Room { get; set; }
    }
}
