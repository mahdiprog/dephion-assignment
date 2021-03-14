namespace ReservationService.Domain.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }
        public string Name { get; set; }
        public int ResourceTypeId { get; set; }
        public ResourceType Type { get; set; }
    }
}