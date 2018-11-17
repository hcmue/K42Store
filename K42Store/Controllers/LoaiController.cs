using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using K42Store.Models;

namespace K42Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyeStoreContext _context;

        public LoaiController(MyeStoreContext context)
        {
            _context = context;
        }

        // GET: api/Loai
        [HttpGet]
        public IEnumerable<Loai> GetLoai()
        {
            return _context.Loai;
        }

        // GET: api/Loai/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoai([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loai = await _context.Loai.FindAsync(id);

            if (loai == null)
            {
                return NotFound();
            }

            return Ok(loai);
        }

        // PUT: api/Loai/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoai([FromRoute] int id, [FromBody] Loai loai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loai.MaLoai)
            {
                return BadRequest();
            }

            _context.Entry(loai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiExists(id))
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

        // POST: api/Loai
        [HttpPost]
        public async Task<IActionResult> PostLoai([FromBody] Loai loai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Loai.Add(loai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoai", new { id = loai.MaLoai }, loai);
        }

        // DELETE: api/Loai/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoai([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loai = await _context.Loai.FindAsync(id);
            if (loai == null)
            {
                return NotFound();
            }

            _context.Loai.Remove(loai);
            await _context.SaveChangesAsync();

            return Ok(loai);
        }

        private bool LoaiExists(int id)
        {
            return _context.Loai.Any(e => e.MaLoai == id);
        }
    }
}