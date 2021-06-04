using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class Ticket
    {
        [Key]
        public long TId { get; set; }
        public long SId { get; set; }
        public string From{ get; set; }
        public string FromHalt { get; set; }
        public string To { get; set; }
        public string ToHalt { get; set; }
        public long PId { get; set; }
        public int NoOfSeats { get; set; }
        public int PStatus { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }

    }
}
