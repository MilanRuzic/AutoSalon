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
    public class ProizvodjacsController : ControllerBase
    {
        private readonly AutoSalonContext _context;
        private readonly IMapper _mapper;

        public ProizvodjacsController(AutoSalonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Proizvodjacs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proizvodjac>>> GetProizvodjacs()
        {

            var proizvodjaci = await _context.Proizvodjacs.Include(proizvodjac=>proizvodjac.Models).ToListAsync();
            return Ok(_mapper.Map<List<ProizvodjacViewModel>>(proizvodjaci));
        }

        // GET: api/Proizvodjacs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proizvodjac>> GetProizvodjac(decimal id)
        {
            var proizvodjac = await _context.Proizvodjacs.FindAsync(id);

            if (proizvodjac == null)
            {
                return NotFound();
            }

            return proizvodjac;
        }

        // PUT: api/Proizvodjacs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProizvodjac(decimal id, Proizvodjac proizvodjac)
        {
            if (id != proizvodjac.Proizvodjacid)
            {
                return BadRequest();
            }

            _context.Entry(proizvodjac).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProizvodjacExists(id))
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

        // POST: api/Proizvodjacs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Proizvodjac>> PostProizvodjac(Proizvodjac proizvodjac)
        {
            _context.Proizvodjacs.Add(proizvodjac);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProizvodjac", new { id = proizvodjac.Proizvodjacid }, proizvodjac);
        }

        // DELETE: api/Proizvodjacs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Proizvodjac>> DeleteProizvodjac(decimal id)
        {
            var proizvodjac = await _context.Proizvodjacs.FindAsync(id);
            if (proizvodjac == null)
            {
                return NotFound();
            }

            _context.Proizvodjacs.Remove(proizvodjac);
            await _context.SaveChangesAsync();

            return proizvodjac;
        }

        private bool ProizvodjacExists(decimal id)
        {
            return _context.Proizvodjacs.Any(e => e.Proizvodjacid == id);
        }
    }
}
