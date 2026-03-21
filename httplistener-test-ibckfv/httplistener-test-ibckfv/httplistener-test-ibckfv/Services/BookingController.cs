using httplistener_test_ibckfv.Dto;
using httplistener_test_ibckfv.models;
using httplistener_test_ibckfv.Repositories;

namespace httplistener_test_ibckfv.Services;

public class BookingController
{
    private readonly BookingRepository _bookingRepo;
    private readonly ApartmentRepository _aptRepo;

    public BookingController(BookingRepository b, ApartmentRepository a)
    {
        _bookingRepo = b;
        _aptRepo = a;
    }

    public Booking CreateBooking(BookingCreateDto dto, int userId)
    {
        if (dto.DateEnd.Date < dto.DateStart.Date) throw new Exception("Дата конца должна юыть >= даты начала");
        bool overlaps = _bookingRepo.ExistsOverlap(dto.ApartmentId, dto.DateStart, dto.DateEnd, "Approved");
        if (overlaps) throw new Exception("Запрашиваемая дата пересекается с занятой");
        var booking = new Booking
        {
            ApartmentId = dto.ApartmentId,
            UserId = userId,
            DateStart = dto.DateStart.Date,
            DateEnd = dto.DateEnd.Date,
            Status = "Pending"
        };
        return _bookingRepo.Create(booking);
    }

    public void UpdateBookingStatus(int bookingId, string newStatus, int actingUserId)
    {
        var booking = _bookingRepo.GetById(bookingId) ?? throw new Exception("Бронь не найдена");
        var ap = _aptRepo.GetById(booking.ApartmentId) ?? throw new Exception("Аппартаменты не найдены");
        if (ap.OwnerId != actingUserId) throw new Exception("Только владелец может менять статус");
        if (newStatus == "Approved")
        {
            bool overlaps = _bookingRepo.ExistsOverlapExcludingBooking(ap.Id, booking.DateStart, booking.DateEnd, "Approved", bookingId);
            if (overlaps) throw new Exception("Подтверждение невозможно: даты пересекаются с уже забронированными");
        }
        _bookingRepo.UpdateStatus(bookingId, newStatus);
    }

    public IEnumerable<object> GetTop3ByApprovedBookings()
    {
        return _bookingRepo.Top3ApartmentsByApprovedBookings();
    }
}