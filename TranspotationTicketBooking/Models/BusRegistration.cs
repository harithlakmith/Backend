using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TranspotationTicketBooking.Models
{
    public class BusRegistration
    {
        public string BusNo { get; set; }
        public string DriverName { get; set; }
        public int DriverNo { get; set; }
        public string CondName { get; set; }
        public int CondNo { get; set; }
        public int MaxSeats { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        /*[DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }*/

        public int Verified { get; set; }

    }
}
