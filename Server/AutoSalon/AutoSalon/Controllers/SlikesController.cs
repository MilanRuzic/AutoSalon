using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoSalon.Models;
using AutoMapper;
using AutoSalon.ViewModels;

namespace AutoSalon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlikesController : ControllerBase
    {
        private readonly AutoSalonContext _context;
        private readonly IMapper _mapper;

        public SlikesController(AutoSalonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Slikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slike>>> GetSlikes()
        {
            return await _context.Slikes.ToListAsync();
        }

        // GET: api/Slikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slike>> GetSlike(decimal id)
        {
            var slike = _context.Slikes.Where(s=> s.Voziloid == id).ToList();

            if (slike == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<SlikeViewModel>>(slike));
        }

        // PUT: api/Slikes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlike(decimal id, Slike slike)
        {
            if (id != slike.Slikeid)
            {
                return BadRequest();
            }

            _context.Entry(slike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlikeExists(id))
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

        // POST: api/Slikes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Slike>> PostSlike(Slike slike)
        {
            _context.Slikes.Add(slike);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSlike", new { id = slike.Slikeid }, slike);
        }

        // DELETE: api/Slikes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Slike>> DeleteSlike(decimal id)
        {
            var slike = await _context.Slikes.FindAsync(id);
            if (slike == null)
            {
                return NotFound();
            }

            _context.Slikes.Remove(slike);
            await _context.SaveChangesAsync();

            return slike;
        }

        private bool SlikeExists(decimal id)
        {
            return _context.Slikes.Any(e => e.Slikeid == id);
        }
    }
}
