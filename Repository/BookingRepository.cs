using System;
using System.Linq;
using LaundryWebApi.Data;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;

namespace LaundryWebApi.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly UserContext _userContext;

        public BookingRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public Booking GetBooking(string currentDate, string startTime)
        {
            return _userContext.bookings.FirstOrDefault(u => u.Date.Equals(currentDate) && u.StartTime.Equals(startTime));
        }

        public Booking SaveBooking(Booking booking)
        {
            _userContext.bookings.Add(booking);
            booking.Id = _userContext.SaveChanges();

            return booking;
        }
    }
}
