using System;
using System.Threading.Tasks;
using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;

namespace reservations_api.Services
{
    public interface IReservationService
    {
        Task<ReservationResponse> CreateReservationAsync(CreateReservationRequest request);
        Task CancelReservationAsync(Guid id);
    }
}
