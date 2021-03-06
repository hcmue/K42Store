﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using K42Store.Models;

namespace K42Store.Controllers
{
    [Route("api/HangHoa")]
    [ApiController]
    public class HangHoaAPIController : ControllerBase
    {
        private readonly MyeStoreContext _context;

        public HangHoaAPIController(MyeStoreContext context)
        {
            _context = context;
        }

        // GET: api/HangHoaAPI
        [HttpGet]
        public IEnumerable<HangHoa> GetHangHoa()
        {
            return _context.HangHoa;
        }

        // GET: api/HangHoaAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHangHoa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hangHoa = await _context.HangHoa.FindAsync(id);

            if (hangHoa == null)
            {
                return NotFound();
            }

            return Ok(hangHoa);
        }

        // PUT: api/HangHoaAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHangHoa([FromRoute] int id, [FromBody] HangHoa hangHoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hangHoa.MaHh)
            {
                return BadRequest();
            }

            _context.Entry(hangHoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HangHoaExists(id))
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

        // POST: api/HangHoaAPI
        [HttpPost]
        public async Task<IActionResult> PostHangHoa([FromBody] HangHoa hangHoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HangHoa.Add(hangHoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHangHoa", new { id = hangHoa.MaHh }, hangHoa);
        }

        // DELETE: api/HangHoaAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHangHoa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hangHoa = await _context.HangHoa.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            _context.HangHoa.Remove(hangHoa);
            await _context.SaveChangesAsync();

            return Ok(hangHoa);
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoa.Any(e => e.MaHh == id);
        }
    }
}