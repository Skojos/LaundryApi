using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaundryWebApi.Dtos;
using LaundryWebApi.Models;

namespace LaundryWebApi.Interface
{
    public interface IBookingService
    {
        Booking CreateBooking(BookingDto dto, int authUserId);

        IEnumerable<TimeSlot> GetTimeSlots(int authUserId);
    }
}
