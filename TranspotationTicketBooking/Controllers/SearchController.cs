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
        public async Task<ActionResult<IEnumerable<seatAll>>> GetTownList(DateTime date, string from_, string to_)
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

            var sessionSelectedExt2 = (from ssl in sessionSelected
                                       join rt in _context.Route on ssl.RId equals rt.RId
                                       select new SessionwithTicket()
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
                                           Tickets = _context.Ticket.Where(t => t.SId == ssl.SId).ToList()

                                       }).ToList();


            var SessionList = sessionSelectedExt.Where(s => s.Seats > _context.Ticket.Where(t => t.SId == s.SId).Count()).ToList();


           // return sessionSelectedExt2;

            var reTicket = (from ssl in sessionSelectedExt
                            join s in _context.Ticket on ssl.SId equals s.SId
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



            List<SessionwithTicket> tics = sessionSelectedExt2.ToList();

            List<seatAll> seatAll = new List<seatAll>();
         
            foreach (var tic in tics) {  // retrives session for date to and from
                                         //   seatAvl[] seatAv = new seatAvl[tic.Seats];
                List<seatAvl> seatAv = new List<seatAvl>();
                List<seAVL> selist = new List<seAVL>();
                
                int s = 0;
                foreach (var ti in tic.Tickets)
                {   //related ticekts to session
                    List<seAVL> selistTEMP = new List<seAVL>();
                    List<seAVL> selistTEMP2 = new List<seAVL>();
                    if (!seatAv.Any())
                    {

                        selist.Add( new seAVL() {TId=ti.TId, to = Int32.Parse(ti.To) , from = Int32.Parse(ti.From)}) ;
                        seatAv.Add(new seatAvl() { seat = 0, FromTo = selist });

                    }
                    else 
                    { 

                            int freeSeat = 0;
                            int countSeats = 0;
                            int SC = 0;
                            bool isSpace = false;
                        
                        int xv = 1;
                            int c = 0;


                        foreach (var SAv in seatAv)  //inside seat info array
                        {
                            int st = SAv.seat;
                            foreach (var FT in SAv.FromTo) // loop inside one seat
                            {

                                if (FT.from <= Int32.Parse(ti.To) && (FT.to >= Int32.Parse(ti.From)))
                                    {
                                        xv = 0; // crossed
                                   
                                    }
                                else
                                    {
                                        if (xv == 1) { xv = 1; } // not crossed
                                    }
                                c++;
                                selistTEMP.Add(new seAVL() {TId=FT.TId, from = FT.from, to = FT.to });
                            }



                        }


                        if (xv == 1)
                        {
                            selistTEMP.Add(new seAVL() { TId = ti.TId, from = Int32.Parse(ti.From), to = Int32.Parse(ti.To) });
                            seatAv.RemoveAt(SC);
                            //  seatAv.Insert(SC, new seatAvl() { seat = s, FromTo = selistTEMP });
                            seatAv.Add(new seatAvl() { seat = s, FromTo = selistTEMP });

                           // selistTEMP.Clear();

                        }

                        if (xv == 0)
                        {
                            // foreach (var FTA in SAv.FromTo) // loop inside one seat

                            selistTEMP2.Add(new seAVL() { TId = ti.TId, from = Int32.Parse(ti.From), to = Int32.Parse(ti.To) });
                            seatAv.Add(new seatAvl() { seat = s, FromTo = selistTEMP2 });
                          //  selistTEMP2.Clear();
                        }



                        //selistTEMP.Clear();
                        //selistTEMP2.Clear();
                        s++;
                    }

                    //s++;
                }
              
                seatAll.Add(new seatAll() {SId = tic.SId, ses = seatAv });

            }


            return seatAll;
          

            // List<TicketInfo> SesList = new List<TicketInfo>();
            // SesList = SessionList;
            //return SessionList;

        }

        public int isCrossed(int T_From, int T_To, int S_From, int S_To) {
            if ((S_From <= T_To) && (S_To >= T_From))
            {
                return 0; // crossed

            }
            else
            {
                return 1; // not crossed
            }

           
        }
        public class seatAll
        {
            public long SId { get; set; }
            public ICollection<seatAvl> ses { get; set; }
        }
        public class seatAvl { 
            public int seat { get; set; }
            public ICollection<seAVL> FromTo { get; set; }
        }
        public class seAVL
        {
            public long TId { get; set; }
            public int from { get; set; }
            public int to { get; set; }
           
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
