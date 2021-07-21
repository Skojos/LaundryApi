using System;
namespace LaundryWebApi.Models
{
    public class TimeSlot
    {
       
        public int UserId { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool Status { get; set; }
    }
}
