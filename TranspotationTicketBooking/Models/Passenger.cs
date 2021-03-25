using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TranspotationTicketBooking.Models
{
    public class Passenger 
    {
        [Key]
        public long PId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Tp { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public int Verified { get; set; }
        public string Token { get; set; }

       // public User User { get; set; }
        
    }
}
