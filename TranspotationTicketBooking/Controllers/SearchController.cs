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
    [Route("[controller]")]
   // [Authorize(Roles = "Passenger")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly TicketBookingDBContext _context;

        public int SesId;
        public int FromHId;
        public int ToHId;

        public SearchController(TicketBookingDBContext context)
        {
            _context = context;
        }

        // GET: search/SearchTicket?date=#&from_=#&to_=#


        [HttpGet("{SearchTicket}")]
        public async Task<ActionResult<IEnumerable<TicketInfo>>> GetTownList(DateTime date, string from_, string to_)
        {

            //var sessions = await _context.Session.Where(s => s.Date == date).ToListAsync();
            var sessions = (from s in _context.Session.Where(s => s.Date == date)
                            select new RID()
                            {
                                RouteID = s.RId
                            }).Distinct().ToList();

            var TownList = (from r in sessions
                            join s in _context.RouteInfo on r.RouteID equals s.RId
                            select new TownList()
                            {
                                Id = s.Id,
                                RId = s.RId,
                                HoltId = s.HoltId,
                                HoltName = s.HoltName,
                                Price = s.Price,
                                Time = s.Time,
                                Distance = s.Distance,
                                RouteID = r.RouteID
                            }
                                ).ToList();

            /* var FindRoute = ((from fr in TownList.Where(fr => fr.HoltName == from_) select fr.RId)
                             .Intersect
                                 (from t in TownList.Where(t => t.HoltName == to_) select t.RId)
                                 ).ToList();*/

            var FindRoute = (from fr in TownList.Where(fr => fr.HoltName == from_)
                             join t in TownList.Where(t => t.HoltName == to_) on fr.RId equals t.RId
                             where (fr.HoltId < t.HoltId)
                             select new FindRId()
                             {
                                 RouteID = fr.RId,
                                 ToHoltId = t.HoltId,
                                 ToDistance = t.Distance,
                                 ToPrice = t.Price,
                                 ToTime = t.Time,
                                 FromHoltId = fr.HoltId,
                                 FromDistance = fr.Distance,
                                 FromPrice = fr.Price,
                                 FromTime = fr.Time

                             }
                                ).ToList();

            var sessionSelected = (from frt in FindRoute
                                   join ses in _context.Session.Where(s => s.Date == date) on frt.RouteID equals ses.RId
                                   select new TicketInfo()
                                   {
                                       SId = ses.SId,
                                       BusNo = ses.BusNo,
                                       RId = ses.RId,
                                       RNum = null,
                                       RouteStartHolt = null,
                                       RouteStopHolt = null,
                                       FromHolt = null,
                                       ToHolt = null,
                                       FromHoltId = frt.FromHoltId,
                                       ToHoltId = frt.ToHoltId,
                                       TicketPrice = frt.ToPrice - frt.FromPrice,
                                       ArrivedTime = ses.StartTime + TimeSpan.FromHours(frt.FromTime),
                                       Duration = frt.ToTime - frt.FromTime,
                                       StartTime = ses.StartTime,
                                       Date = ses.Date,
                                       Seats = ses.Seats,
                                       Check = 0,

                                   }).ToList();

            var sessionSelectedExt = (from ssl in sessionSelected
                                      join rt in _context.Route on ssl.RId equals rt.RId
                                      select new TicketInfo()
                                      {
                                          SId = ssl.SId,
                                          BusNo = ssl.BusNo,
                                          RId = ssl.RId,
                                          RNum = rt.RNum,
                                          RouteStartHolt = rt.StartHolt,
                                          RouteStopHolt = rt.StopHolt,
                                          FromHolt = from_,
                                          ToHolt = to_,
                                          FromHoltId = ssl.FromHoltId,
                                          ToHoltId = ssl.ToHoltId,
                                          TicketPrice = ssl.TicketPrice,
                                          ArrivedTime = ssl.ArrivedTime,
                                          Duration = ssl.Duration,
                                          StartTime = ssl.StartTime,
                                          Date = ssl.Date,
                                          Seats = ssl.Seats,
                                          Check = ssl.Check,

                                      }).ToList();


            var SessionList = sessionSelectedExt.Where(s => s.Seats > _context.Ticket.Where(t => t.SId == s.SId).Count()).ToList();


            return SessionList;


            // List<TicketInfo> SesList = new List<TicketInfo>();
            // SesList = SessionList;
            //return SessionList;

        }

        /*foreach (var vall in BookedTicketList)
                    {
                       
                        if ((vall.From > sslst.FromHoltId
                                && vall.From > sslst.ToHoltId)
                            || (vall.To < sslst.FromHoltId 
                                 && vall.To < sslst.ToHoltId))
                        {
                            

                            sslst.Check = 1;
                            break;
                         
                        }
                        else 
                        {
                            sslst.Check = 0;
                            break;
                        }
                        
                    }*/



        /*var BookedTicketList =(from tk in _context.Ticket
                                            select new Ticket()
                                            {
                                                TId = tk.TId,
                                                SId = tk.SId,
                                                From =tk.From,
                                                FromHalt = tk.FromHalt,
                                                To = tk.To,
                                                ToHalt = tk.ToHalt,
                                                PId = tk.PId,
                                                PStatus = tk.PStatus,
                                                NoOfSeats = tk.NoOfSeats,
                                                Date = tk.Date
                                              
                                            }).ToList();*/















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
