using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaundryWebApi.Models
{
    public class Booking
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool Status { get; set; }
        public string DateBooked { get; set; }

        
    }
}
