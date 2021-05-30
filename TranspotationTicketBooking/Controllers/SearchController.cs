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
        public async Task<ActionResult<ICollection<SendList>>> GetTownList(DateTime date, string from_, string to_)
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


          //  return sessionSelectedExt2;


            ////////////////////////////////////////////////////////////////////
            
        List<SessionwithTicket> FilteredSessions = sessionSelectedExt2.ToList();
            List<AllList> AllList = new List<AllList>();

            foreach (var FilteredSession in FilteredSessions)  //inside one session
            {

                List<MergeList> MergeList = new List<MergeList>();

                foreach (var Ticket in FilteredSession.Tickets)  //inside one ticket
                {
                    
                    List<TicketId> TIds = new List<TicketId>();
                    
                    if (!MergeList.Any())
                    {
                        TIds.Add(new TicketId() { TId = Ticket.TId });
                        MergeList.Add(new MergeList() { TIds=TIds, from = Int32.Parse(Ticket.From), to = Int32.Parse(Ticket.To) });
                    }
                    else
                    {
                        int countMergeList = 0;
                        int TempFrom=0;
                        int TempTo=0;
                        int operation=0;
                     
                        foreach (var ItemMergeList in MergeList) 
                        {
                            if (Int32.Parse(Ticket.To) == ItemMergeList.from)
                            {
                                foreach (var TicIds in ItemMergeList.TIds)
                                {
                                    TIds.Add(new TicketId() { TId = TicIds.TId });
                                }
                                TIds.Add(new TicketId() { TId = Ticket.TId });
                                TempFrom = ItemMergeList.from;
                                TempTo = ItemMergeList.to;

                                operation = 1;
                                break;
                            }
                            else if (Int32.Parse(Ticket.From) == ItemMergeList.to)
                            {
                                foreach (var TicIds in ItemMergeList.TIds)
                                {
                                    TIds.Add(new TicketId() { TId = TicIds.TId });
                                }
                                TIds.Add(new TicketId() { TId = Ticket.TId });
                                TempFrom = ItemMergeList.from;
                                TempTo = ItemMergeList.to;

                                operation = 2;
                                break;
                            }
                           
                            countMergeList++;
                        }

                        if (operation == 1)
                        {

                            MergeList.RemoveAt(countMergeList);

                            MergeList.Insert(countMergeList, new MergeList()
                            {
                                TIds = TIds,
                                from = Int32.Parse(Ticket.From),
                                to = TempTo
                            });
                        }
                        else if (operation == 2)
                        {

                            MergeList.RemoveAt(countMergeList);

                            MergeList.Insert(countMergeList, new MergeList()
                            { TIds = TIds, from = TempFrom, to = Int32.Parse(Ticket.To) });
                        }
                        else 
                        {
                            TIds.Add(new TicketId() { TId = Ticket.TId });
                            MergeList.Add(new MergeList()
                            {
                                TIds = TIds,
                                from = Int32.Parse(Ticket.From),
                                to = Int32.Parse(Ticket.To)
                            });
                        }



                    }


                }
              //  AllList.Add(new AllList() {SessionID = FilteredSession.SId ,List = MergeList });


                //final list
                List<FinalList> FinalLists = new List<FinalList>();
                foreach (var FixedList in MergeList ) 
                {
                    List<MergeList> FromTo = new List<MergeList>();
                    if (!FinalLists.Any())
                    {
                        FromTo.Add(new MergeList() { from = FixedList.from, to = FixedList.to, TIds = FixedList.TIds });
                        FinalLists.Add(new FinalList() { seat = 0, FromTo = FromTo });
                    }
                    else 
                    {
                        bool canAdd = true;
                        int countSeat = 0;
                        List<MergeList> TempFromTo = new List<MergeList>();

                        foreach (var FinalList in FinalLists )
                        {
                           canAdd = true;
                            TempFromTo.Clear();
                            foreach (var FromToList in FinalList.FromTo)
                            {
                                if ((FixedList.to < FromToList.from) || (FixedList.from > FromToList.to))
                                {
                                    canAdd = canAdd && true;
                                }
                                else
                                {
                                    canAdd = canAdd && false;
                                }

                                TempFromTo.Add(new MergeList() {from = FromToList.from, to = FromToList.to, TIds = FromToList.TIds });


                            }

                            if (canAdd) { break; }

                            countSeat++;
                        }
                        if (canAdd)
                        {
                            TempFromTo.Add(new MergeList() { from = FixedList.from, to = FixedList.to, TIds = FixedList.TIds });
                            FinalLists.RemoveAt(countSeat);
                            FinalLists.Add(new FinalList() { seat = countSeat, FromTo = TempFromTo });

                        }
                        if (!canAdd)
                        {
                            FromTo.Add(new MergeList() { from = FixedList.from, to = FixedList.to, TIds = FixedList.TIds });
                            FinalLists.Add(new FinalList() { seat = countSeat, FromTo = FromTo });

                        }


                    }


                }

                AllList.Add(new AllList() { SessionID = FilteredSession.SId, List = FinalLists });


            } //end inside one session



            List<SessionwithTicket> FilteredSessionsCheck = sessionSelectedExt2.ToList();
            List<SendList> FinalSendLists = new List<SendList>();
            List<SendList> SendLists = new List<SendList>();

            foreach (var FSession in FilteredSessionsCheck)  //inside one session
            {
                long userFrom = FSession.FromHoltId;
                long userTo = FSession.ToHoltId;
                int AvSeat = 0;
                int bookedSeat = 0;
                int SesSeat = FSession.Seats;
                int canBook = 0;
                var ListAccSess = AllList.Where(a => a.SessionID == FSession.SId).ToList();
                foreach (var ListAccSes in ListAccSess)
                {
                    bookedSeat = ListAccSes.List.Count();
                    foreach (var seatList in ListAccSes.List) //seat by seat
                    {
                        bool IsSpace = true;
                        foreach(var checkFromTo in seatList.FromTo)
                        {
                            if ((userTo <= checkFromTo.from) || (checkFromTo.to<=userFrom))
                            {
                                IsSpace = IsSpace && true;
                            }
                            else
                            {
                                IsSpace = IsSpace && false;
                            }
                        }
                    
                        if(IsSpace)
                        { 
                            AvSeat++;
                        }
                    }
                
                }

                canBook = SesSeat - bookedSeat + AvSeat;


                if (canBook > 0)
                {

                    SendLists.Add(new SendList()
                    {
                        SId = FSession.SId,
                        BusNo = FSession.BusNo,
                        RId = FSession.RId,
                        RNum = FSession.RNum,
                        RouteStartHolt = FSession.RouteStartHolt,
                        RouteStopHolt = FSession.RouteStopHolt,
                        FromHolt = from_,
                        ToHolt = to_,
                        FromHoltId = FSession.FromHoltId,
                        ToHoltId = FSession.ToHoltId,
                        TicketPrice = FSession.TicketPrice,
                        ArrivedTime = FSession.ArrivedTime,
                        Duration = FSession.Duration,
                        StartTime = FSession.StartTime,
                        Date = FSession.Date,
                        Seats = FSession.Seats,
                        Check = FSession.Check,
                        FreeSeats = canBook
                    });
                }

            }

               return SendLists;
             
        


        }
        ///////////////////////////////////////////////////////
        public class FinalSendList
        {
            public ICollection<SendList> Send { get; set; }
        }
        public class AllList
        {
            public long SessionID { get; set; }
            public ICollection<FinalList> List { get; set; }
        }
            public class MergeList
        {
            public ICollection<TicketId> TIds { get; set; }
            public int from { get; set; }
            public int to { get; set; }
        }

        public class TicketId
        {
            public long TId { get; set; }
            
        }

        public class FinalList
        {
            public int seat { get; set; }
            public ICollection<MergeList> FromTo { get; set; }

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
