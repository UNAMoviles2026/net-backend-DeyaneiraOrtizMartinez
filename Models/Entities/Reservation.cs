using System;

namespace reservations_api.Models.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid ClassroomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ReservedBy { get; set; } = string.Empty;
    }
}
