namespace ReservationService.Domain.Models
{
    public class SteadyResource:Resource
    {
        public int? RoomId { get; set; }
        public Room Room { get; set; }
    }
}