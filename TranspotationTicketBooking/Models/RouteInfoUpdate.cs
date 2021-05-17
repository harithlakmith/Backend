using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class RouteInfoUpdate
    {
        public long RId { get; set; }
        public string HoltName { get; set; }
        public float Price { get; set; }
        public float Time { get; set; }
        public long Distance { get; set; }
    }
}
