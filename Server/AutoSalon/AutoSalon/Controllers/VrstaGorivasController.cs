using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoSalon.Models;

namespace AutoSalon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VrstaGorivasController : ControllerBase
    {
        private readonly AutoSalonContext _context;

        public VrstaGorivasController(AutoSalonContext context)
        {
            _context = context;
        }

        // GET: api/VrstaGorivas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VrstaGoriva>>> GetVrstaGorivas()
        {
            return await _context.VrstaGorivas.ToListAsync();
        }

        // GET: api/VrstaGorivas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VrstaGoriva>> GetVrstaGoriva(decimal id)
        {
            var vrstaGoriva = await _context.VrstaGorivas.FindAsync(id);

            if (vrstaGoriva == null)
            {
                return NotFound();
            }

            return vrstaGoriva;
        }

        // PUT: api/VrstaGorivas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVrstaGoriva(decimal id, VrstaGoriva vrstaGoriva)
        {
            if (id != vrstaGoriva.Vrstagorivaid)
            {
                return BadRequest();
            }

            _context.Entry(vrstaGoriva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VrstaGorivaExists(id))
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

        // POST: api/VrstaGorivas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VrstaGoriva>> PostVrstaGoriva(VrstaGoriva vrstaGoriva)
        {
            _context.VrstaGorivas.Add(vrstaGoriva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVrstaGoriva", new { id = vrstaGoriva.Vrstagorivaid }, vrstaGoriva);
        }

        // DELETE: api/VrstaGorivas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VrstaGoriva>> DeleteVrstaGoriva(decimal id)
        {
            var vrstaGoriva = await _context.VrstaGorivas.FindAsync(id);
            if (vrstaGoriva == null)
            {
                return NotFound();
            }

            _context.VrstaGorivas.Remove(vrstaGoriva);
            await _context.SaveChangesAsync();

            return vrstaGoriva;
        }

        private bool VrstaGorivaExists(decimal id)
        {
            return _context.VrstaGorivas.Any(e => e.Vrstagorivaid == id);
        }
    }
}
