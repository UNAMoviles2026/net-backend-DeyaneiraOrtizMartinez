using System;

namespace reservations_api.DTOs.Responses
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public Guid ClassroomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ReservedBy { get; set; } = string.Empty;
    }
}
