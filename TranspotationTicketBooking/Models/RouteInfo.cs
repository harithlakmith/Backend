using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranspotationTicketBooking.Models
{
    public class RouteInfo
    {
        [Key]
        public long Id { get; set; }
        public long RId { get; set; }
        public long HoltId { get; set; }
        public string HoltName { get; set; }
        public float Price { get; set; }
        public float Time { get; set; }
        public long Distance { get; set; }
       
    }
}
