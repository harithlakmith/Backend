using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class BusInfo
    {
        [Key]
        public string BusNo { get; set; }
        public string DriverName { get; set; }
        public int  DriverNo{ get; set; }
        public string CondName { get; set; }
        public int CondNo { get; set; }
        public int MaxSeats { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public int Verified { get; set; }

       // public User User { get; set; }
    }
}
