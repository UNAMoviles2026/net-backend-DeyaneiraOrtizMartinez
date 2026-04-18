using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reservations_api.Models.Entities;

namespace reservations_api.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> GetByIdAsync(Guid id);
        Task<Reservation> CreateAsync(Reservation reservation);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Reservation>> FindByClassroomAndDateAsync(Guid classroomId, DateTime date);
        Task<IEnumerable<Reservation>> FindByDateAsync(DateTime date);
    }
}
