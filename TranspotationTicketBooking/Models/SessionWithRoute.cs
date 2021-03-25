using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class SessionWithRoute
    {

        public long SId { get; set; }
        public string BusNo { get; set; }
        public long RId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Date { get; set; }
        public int Seats { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }
        public string RNum { get; set; }
    }
}
