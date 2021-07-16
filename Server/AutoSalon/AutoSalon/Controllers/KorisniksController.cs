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
    public class KorisniksController : ControllerBase
    {
        private readonly AutoSalonContext _context;

        public KorisniksController(AutoSalonContext context)
        {
            _context = context;
        }

        // GET: api/Korisniks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Korisnik>>> GetKorisniks()
        {
            return await _context.Korisniks.ToListAsync();
        }

        // GET: api/Korisniks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Korisnik>> GetKorisnik(string username,string password)
        {
            var korisnik = await _context.Korisniks.FindAsync(username);
            
            if (korisnik == null || korisnik.Lozinka != password)
            {
                return NotFound();
            }
            else 
            return korisnik;
        }

        // PUT: api/Korisniks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKorisnik(string id, Korisnik korisnik)
        {
            if (id != korisnik.Korisnickoime)
            {
                return BadRequest();
            }

            _context.Entry(korisnik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
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

        // POST: api/Korisniks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Korisnik>> PostKorisnik(Korisnik korisnik)
        {
            _context.Korisniks.Add(korisnik);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KorisnikExists(korisnik.Korisnickoime))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKorisnik", new { id = korisnik.Korisnickoime }, korisnik);
        }

        // DELETE: api/Korisniks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Korisnik>> DeleteKorisnik(string id)
        {
            var korisnik = await _context.Korisniks.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            _context.Korisniks.Remove(korisnik);
            await _context.SaveChangesAsync();

            return korisnik;
        }

        private bool KorisnikExists(string id)
        {
            return _context.Korisniks.Any(e => e.Korisnickoime == id);
        }
    }
}
