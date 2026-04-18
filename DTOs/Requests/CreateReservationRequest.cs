using System;

namespace reservations_api.DTOs.Requests
{
    public class CreateReservationRequest
    {
        public Guid ClassroomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ReservedBy { get; set; } = string.Empty;
    }
}
