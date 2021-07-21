using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaundryWebApi.Dtos;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace LaundryWebApi.Service
{
    public class BookingsService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingsService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Booking CreateBooking(BookingDto dto, int authUserId)
        {

            //Check in DB if booking.Date & booking.StartTime is taken


            var booking = new Booking
            {

                UserId = authUserId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsCheckedOut = false,
                Status = false,
                DateBooked = DateTime.Today.ToString("yyyy-MM-dd")


            };

            DateTime currentTime = DateTime.Parse(DateTime.Now.ToString("HH:mm"));


            Console.WriteLine(currentTime);

            if (booking.DateBooked.Equals(dto.Date) && currentTime >= DateTime.Parse(booking.StartTime) && currentTime < DateTime.Parse(booking.EndTime))
                booking.Status = true;

            return _bookingRepository.SaveBooking(booking);



        }

        public IEnumerable<TimeSlot> GetTimeSlots(int authUserId)
        {
            //Generate timeSlots from Current date.
            DateTime startDate = DateTime.Today;
            string startDateAsString = startDate.ToString("yyyy-MM-dd");
            startDate = DateTime.Parse(startDateAsString);

            
            //Generete timeSlots to this date
            DateTime endDate = DateTime.Today.AddDays(31);
            string endDateAsString = endDate.ToString("yyyy-MM-dd");

            string startTimeAsString = "07:00";
            DateTime startTime = DateTime.Parse(startTimeAsString);
            startTimeAsString = startTime.ToString("HH:mm");

            string endTimeAsString = "14:00";
            DateTime endTime = DateTime.Parse(endTimeAsString);
            endTimeAsString = endTime.ToString("HH:mm");

            List<TimeSlot> timeSlotList = new List<TimeSlot>();

            int compareDates = DateTime.Compare(startDate, endDate);

            
           

            
            for (int i = 0; i<31; i++)
            {
               

                int counter = 0;

               

                for (int j = 0; j < 3; j++)
                {
                    TimeSlot slot = new TimeSlot();

                    string currentTime = DateTime.Now.ToString("HH:mm");

                    //Check if booking with date and time already exist.
                    var booking = _bookingRepository.GetBooking(startDateAsString, startTimeAsString);

                    if (booking != null && startTimeAsString == booking.StartTime && endTimeAsString == booking.EndTime)
                    {
                        slot.UserId = booking.UserId;
                        slot.Date = startDateAsString;
                        slot.StartTime = startTimeAsString;
                        slot.EndTime = endTimeAsString;
                        slot.Status = booking.Status;
                        slot.IsCheckedOut = booking.IsCheckedOut;
                    }
                    else
                    {
                        slot.Date = startDateAsString;
                        slot.StartTime = startTimeAsString;
                        slot.EndTime = endTimeAsString;
                        slot.Status = false;
                        slot.IsCheckedOut = false;
                    }

                     timeSlotList.Add(slot);

                   

                    if (counter == 0)
                    {
                        startTime = startTime.AddHours(7).AddMinutes(1);
                        startTimeAsString = startTime.ToString("HH:mm");

                        endTime = endTime.AddHours(2);
                        endTimeAsString = endTime.ToString("HH:mm");
                        counter++;
                    }
                    else
                    {
                        startTime = startTime.AddHours(2);
                        startTimeAsString = startTime.ToString("HH:mm");

                        endTime = endTime.AddHours(4);
                        endTimeAsString = endTime.ToString("HH:mm");
                        counter++;
                    }
                }

                startDate = startDate.AddDays(1);
                startDateAsString = startDate.ToString("yyyy-MM-dd");

                startTimeAsString = "07:00";
                startTime = DateTime.Parse(startTimeAsString);
                startTimeAsString = startTime.ToString("HH:mm");

                endTimeAsString = "14:00";
                endTime = DateTime.Parse(endTimeAsString);
                endTimeAsString = endTime.ToString("HH:mm");
            }

            

            

            return timeSlotList;


        }
    }
}
