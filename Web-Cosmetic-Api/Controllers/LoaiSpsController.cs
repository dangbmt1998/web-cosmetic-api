using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Cosmetic_Api.Models;

namespace Web_Cosmetic_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiSpsController : ControllerBase
    {
        private readonly usb40857_webusbeautyContext _context;

        public LoaiSpsController(usb40857_webusbeautyContext context)
        {
            _context = context;
        }

        // GET: api/LoaiSps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiSp>>> GetLoaiSps()
        {
            return await _context.LoaiSps.ToListAsync();
        }

        // GET: api/LoaiSps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiSp>> GetLoaiSp(int id)
        {
            var loaiSp = await _context.LoaiSps.FindAsync(id);

            if (loaiSp == null)
            {
                return NotFound();
            }

            return loaiSp;
        }

        // PUT: api/LoaiSps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoaiSp(int id, LoaiSp loaiSp)
        {

            var lsp = new LoaiSp();

            var result = _context.LoaiSps.Where(th => th.Id == id).FirstOrDefault<LoaiSp>();
            if (result != null && lsp != null)
            {
                result.TenLoaiSp = loaiSp.TenLoaiSp;
                result.MoTa = loaiSp.MoTa;
                _context.SaveChanges();
            }
            return Ok();
        }

        // POST: api/LoaiSps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{idTH}")]
        public async Task<ActionResult<LoaiSp>> PostLoaiSp(int idTH,LoaiSp loaiSp)
        {
             loaiSp.IdThuongHieu = idTH;
            _context.LoaiSps.Add(loaiSp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoaiSp", new { id = loaiSp.Id }, loaiSp);
        }

        // DELETE: api/LoaiSps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoaiSp>> DeleteLoaiSp(int id)
        {
            var loaiSp = await _context.LoaiSps.FindAsync(id);
            if (loaiSp == null)
            {
                return NotFound();
            }

            _context.LoaiSps.Remove(loaiSp);
            await _context.SaveChangesAsync();

            return loaiSp;
        }

        private bool LoaiSpExists(int id)
        {
            return _context.LoaiSps.Any(e => e.Id == id);
        }
    }
}
