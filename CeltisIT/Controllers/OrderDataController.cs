using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CeltisIT.Model;
using CeltisIT.Model.Repository;

namespace CeltisIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDataController : ControllerBase
    {
        private readonly CeltisITContext _context;

        public OrderDataController(CeltisITContext context)
        {
            _context = context;
        }

        // GET: api/OrderData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tabsodata>>> GetTabsodata()
        {
            return await _context.Tabsodata.ToListAsync();
        }

        // GET: api/OrderData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tabsodata>> GetTabsodata(decimal id)
        {
            var tabsodata = await _context.Tabsodata.FindAsync(id);

            if (tabsodata == null)
            {
                return NotFound();
            }

            return tabsodata;
        }

        // PUT: api/OrderData/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTabsodata(decimal id, Tabsodata tabsodata)
        {
            if (id != tabsodata.Sodataid)
            {
                return BadRequest();
            }

            _context.Entry(tabsodata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabsodataExists(id))
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

        // POST: api/OrderData
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tabsodata>> PostTabsodata(Tabsodata tabsodata)
        {
            _context.Tabsodata.Add(tabsodata);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTabsodata", new { id = tabsodata.Sodataid }, tabsodata);
        }

        // DELETE: api/OrderData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tabsodata>> DeleteTabsodata(decimal id)
        {
            var tabsodata = await _context.Tabsodata.FindAsync(id);
            if (tabsodata == null)
            {
                return NotFound();
            }

            _context.Tabsodata.Remove(tabsodata);
            await _context.SaveChangesAsync();

            return tabsodata;
        }

        private bool TabsodataExists(decimal id)
        {
            return _context.Tabsodata.Any(e => e.Sodataid == id);
        }
    }
}
