using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DogTrackerApi.Models;

namespace DogTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly DogTrackerContext _context;

        public EntriesController(DogTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            return await _context.Entries.ToListAsync();
        }

        // GET: api/Entries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetEntry(DateTime id)
        {
            var entry = await _context.Entries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            return entry;
        }

        // GET: api/Entries/Last/Boba
        [HttpGet("Last/{name}")]
        public async Task<ActionResult<Entry?>> GetLastEntry(string name)
        {
            Entry? entry;
            try
            {
                entry = await _context.Entries
                    .Where(entry => entry.Name == name)
                    .OrderByDescending(e => e.DateTimeId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                entry = null;
            }
            return entry;
        }

        // GET: api/Entries/LastPee/Boba
        [HttpGet("LastPee/{name}")]
        public async Task<ActionResult<Entry?>> GetLastPee(string name)
        {
            Entry? entry;
            try
            {
                entry = await _context.Entries
                    .Where(e => e.Name == name && e.HasPeed)
                    .OrderByDescending(e => e.DateTimeId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                entry = null;
            }
            return entry;
        }

        // GET: api/Entries/LastPoo/Boba
        [HttpGet("LastPoo/{name}")]
        public async Task<ActionResult<Entry?>> GetLastPoo(string name)
        {
            Entry? entry;
            try
            {
                entry = await _context.Entries
                    .Where(e => e.Name == name && e.HasPooped)
                    .OrderByDescending(e => e.DateTimeId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                entry = null;
            }
            return entry;
        }

        // PUT: api/Entries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntry(DateTime id, Entry entry)
        {
            if (id != entry.DateTimeId)
            {
                return BadRequest();
            }

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
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

        // POST: api/Entries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entry>> PostEntry(Entry entry)
        {
            _context.Entries.Add(entry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EntryExists(entry.DateTimeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEntry", new { id = entry.DateTimeId }, entry);
        }

        // DELETE: api/Entries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(DateTime id)
        {
            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntryExists(DateTime id)
        {
            return _context.Entries.Any(e => e.DateTimeId == id);
        }
    }
}
