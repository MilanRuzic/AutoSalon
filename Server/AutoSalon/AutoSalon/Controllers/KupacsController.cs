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
    public class KupacsController : ControllerBase
    {
        private readonly AutoSalonContext _context;
        private readonly IMapper _mapper;

        public KupacsController(AutoSalonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Kupacs
        [HttpGet]
        public async Task<ActionResult> GetKupacs()
        {
            var kupci = _context.Kupacs.ToList();
            return Ok(_mapper.Map<List<KupacViewModel>>(kupci));
        }

        // GET: api/Kupacs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kupac>> GetKupac(decimal id)
        {
            var kupac = await _context.Kupacs.FindAsync(id);

            if (kupac == null)
            {
                return NotFound();
            }

            return kupac;
        }

        // PUT: api/Kupacs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKupac(decimal id, Kupac kupac)
        {
            if (id != kupac.Kupacid)
            {
                return BadRequest();
            }

            _context.Entry(kupac).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KupacExists(id))
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

        // POST: api/Kupacs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Kupac>> PostKupac(Kupac kupac)
        {
            _context.Kupacs.Add(kupac);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKupac", new { id = kupac.Kupacid }, kupac);
        }

        // DELETE: api/Kupacs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kupac>> DeleteKupac(decimal id)
        {
            var kupac = await _context.Kupacs.FindAsync(id);
            if (kupac == null)
            {
                return NotFound();
            }

            _context.Kupacs.Remove(kupac);
            await _context.SaveChangesAsync();

            return kupac;
        }

        private bool KupacExists(decimal id)
        {
            return _context.Kupacs.Any(e => e.Kupacid == id);
        }
    }
}
