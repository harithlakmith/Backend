using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class FindRId
    {
        public long RouteID { get; set; }
        public long ToHoltId { get; set; }
        public float ToPrice { get; set; }
        public float ToTime { get; set; }
        public long ToDistance { get; set; }
        public long FromHoltId { get; set; }
        public float FromPrice { get; set; }
        public float FromTime { get; set; }
        public long FromDistance { get; set; }

    }
}
