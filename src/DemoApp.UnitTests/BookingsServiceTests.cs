using AutoMapper;
using DemoApp.Business.Services;
using DemoApp.Data;
using DemoApp.Models.Dtos;
using DemoApp.Models.Requests;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DemoApp.UnitTests.Services
{
    public class BookingsServiceTests
    {
        private readonly BookingsService _bookingsService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<BookingsService>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        public BookingsServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<BookingsService>>();
            _mockMapper = new Mock<IMapper>();
            _bookingsService = new BookingsService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Theory]
        [MemberData(nameof(GetBookingRequests))]
        public async Task AddBooking_ShouldCompleteSuccessfully(BookingRequest bookingRequest)
        {
            // Arrange
            var member = new Data.Entities.Member { Id = bookingRequest.MemberId, BookingCount = 1 };
            var inventoryItem = new Data.Entities.InventoryItem { Id = bookingRequest.InventoryItemId, RemaningCount = 5 };
            _mockUnitOfWork.Setup(uow => uow.Members.GetAsync(bookingRequest.MemberId)).ReturnsAsync(member);
            _mockUnitOfWork.Setup(uow => uow.Inventory.GetAsync(bookingRequest.InventoryItemId)).ReturnsAsync(inventoryItem);
            _mockUnitOfWork.Setup(uow => uow.Bookings.Add(It.IsAny<Data.Entities.Booking>()));
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _bookingsService.AddBooking(bookingRequest);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Bookings.Add(It.IsAny<Data.Entities.Booking>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetCancellationRequests))]
        public async Task CancelBooking_ShouldCompleteSuccessfully(CancellationRequest cancellationRequest)
        {
            // Arrange
            var booking = new Data.Entities.Booking { Id = cancellationRequest.BookingId, MemberId = Guid.NewGuid(), InventoryItemId = Guid.NewGuid() };
            var member = new Data.Entities.Member { Id = booking.MemberId, BookingCount = 1 };
            var inventoryItem = new Data.Entities.InventoryItem { Id = booking.InventoryItemId, RemaningCount = 5 };
            _mockUnitOfWork.Setup(uow => uow.Bookings.GetAsync(cancellationRequest.BookingId)).ReturnsAsync(booking);
            _mockUnitOfWork.Setup(uow => uow.Members.GetAsync(booking.MemberId)).ReturnsAsync(member);
            _mockUnitOfWork.Setup(uow => uow.Inventory.GetAsync(booking.InventoryItemId)).ReturnsAsync(inventoryItem);
            _mockUnitOfWork.Setup(uow => uow.Bookings.Delete(booking));
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _bookingsService.CancelBooking(cancellationRequest);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Bookings.Delete(booking), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetBookingsData))]
        public async Task GetBookings_ShouldReturnListOfBookings(List<Data.Entities.Booking> bookings, List<BookingDto> expectedBookings)
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Bookings.GetAllAsync()).ReturnsAsync(bookings);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<BookingDto>>(bookings)).Returns(expectedBookings);

            // Act
            var result = await _bookingsService.GetBookings();

            // Assert
            Assert.Equal(expectedBookings, result);
        }

        public static IEnumerable<object[]> GetBookingRequests()
        {
            yield return new object[]
            {
                new BookingRequest(Guid.NewGuid(), Guid.NewGuid()),
            };
        }

        public static IEnumerable<object[]> GetCancellationRequests()
        {
            yield return new object[]
            {
                new CancellationRequest(Guid.NewGuid())
            };
        }

        public static IEnumerable<object[]> GetBookingsData()
        {
            yield return new object[]
            {
                new List<Data.Entities.Booking>
                {
                    new Data.Entities.Booking { /* Initialize properties */ },
                    new Data.Entities.Booking { /* Initialize properties */ }
                },
                new List<BookingDto>
                {
                    new BookingDto { /* Initialize properties */ },
                    new BookingDto { /* Initialize properties */ }
                }
            };
        }
    }
}
