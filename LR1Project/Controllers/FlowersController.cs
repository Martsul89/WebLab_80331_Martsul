using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace LR1Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly ÀpplicationDbContext _context;

        public FlowersController(ÀpplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Flowers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flower>>> GetFlower(int? group)
        {
            return await _context
            .Flower
            .Where(d => !group.HasValue
            || d.FlowerGroupId.Equals(group.Value))
            ?.ToListAsync();
        }

        // GET: api/Flowers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flower>> GetFlower(int id)
        {
            var flower = await _context.Flower.FindAsync(id);

            if (flower == null)
            {
                return NotFound();
            }

            return flower;
        }

        // PUT: api/Flowers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlower(int id, Flower flower)
        {
            if (id != flower.FlowerId)
            {
                return BadRequest();
            }

            _context.Entry(flower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlowerExists(id))
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

        // POST: api/Flowers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Flower>> PostFlower(Flower flower)
        {
            _context.Flower.Add(flower);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlower", new { id = flower.FlowerId }, flower);
        }

        // DELETE: api/Flowers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Flower>> DeleteFlower(int id)
        {
            var flower = await _context.Flower.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }

            _context.Flower.Remove(flower);
            await _context.SaveChangesAsync();

            return flower;
        }

        private bool FlowerExists(int id)
        {
            return _context.Flower.Any(e => e.FlowerId == id);
        }
    }
}
