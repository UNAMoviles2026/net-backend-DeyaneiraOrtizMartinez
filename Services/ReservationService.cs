using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;
using reservations_api.Mappers;
using reservations_api.Repositories;

namespace reservations_api.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IClassroomRepository _classroomRepository;

        public ReservationService(IReservationRepository reservationRepository, IClassroomRepository classroomRepository)
        {
            _reservationRepository = reservationRepository;
            _classroomRepository = classroomRepository;
        }

        public async Task<ReservationResponse> CreateReservationAsync(CreateReservationRequest request)
        {
            // 1. Validate classroom exists
            var classroom = await _classroomRepository.GetByIdAsync(request.ClassroomId);
            if (classroom == null)
            {
                throw new Exception("Classroom not found.");
            }

            // 2. Check for time conflicts
            var existingReservations = await _reservationRepository.FindByClassroomAndDateAsync(request.ClassroomId, request.ReservationDate);
            
            var conflict = existingReservations.Any(r =>
                (request.StartTime < r.EndTime && request.EndTime > r.StartTime)
            );

            if (conflict)
            {
                throw new Exception("Time conflict with another reservation.");
            }

            // 3. Create and save reservation
            var newReservation = ReservationMapper.ToEntity(request);
            var createdReservation = await _reservationRepository.CreateAsync(newReservation);

            // 4. Return response DTO
            return ReservationMapper.ToResponse(createdReservation);
        }

        public async Task<IEnumerable<ReservationResponse>> GetReservationsByDateAsync(DateTime date)
        {
            var reservations = await _reservationRepository.FindByDateAsync(date);
            return reservations.Select(ReservationMapper.ToResponse).ToList();
        }

        public async Task CancelReservationAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new Exception("Reservation not found.");
            }

            await _reservationRepository.DeleteAsync(id);
        }
    }
}
