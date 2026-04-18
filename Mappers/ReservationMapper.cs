using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;
using reservations_api.Models.Entities;

namespace reservations_api.Mappers
{
    public static class ReservationMapper
    {
        public static Reservation ToEntity(CreateReservationRequest request)
        {
            return new Reservation
            {
                Id = Guid.NewGuid(),
                ClassroomId = request.ClassroomId,
                ReservationDate = request.ReservationDate.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                ReservedBy = request.ReservedBy
            };
        }

        public static ReservationResponse ToResponse(Reservation reservation)
        {
            return new ReservationResponse
            {
                Id = reservation.Id,
                ClassroomId = reservation.ClassroomId,
                ReservationDate = reservation.ReservationDate,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
                ReservedBy = reservation.ReservedBy
            };
        }
    }
}
