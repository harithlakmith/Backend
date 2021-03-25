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
    [Authorize(Roles = "BusController , Administrator")]
    [ApiController]
    public class BusInfoController : ControllerBase
    {
        private readonly TicketBookingDBContext _context;

        public BusInfoController(TicketBookingDBContext context)
        {
            _context = context;
        }

        // GET: api/BusInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusInfo>>> GetBusInfo()
        {
            return await _context.BusInfo.ToListAsync();
        }

        // GET: api/BusInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusInfo>> GetBusInfo(string id)
        {
            var busInfo = await _context.BusInfo.FindAsync(id);

            if (busInfo == null)
            {
                return NotFound();
            }

            return busInfo;
        }

        // PUT: api/BusInfo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusInfo(string id, BusInfo busInfo)
        {
            if (id != busInfo.BusNo)
            {
                return BadRequest();
            }

            _context.Entry(busInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusInfoExists(id))
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

        // POST: api/BusInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BusInfo>> PostBusInfo(BusInfo busInfo)
        {
            _context.BusInfo.Add(busInfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BusInfoExists(busInfo.BusNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBusInfo", new { id = busInfo.BusNo }, busInfo);
        }

        // DELETE: api/BusInfo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BusInfo>> DeleteBusInfo(string id)
        {
            var busInfo = await _context.BusInfo.FindAsync(id);
            if (busInfo == null)
            {
                return NotFound();
            }

            _context.BusInfo.Remove(busInfo);
            await _context.SaveChangesAsync();

            return busInfo;
        }

        private bool BusInfoExists(string id)
        {
            return _context.BusInfo.Any(e => e.BusNo == id);
        }
    }
}
