using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranspotationTicketBooking.Models;

namespace TranspotationTicketBooking.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "BusController , Administrator")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly TicketBookingDBContext _context;

        public SessionController(TicketBookingDBContext context)
        {
            _context = context;
        }

        // GET: api/Session
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSession()
        {
            return await _context.Session.ToListAsync();
        }

        // GET: api/Session/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(long id)
        {
            var session = await _context.Session.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

        public class SessionRev
        {
         
            public long SId { get; set; }
            public string BusNo { get; set; }
            public long RId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime Date { get; set; }
            public int Seats { get; set; }
            public long Rev { get; set; }
        }
        [HttpGet("rev")]
        public async Task<ActionResult<IEnumerable<SessionRev>>> GetSessionWithRev()
        {
            //var SessionWithBusno = (_context.Session.Where(s => s.BusNo == id)).ToList();
            List<Session> sessions = _context.Session.ToList();
            List<SessionRev> sessionRevs = new List<SessionRev>();

            foreach (var ses in sessions) {

               
               List<Ticket> Ticket =_context.Ticket.Where(s => s.SId == ses.SId).ToList();
                var tot = 0;
                foreach(var tic in Ticket)
                {

                    tot = tot + tic.NoOfSeats * tic.Price;

                }

                sessionRevs.Add(new SessionRev() { BusNo = ses.BusNo, 
                    Date = ses.Date, 
                    RId = ses.RId, 
                    Seats = ses.Seats, 
                    SId = ses.SId, 
                    StartTime = ses.StartTime,
                    Rev = tot});


            }


            var SessionRev = (from r in _context.Session
                                    select new SessionRev()
                                    {


                                        BusNo = r.BusNo,
                                        Date = r.Date,
                                        StartTime = r.StartTime,
                                        RId = r.RId,
                                        SId = r.SId,
                                        Seats = r.Seats,
                                        Rev = 10
                                        
                                    }).ToList();



            if (SessionRev == null)
            {
                return NotFound();
            }

            return sessionRevs;
        }


        // GET: api/Session/BusNo/####
        [HttpGet("BusNo/{id}")]
        public async Task<ActionResult<IEnumerable<SessionWithRoute>>> GetSessionWithBusno(string id)
        {
            //var SessionWithBusno = (_context.Session.Where(s => s.BusNo == id)).ToList();

            var SessionWithBusno = (from r in _context.Session.Where(s => s.BusNo == id)
                                    join s in _context.Route on r.RId equals s.RId
                                    select new SessionWithRoute()
                                    {
                                        BusNo = r.BusNo,
                                        Date = r.Date,
                                        StartTime = r.StartTime,
                                        RId = r.RId,
                                        SId = r.SId,
                                        Seats = r.Seats,
                                        Start = s.StartHolt,
                                        Stop = s.StopHolt,
                                        RNum = s.RNum
                                    }).ToList();



            if (SessionWithBusno == null)
            {
                return NotFound();
            }

            return SessionWithBusno;
        }

        // PUT: api/Session/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(long id, Session session)
        {
            if (id != session.SId)
            {
                return BadRequest();
            }

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
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

        // POST: api/Session
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Session>> PostSession(Session session)
        {
            _context.Session.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.SId }, session);
        }

        // DELETE: api/Session/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Session>> DeleteSession(long id)
        {
            var session = await _context.Session.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Session.Remove(session);
            await _context.SaveChangesAsync();

            return session;
        }

        private bool SessionExists(long id)
        {
            return _context.Session.Any(e => e.SId == id);
        }
    }
}
