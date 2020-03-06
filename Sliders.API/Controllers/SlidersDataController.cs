using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sliders.API.Data;
using Sliders.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sliders.API.Controllers
{
    [Route("api/SlidersData")]
    [ApiController]
    public class SlidersDataController : ControllerBase
    {
        private readonly SlidersWebContext _context;

        public SlidersDataController(SlidersWebContext context)
        {
            _context = context;
        }

        // GET: api/SlidersData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SlidersData>>> GetSlidersDataAsync()
        {
            return await _context.SlidersData.ToListAsync();
        }

        // GET: api/SlidersData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SlidersData>> GetSlidersDataAsync(string id)
        {
            var items = await _context.SlidersData.ToListAsync();

            if (id.ToLower() == "last")
            {
                return items.OrderBy(data => data.Time).LastOrDefault();
            }
            else
            {
                var slidersData = await _context.SlidersData.FindAsync(id);

                if (slidersData == null)
                {
                    return NotFound();
                }
                else
                {
                    return slidersData;
                }
            }
        }

        // PUT: api/SlidersData/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlidersDataAsync(string id, SlidersData slidersData)
        {
            if (id != slidersData.Id)
            {
                return BadRequest();
            }

            _context.Entry(slidersData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlidersDataExists(id))
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

        // POST: api/SlidersData
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SlidersData>> PostSlidersDataAsync(SlidersData slidersData)
        {
            _context.SlidersData.Add(slidersData);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SlidersDataExists(slidersData.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/SlidersData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SlidersData>> DeleteSlidersDataAsync(string id)
        {
            var slidersData = await _context.SlidersData.FindAsync(id);
            if (slidersData == null)
            {
                return NotFound();
            }

            _context.SlidersData.Remove(slidersData);
            await _context.SaveChangesAsync();

            return slidersData;
        }

        // DELETE: api/SlidersData
        [HttpDelete]
        public async Task<ActionResult> DeleteSlidersDataAsync()
        {
            var items = await _context.SlidersData.ToArrayAsync();
            _context.SlidersData.RemoveRange(items);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlidersDataExists(string id)
        {
            return _context.SlidersData.Any(e => e.Id == id);
        }
    }
}