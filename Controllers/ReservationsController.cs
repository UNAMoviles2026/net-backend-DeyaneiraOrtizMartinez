using Microsoft.AspNetCore.Mvc;
using reservations_api.Services;
using System;
using System.Threading.Tasks;
using reservations_api.DTOs.Requests;

namespace reservations_api.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationsByDate([FromQuery] DateTime? date)
        {
            if (!date.HasValue)
            {
                return BadRequest(new { message = "The date query parameter is required." });
            }

            var reservations = await _reservationService.GetReservationsByDateAsync(date.Value);
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request)
        {
            try
            {
                var reservation = await _reservationService.CreateReservationAsync(request);
                return CreatedAtAction(nameof(CreateReservation), new { id = reservation.Id }, reservation);
            }
            catch (Exception ex)
            {
                // In a real application, you would log the exception
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            try
            {
                await _reservationService.CancelReservationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // In a real application, you would log the exception
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
