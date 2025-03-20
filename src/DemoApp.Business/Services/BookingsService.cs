using AutoMapper;
using DemoApp.Business.CustomExceptions;
using DemoApp.Data;
using DemoApp.Models.Dtos;
using DemoApp.Models.Requests;
using Microsoft.Extensions.Logging;

namespace DemoApp.Business.Services
{
    public class BookingsService : IBookingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookingsService> _logger;
        private readonly IMapper _mapper;

        const int MAX_ALLOWED_BOOKINGS = 2;

        public BookingsService(IUnitOfWork unitOfWork, ILogger<BookingsService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> AddBooking(BookingRequest request)
        {
            // Check if member have reached max allowed bookings.
            var member = await _unitOfWork.Members.GetAsync(request.MemberId);
            if (member == null)
            {
                throw new BadRequestException($"Member with id {request.MemberId} doesn't exists.");
            }
            if (member.BookingCount >= MAX_ALLOWED_BOOKINGS)
            {
                throw new BadRequestException("Member had reached maximum no. of bookings.");
            }

            // Check if inventory count has not depleted.
            var inventoryItem = await _unitOfWork.Inventory.GetAsync(request.InventoryItemId);
            if (inventoryItem == null)
            {
                throw new BadRequestException($"Inventory item with id {request.InventoryItemId} doesn't exists.");
            }
            if (inventoryItem.RemaningCount == 0)
            {
                throw new BadRequestException("Requested inventory item is depleted.");
            }

            var booking = new Data.Entities.Booking 
            { 
                MemberId = request.MemberId, 
                InventoryItemId = request.InventoryItemId,
                TimeStamp = DateTime.UtcNow
            };
            _unitOfWork.Bookings.Add(booking);
            inventoryItem.RemaningCount--;
            member.BookingCount++;

            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Booking with booking id {BookingId} created successfully.", booking.Id);
            return booking.Id;
        }

        public async Task CancelBooking(CancellationRequest request)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(request.BookingId);
            if (booking == null)
            {
                throw new BadRequestException($"Booking with booking ref {request.BookingId} doesn't exists");
            }

            var member = await _unitOfWork.Members.GetAsync(booking.MemberId);
            var inventoryItem = await _unitOfWork.Inventory.GetAsync(booking.InventoryItemId);

            _unitOfWork.Bookings.Delete(booking);
            inventoryItem.RemaningCount++;
            member.BookingCount--;

            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Booking with booking id {BookingId} deleted successfully.", request.BookingId);
        }

        public async Task<IEnumerable<BookingDto>> GetBookings()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }
    }
}
