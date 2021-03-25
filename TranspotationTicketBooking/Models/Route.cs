using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class Route
    {
        [Key]
        public long RId { get; set; }
        public long StartHoltId { get; set; }
        public string StartHolt { get; set; }
        public long StopHoltId { get; set; }
        public string StopHolt { get; set; }
        public long Duration { get; set; }
        public long Distance { get; set; }
        public string RNum { get; set; }
        
    }
}
