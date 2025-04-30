namespace CRUDify_API.Entities
{
    public class Log
    {
        public Guid LogId { get; set; }  //pk
        public string Activity { get; set; } = string.Empty;
        public DateTime ActivityTime { get; set; }

        public Guid UserId { get; set; } //fk
        public Guid DeviceTag { get; set; } //fk
        public Guid TagId { get; set; }


        //public User User { get; set; }
        //public Tag Tag { get; set; }
    }
}
