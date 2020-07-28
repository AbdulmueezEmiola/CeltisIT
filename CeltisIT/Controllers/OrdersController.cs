using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CeltisIT.Model;
using CeltisIT.Model.Repository;
using Microsoft.AspNetCore.Authorization;

namespace CeltisIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CeltisITContext _context;

        public OrdersController(CeltisITContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<Tabsorder>>> GetTabsorder()
        {
            var orders = await _context.Tabsorder.ToListAsync();
            foreach(Tabsorder order in orders)
            {
                _context.Entry(order).Reference(p => p.Cust).Load();
            }
            return orders;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Tabsorder>> GetTabsorder(decimal id)
        {
            var tabsorder = await _context.Tabsorder.FindAsync(id);
            _context.Entry(tabsorder).Reference(x => x.Cust).Load();
            _context.Entry(tabsorder).Collection(x => x.Tabsodata).Load();
            foreach(Tabsodata data in tabsorder.Tabsodata)
            {
                _context.Entry(data).Reference(x => x.Item).Load();
            }
            string value = tabsorder.Cust?.Custname;
            if (tabsorder == null)
            {
                return NotFound();
            }

            return tabsorder;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> PutTabsorder(decimal id, Tabsorder tabsorder)
        {
            if (id != tabsorder.Sordid)
            {
                return BadRequest();
            }

            _context.Entry(tabsorder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabsorderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Tabsorder>> PostTabsorder(Tabsorder tabsorder)
        {
            _context.Tabsorder.Add(tabsorder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTabsorder", new { id = tabsorder.Sordid }, tabsorder);
        }

        // DELETE: api/Orders/5
        [HttpPatch("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Tabsorder>> DeleteTabsorder(decimal id)
        {
            var tabsorder = await _context.Tabsorder.FindAsync(id);
            if (tabsorder == null)
            {
                return NotFound();
            }
            tabsorder.Dataexst = "DEL";
            await _context.SaveChangesAsync();

            return tabsorder;
        }

        private bool TabsorderExists(decimal id)
        {
            return _context.Tabsorder.Any(e => e.Sordid == id);
        }
    }
}
