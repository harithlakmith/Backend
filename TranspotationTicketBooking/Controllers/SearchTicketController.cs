using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranspotationTicketBooking.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Web;
using System.Data;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace TranspotationTicketBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchTicketController : ControllerBase
    {
        private readonly TicketBookingDBContext _context;

        public SearchTicketController(TicketBookingDBContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ICollection<TicketList>>> GetTicketList(string id)
        {

           
            var ticketList = (from t in _context.Ticket.Where(t => t.UserId == id)
                            join s in _context.Session on t.SId equals s.SId
                            select new TicketList()
                            {
                                TId = t.TId,
                                SId = t.SId,
                                From = t.From,
                                FromHalt = t.FromHalt,
                                To = t.To,
                                ToHalt = t.ToHalt,
                                PId = t.PId,
                                NoOfSeats = t.NoOfSeats,
                                PStatus = t.PStatus,
                                Price = t.Price,
                                Date = s.Date,
                                UserId = t.UserId
                                
                            }
                                ).ToList();
            return ticketList;

        }

    }
 }
