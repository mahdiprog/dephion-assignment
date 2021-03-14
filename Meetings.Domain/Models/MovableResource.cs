namespace ReservationService.Domain.Models
{
    public class MovableResource:Resource
    {
       
        public int? OfficeId { get; set; }
        public Office Office { get; set; }
    }
}