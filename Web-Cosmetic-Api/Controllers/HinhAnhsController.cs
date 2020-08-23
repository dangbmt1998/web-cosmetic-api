using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web_Cosmetic_Api.Models;

namespace Web_Cosmetic_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HinhAnhsController : ControllerBase
    {
        private readonly usb40857_webusbeautyContext _context;

        public HinhAnhsController(usb40857_webusbeautyContext context)
        {
            _context = context;
        }

        // GET: api/HinhAnhs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HinhAnh>>> GetHinhAnhs()
        {
            return await _context.HinhAnhs.ToListAsync();
        }

        // GET: api/HinhAnhs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HinhAnh>> GetHinhAnh(int id)
        {
            var hinhAnh = await _context.HinhAnhs.FindAsync(id);

            if (hinhAnh == null)
            {
                return NotFound();
            }

            return hinhAnh;
        }
        [HttpGet("HinhAnhSanPham/{idSp}")]
        public async Task<ActionResult<HinhAnh>> GetHinhAnhSanPham(int idSp)
        {
            var hinhAnh = _context.HinhAnhs.Where(ha => ha.IdSanPham == idSp).ToList();
            string str = "";
            foreach (var th in hinhAnh)
            {
                var data = new { id = th.Id, linkHinhAnh = th.LinkHinhAnh, tenHinhAnh = th.TenHinhAnh };
                str += JsonConvert.SerializeObject(data) + ",";
            }
            if (str == "")
            {
                return Ok("[]");
            }
            str = str.Remove(str.Length - 1);
            str = "[" + str + "]";
            return Ok(str);
        }
        // PUT: api/HinhAnhs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHinhAnh(int id, HinhAnh hinhAnh)
        {
            if (id != hinhAnh.Id)
            {
                return BadRequest();
            }

            _context.Entry(hinhAnh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HinhAnhExists(id))
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

        // POST: api/HinhAnhs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HinhAnh>> PostHinhAnh(HinhAnh hinhAnh)
        {
            _context.HinhAnhs.Add(hinhAnh);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHinhAnh", new { id = hinhAnh.Id }, hinhAnh);
        }

        // DELETE: api/HinhAnhs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HinhAnh>> DeleteHinhAnh(int id)
        {
            var hinhAnh = await _context.HinhAnhs.FindAsync(id);
            if (hinhAnh == null)
            {
                return NotFound();
            }

            _context.HinhAnhs.Remove(hinhAnh);
            await _context.SaveChangesAsync();

            return hinhAnh;
        }

        private bool HinhAnhExists(int id)
        {
            return _context.HinhAnhs.Any(e => e.Id == id);
        }
    }
}
