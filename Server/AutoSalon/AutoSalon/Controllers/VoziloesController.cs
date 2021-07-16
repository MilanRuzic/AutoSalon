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
    public class VoziloesController : ControllerBase
    {
        private readonly AutoSalonContext _context;
        private readonly IMapper _mapper;
     

        public VoziloesController(AutoSalonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Route("filter")]
        public async Task<ActionResult> FilterVozilos(decimal proizvodjacId,decimal modelId, decimal cenaOd, decimal cenaDo,decimal vrstaGorivaId, decimal kilometrazaOd, decimal kilometrazaDo,decimal snagaMotoraOd,decimal snagaMotoraDo,Boolean prodata)
        {

            var vozila = from vozilo in _context.Vozilos select vozilo;


            if (proizvodjacId != 0)
                vozila = vozila.Where(vozilo => vozilo.Proizvodjacid == proizvodjacId);
            if (modelId != 0)
                vozila = vozila.Where(vozilo => vozilo.Modelid == modelId);
            if (vrstaGorivaId != 0)
                vozila = vozila.Where(vozilo => vozilo.Vrstagorivaid == vrstaGorivaId);

            if (cenaDo > 0)
                vozila = vozila.Where(vozilo => vozilo.Cena < cenaDo );

            if (snagaMotoraDo > 0)
                vozila = vozila.Where(vozilo => vozilo.Snagamotora < snagaMotoraDo);

            if (kilometrazaDo > 0)
                vozila = vozila.Where(vozilo => vozilo.Kilometraza < kilometrazaDo);

            vozila = vozila.Where(vozilo =>vozilo.Cena > cenaOd &&
            vozilo.Kilometraza > kilometrazaOd &&
            vozilo.Snagamotora > snagaMotoraOd);
            vozila = vozila.Include(vozilo => vozilo.Slikes);
            vozila = vozila.Include(vozilo => vozilo.Vrstagoriva);
            vozila = vozila.Include(vozilo => vozilo.Kupac);
            vozila = vozila.Include(vozilo => vozilo.Model).ThenInclude(model =>model.Proizvodjac);
               

                return  Ok( _mapper.Map<List<VoziloViewModel>>(vozila).ToList());
        }



        [Route("modelsSearch")]
        public async Task<ActionResult> modelSearch(decimal modelid)
        {
            var vozila = from vozilo in _context.Vozilos select vozilo;
            vozila = vozila.Where(vozilo => vozilo.Modelid == modelid);
            return Ok(_mapper.Map<List<VoziloViewModel>>(vozila).ToList());
        }



        [Route("sort")]
        public async Task<ActionResult> SortVozilos()
        {
            var vozila = _context.Vozilos
                ; // vozila.OrderBy(o => o.Cena);
            Console.WriteLine(vozila);
            return Ok(_mapper.Map<List<VoziloViewModel>>(vozila));
        
        }

        // GET: api/Voziloes
        [HttpGet]
        public async Task<ActionResult>GetVozilos()
        {
            // return await _context.Vozilos.Include(c=>c.Vrstagoriva).ToListAsync();
            var vozila =  _context
                 .Vozilos.Include(vozilo => vozilo.Slikes)
                 .Include(vozilo => vozilo.Vrstagoriva)
                 .Include(vozilo => vozilo.Kupac)
                 .Include(vozilo => vozilo.Model)
                 .ThenInclude(model => model.Proizvodjac).ToList();
            return Ok(_mapper.Map<List<VoziloViewModel>>(vozila));
        }

        // GET: api/Voziloes/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetVozilo(decimal id)
        {
            IQueryable<Vozilo> vozilo = _context.Vozilos.Where(v => v.Voziloid == id).Include(vozilo => vozilo.Slikes)
                 .Include(vozilo => vozilo.Vrstagoriva)
                 .Include(vozilo => vozilo.Kupac)
                 .Include(vozilo => vozilo.Model)
                 .ThenInclude(model => model.Proizvodjac);
            if (vozilo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<VoziloViewModel>>(vozilo)); 
        }

        // PUT: api/Voziloes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVozilo(decimal id, Vozilo vozilo)
        {
            if (id != vozilo.Voziloid)
            {
                return BadRequest();
            }

            _context.Entry(vozilo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoziloExists(id))
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

        // POST: api/Voziloes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vozilo>> PostVozilo(Vozilo vozilo)
        {
            _context.Vozilos.Add(vozilo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVozilo", new { id = vozilo.Voziloid }, vozilo);
        }

        // DELETE: api/Voziloes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vozilo>> DeleteVozilo(decimal id)
        {
            Vozilo vozilo = await _context.Vozilos.FirstAsync(c => c.Voziloid == id);
            if (vozilo == null)
            {
                return NotFound();
            }

            _context.Vozilos.Remove(vozilo);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<List<VoziloViewModel>>(vozilo));
        }

        private bool VoziloExists(decimal id)
        {
            return _context.Vozilos.Any(e => e.Voziloid == id);
        }
    }
}
