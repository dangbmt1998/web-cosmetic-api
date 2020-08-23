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
    public class KichCoSpsController : ControllerBase
    {
        private readonly usb40857_webusbeautyContext _context;

        public KichCoSpsController(usb40857_webusbeautyContext context)
        {
            _context = context;
        }

        // GET: api/KichCoSps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KichCoSp>>> GetKichCoSps()
        {
            return await _context.KichCoSps.ToListAsync();
        }

        // GET: api/KichCoSps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KichCoSp>> GetKichCoSp(int id)
        {
            var kichCoSp = await _context.KichCoSps.FindAsync(id);

            if (kichCoSp == null)
            {
                return NotFound();
            }

            return kichCoSp;
        }
        [HttpGet("DsKichCo/{idSP}")]
        public async Task<ActionResult<KichCoSp>> GetdsKichCoSp(int idSP)
        {
            var kichCoSP = _context.KichCoSps.Where(kc => kc.IdSanPham == idSP).ToList();
            string str = "";
            foreach (var kc in kichCoSP)
            {
                var data = new { id = kc.Id, giaSp = kc.GiaSp, tenKichCo = kc.TenKichCo };
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
        // PUT: api/KichCoSps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKichCoSp(int id, KichCoSp kichCoSp)
        {
            var lsp = new LoaiSp();

            var result = _context.KichCoSps.Where(th => th.Id == id).FirstOrDefault<KichCoSp>();
            if (result != null && lsp != null)
            {
                result.TenKichCo = kichCoSp.TenKichCo;
                result.GiaSp = kichCoSp.GiaSp;
                result.IdSanPham = kichCoSp.IdSanPham;
                _context.SaveChanges();
            }
            return Ok();
        }

        // POST: api/KichCoSps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KichCoSp>> PostKichCoSp(KichCoSp kichCoSp)
        {
            _context.KichCoSps.Add(kichCoSp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKichCoSp", new { id = kichCoSp.Id }, kichCoSp);
        }

        // DELETE: api/KichCoSps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KichCoSp>> DeleteKichCoSp(int id)
        {
            var kichCoSp = await _context.KichCoSps.FindAsync(id);
            if (kichCoSp == null)
            {
                return NotFound();
            }

            _context.KichCoSps.Remove(kichCoSp);
            await _context.SaveChangesAsync();

            return kichCoSp;
        }

        private bool KichCoSpExists(int id)
        {
            return _context.KichCoSps.Any(e => e.Id == id);
        }
    }
}
