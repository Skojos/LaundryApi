using System;
using LaundryWebApi.Models;

namespace LaundryWebApi.Interface
{
    public interface IBookingRepository
    {
        Booking SaveBooking(Booking booking);

        Booking GetBooking(string currentDate, string startTime);
    }
}
