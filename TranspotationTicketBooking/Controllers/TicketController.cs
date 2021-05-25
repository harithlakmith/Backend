﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranspotationTicketBooking.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Web;
using System.Data;
using System.Collections;


namespace TranspotationTicketBooking.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketBookingDBContext _context;

        public TicketController(TicketBookingDBContext context)
        {
            _context = context;
        }

        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            return await _context.Ticket.ToListAsync();
        }


        // GET: api/Ticket
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GeTicket(long id)
        {
            // var ticket = await _context.Ticket.FindAsync(id);
            var ticket = (from s in _context.Ticket.Where(s => s.PId == id)
                          select new Ticket()
                          {
                              TId = s.TId,
                              SId = s.SId,
                              From = s.From,
                              FromHalt = s.FromHalt,
                              PId = s.PId,
                              NoOfSeats = s.NoOfSeats,
                              PStatus = s.PStatus,
                              Date = s.Date,
                              Price = s.Price,
                              To = s.To,
                              ToHalt = s.ToHalt

                          }).ToList();

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }



        [HttpGet("session/{id}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetSessionTicket(long id)
        {
           // var ticket = await _context.Ticket.FindAsync(id);
            var ticket = (from s in _context.Ticket.Where(s => s.SId == id)
                          select new Ticket()
                          {
                                TId= s.TId,
                                SId= s.SId,
                                From= s.From,
                                FromHalt= s.FromHalt,
                                PId= s.PId,
                                NoOfSeats= s.NoOfSeats,
                                PStatus= s.PStatus,
                                Date= s.Date,
                                Price= s.Price,
                                To=s.To,
                                ToHalt=s.ToHalt
                          
                          }).ToList();

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }


        // PUT: api/Ticket/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(long id, Ticket ticket)
        {
            if (id != ticket.TId)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ticket
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.TId }, ticket);
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ticket>> DeleteTicket(long id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        private bool TicketExists(long id)
        {
            return _context.Ticket.Any(e => e.TId == id);
        }
    }
}
