
using Application.DTOs.Booking;

namespace Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAll();
        Task<BookingDto> GetById(int id);
        Task<BookingDto> Add(BookingInsertDto bookingInsertDto);
        Task<bool> Update(BookingUpdateDto bookingUpdateDto );
        Task<bool> Delete(int id);
    }
}