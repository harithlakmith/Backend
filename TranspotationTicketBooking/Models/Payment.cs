using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class Payment
    {
        public string BusNo { get; set; }
        public int TId { get; set; }
        public string Road { get; set; }
        public string Steats { get; set; }
        public int Price { get; set; }
        public string CusEmail { get; set; }
    }
}
