using Booking.UseCases.DTOs.Booking;

namespace Booking.UseCases.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAll();
        Task<BookingDto> GetById(int id);
        Task<BookingDto> Add(BookingInsertDto bookingInsertDto);
        Task<bool> Update(BookingUpdateDto bookingUpdateDto);
        Task<bool> Delete(int id);
    }
}