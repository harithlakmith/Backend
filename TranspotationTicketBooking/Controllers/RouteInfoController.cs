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
    [Route("[controller]")]
    //[Authorize(Roles = "BusController , Administrator")]
    [ApiController]
    public class RouteInfoController : ControllerBase
    {
        private readonly TicketBookingDBContext _context;

        public RouteInfoController(TicketBookingDBContext context)
        {
            _context = context;
        }

        // GET: api/RouteInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteInfo>>> GetRouteInfo()
        {
            return await _context.RouteInfo.ToListAsync();
        }

        // GET: RouteInfo/townlist
        [HttpGet("townlist")]
        public async Task<ActionResult<IEnumerable<SearchTownlist>>> GetTownList()
        {
            var townList = (from l in _context.RouteInfo
                            select new SearchTownlist()
                            {
                                HoltName = l.HoltName
                            }).Distinct().ToList();
            if (townList == null)
            {
                return NotFound();
            }

            return townList;
        }



        // GET: RouteInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RouteInfo>>> GetRouteInfo(int id)
        {
            var routeInfo = (_context.RouteInfo.Where(s => s.RId == id)).ToList();

            if (id == 0)
            {
                return NotFound();
            }

            return routeInfo;
        }

        // PUT: api/RouteInfo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouteInfo(int id, RouteInfo routeInfo)
        {
            if (id != routeInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(routeInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteInfoExists(id))
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

        // POST: api/RouteInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RouteInfo>> PostRouteInfo(RouteInfo routeInfo)
        {
            _context.RouteInfo.Add(routeInfo);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetRouteInfo", new { id = routeInfo.Id }, routeInfo);
        }

        [HttpPost("bulk/")]
        public async Task<ActionResult<RouteInfo>> PostRouteInfoUp(RouteInfo routeInfo)
        {



            return Ok();
        }

        // DELETE: api/RouteInfo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RouteInfo>> DeleteRouteInfo(int id)
        {
            var routeInfo = await _context.RouteInfo.FindAsync(id);
            if (routeInfo == null)
            {
                return NotFound();
            }

            _context.RouteInfo.Remove(routeInfo);
            await _context.SaveChangesAsync();

            return routeInfo;
        }

        private bool RouteInfoExists(int id)
        {
            return _context.RouteInfo.Any(e => e.Id == id);
        }
    }
}
