using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reservations_api.Data;
using reservations_api.Models.Entities;

namespace reservations_api.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteAsync(Guid id)
        {
            var reservation = await GetByIdAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Reservation>> FindByClassroomAndDateAsync(Guid classroomId, DateTime date)
        {
            return await _context.Reservations
                .Where(r => r.ClassroomId == classroomId && r.ReservationDate.Date == date.Date)
                .ToListAsync();
        }
    }
}
