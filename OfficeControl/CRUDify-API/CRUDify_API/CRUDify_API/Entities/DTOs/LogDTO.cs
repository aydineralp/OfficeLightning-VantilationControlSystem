namespace CRUDify_API.Entities.DTOs
{
    public class LogDTO
    {
        public string Activity { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int TagCode { get; set; }
    }
}
