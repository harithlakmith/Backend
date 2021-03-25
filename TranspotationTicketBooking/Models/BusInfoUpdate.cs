using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class BusInfoUpdate
    {
        public string BusNo { get; set; }
        public string DriverName { get; set; }
        public int DriverNo { get; set; }
        public string CondName { get; set; }
        public int CondNo { get; set; }
        public int MaxSeats { get; set; }
    }
}
