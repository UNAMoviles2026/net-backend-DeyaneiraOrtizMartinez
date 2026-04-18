using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;

namespace reservations_api.Services
{
    public interface IReservationService
    {
        Task<ReservationResponse> CreateReservationAsync(CreateReservationRequest request);
        Task<IEnumerable<ReservationResponse>> GetReservationsByDateAsync(DateTime date);
        Task CancelReservationAsync(Guid id);
    }
}
