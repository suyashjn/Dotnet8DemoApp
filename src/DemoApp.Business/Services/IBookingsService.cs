using DemoApp.Models.Dtos;
using DemoApp.Models.Requests;

namespace DemoApp.Business.Services
{
    public interface IBookingsService
    {
        Task<Guid> AddBooking(BookingRequest request);
        Task CancelBooking(CancellationRequest request);
        Task<IEnumerable<BookingDto>> GetBookings();
    }
}
