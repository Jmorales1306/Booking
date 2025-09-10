using Booking.UseCases.DTOs.Booking;

namespace Booking.UseCases.Services
{
    public class BookingService(IUnitOfWork unitOfWork) : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<BookingDto>> GetAll()
        {
            var bookings = await _unitOfWork.Bookings.GetAll();
            return [.. bookings.Select(b => new BookingDto
            {
                Id = b.Id,
                Date = b.Date,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                RoomId = b.RoomId,
                UserId = b.UserId,
                ClientId = b.ClientId
            })];
        }

        public async Task<BookingDto> GetById(int id)
        {
            var booking = await _unitOfWork.Bookings.GetById(id)
                          ?? throw new KeyNotFoundException($"Reserva con el Id:{id} no encontrada.");

            return new BookingDto
            {
                Id = booking.Id,
                Date = booking.Date,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                ClientId = booking.ClientId
            };
        }

        public async Task<BookingDto> Add(BookingInsertDto bookingInsertDto)
        {

            var room = await _unitOfWork.Rooms.GetById(bookingInsertDto.RoomId);
            if (room == null)
            {
                throw new KeyNotFoundException($"La Sala con el ID '{bookingInsertDto.RoomId}' no existe.");
            }

            var user = await _unitOfWork.Users.GetById(bookingInsertDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"El Usuario con el ID '{bookingInsertDto.UserId}' no existe.");
            }

            var client = await _unitOfWork.Clients.GetById(bookingInsertDto.ClientId);
            if (client == null)
            {
                throw new KeyNotFoundException($"El Cliente con el ID '{bookingInsertDto.ClientId}' no existe.");
            }

            if (bookingInsertDto.StartTime >= bookingInsertDto.EndTime)
            {
                throw new InvalidOperationException("La hora de inicio debe ser anterior a la hora de fin.");
            }

            if (bookingInsertDto.Date.Date < DateTime.Today.Date)
            {
                throw new InvalidOperationException("La fecha de la reserva no puede ser en el pasado.");
            }

            var booking = new Core.Entities.Booking
            {
                Date = bookingInsertDto.Date,
                StartTime = bookingInsertDto.StartTime,
                EndTime = bookingInsertDto.EndTime,
                RoomId = bookingInsertDto.RoomId,
                UserId = bookingInsertDto.UserId,
                ClientId = bookingInsertDto.ClientId
            };

            await _unitOfWork.Bookings.Add(booking);
            await _unitOfWork.Complete();

            return new BookingDto
            {
                Id = booking.Id,
                Date = booking.Date,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                ClientId = booking.ClientId
            };
        }

        public async Task<bool> Update(BookingUpdateDto bookingUpdateDto)
        {

            var existBooking = await _unitOfWork.Bookings.GetById(bookingUpdateDto.Id) ?? throw new KeyNotFoundException($"La Reserva con el ID '{bookingUpdateDto.Id}' no existe para actualizar.");

            var room = await _unitOfWork.Rooms.GetById(bookingUpdateDto.RoomId);
            if (room == null)
            {
                throw new KeyNotFoundException($"La Sala con el ID '{bookingUpdateDto.RoomId}' no existe.");
            }

            var user = await _unitOfWork.Users.GetById(bookingUpdateDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"El Usuario con el ID '{bookingUpdateDto.UserId}' no existe.");
            }

            var client = await _unitOfWork.Clients.GetById(bookingUpdateDto.ClientId);
            if (client == null)
            {
                throw new KeyNotFoundException($"El Cliente con el ID '{bookingUpdateDto.ClientId}' no existe.");
            }

            if (bookingUpdateDto.StartTime >= bookingUpdateDto.EndTime)
            {
                throw new InvalidOperationException("La hora de inicio debe ser anterior a la hora de fin.");
            }

            if (bookingUpdateDto.Date.Date < DateTime.Today.Date)
            {
                throw new InvalidOperationException("La fecha de la reserva no puede ser en el pasado.");
            }

            var isRoomAvailable = await _unitOfWork.Bookings.IsRoomAvailableForUpdate(
                bookingUpdateDto.RoomId,
                bookingUpdateDto.Date,
                bookingUpdateDto.StartTime,
                bookingUpdateDto.EndTime,
                bookingUpdateDto.Id
            );

            if (!isRoomAvailable)
            {
                throw new InvalidOperationException($"La Sala con ID '{bookingUpdateDto.RoomId}' no está disponible en la fecha y horario especificados debido a otra reserva.");
            }

            existBooking.Date = bookingUpdateDto.Date;
            existBooking.StartTime = bookingUpdateDto.StartTime;
            existBooking.EndTime = bookingUpdateDto.EndTime;
            existBooking.RoomId = bookingUpdateDto.RoomId;
            existBooking.UserId = bookingUpdateDto.UserId;
            existBooking.ClientId = bookingUpdateDto.ClientId;

            _unitOfWork.Bookings.Update(existBooking);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var bookingToDelete = await _unitOfWork.Bookings.GetById(id);
            if (bookingToDelete == null)
            {
                throw new KeyNotFoundException($"La Reserva con el ID '{id}' no existe para eliminar.");
            }

            _unitOfWork.Bookings.Delete(bookingToDelete);
            var rowsAffected = await _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}