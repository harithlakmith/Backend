using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class TicketInfo
    {

        public long SId { get; set; }
        public string BusNo { get; set; }
        public long RId { get; set; }
        public string RNum { get; set; }
        public string RouteStartHolt { get; set; }
        public string RouteStopHolt { get; set; }
        public string FromHolt { get; set; }
        public string ToHolt { get; set; }
        public long FromHoltId { get; set; }
        public long ToHoltId { get; set; }
        public float TicketPrice { get; set; }
        public DateTime ArrivedTime { get; set; }
        public float Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Date { get; set; }
        public int Seats { get; set; }
        public int Check { get; set; }
    }
}
