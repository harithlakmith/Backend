using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class TownList
    {
        public long Id { get; set; }
        public long RId { get; set; }
        public long HoltId { get; set; }
        public string HoltName { get; set; }
        public float Price { get; set; }
        public float Time { get; set; }
        public long Distance { get; set; }
        public long RouteID { get; set; }

    }
}
